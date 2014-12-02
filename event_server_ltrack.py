import socket
import sys
import u6
from time import sleep
from json_logging import JsonLogThread
from labjack_logging import LabjackLogger
import ast

class EventServer( object ):
    """
    This class is able to generate a TCP/IP server that persists and listens
    for transient clients. Once a client is contacted, the server listens for
    instructions from the client and dispatched commands to other devices conditional
    on the client data. It also manages time locking with other machines.
    """
    def __init__(self, options):
        """
        Initialize class with options passed in from option parser,
        durations of events, start the labjack, etc.
        """
        self.port_num = options.port_num
        self.debug = options.debug
        
        # initialize json logging
        self.json_logger = JsonLogThread()
        self.json_logger.set_filename(options.output_path)
        self.json_logger.recording = True
        self.json_logger.start()

        # initialize labjack logger
        self.labjack_logger = LabjackLogger(options.config_file, self.json_logger)
        self.labjack_logger.start()

    def spawn_server(self):
        """
        Start a TCP server that listens for connections from clients,
        collects events, logs them, and potentially triggers labjack events.
        """
        # Create a TCP/IP socket
        sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)

        # Bind the socket to the port
        server_address = ('localhost', self.port_num)
        if self.debug:
            print >>sys.stderr, 'starting up on %s port %s' % server_address
        sock.bind(server_address)

        # Listen for incoming connections
        sock.listen(1)

#        while True:
        # Wait for a connection
        if self.debug:
            print >>sys.stderr, 'waiting for a connection'
        try:
            self.connection, client_address = sock.accept()
        except KeyboardInterrupt:
            print "Session ended via keypress."
#            break
        try:
            if self.debug:
                print >>sys.stderr, 'connection from', client_address

            # Receive the data.
            # Run as long as data is streaming.
            while True:
                # Note: if Unity messages increase in length, 
                # data recv size may need to increase
                data = self.connection.recv(1024) 
                if self.debug:
                    pass
                    #print >>sys.stderr, 'received "%s"' % data
                if data:
                    try:
                        # check if there is a data hangover
                        split_data = data.split("}")[:-1]
                        if len(split_data) > 1:
                            # if so run and log each split separately
                            for d in data:
                                current_data = d + '}' 
                                self.json_logger.log(current_data)
                                self.run_event(current_data)
                        else:
                            current_data = split_data[0] + '}'
                            self.json_logger.log(current_data)
                            self.run_event(current_data)
                    except Exception, e:
                        print "Lost event", data, "due to error:"
                        print e
                    except KeyboardInterrupt:
                        print "Run ended via ESC keypress."
                        break
                else:
                    if self.debug:
                        print >>sys.stderr, 'no more data from', client_address
                    break
        finally:
            # Clean up the connection
            self.connection.close()
            print "Connection to Unity closed."

            # Shut down the logging thread
            self.json_logger.shutdown()
            self.labjack_logger.shutdown()

    def run_event(self,data):
        """
        Run a particular event given an event number sent by a client oven TCP/IP.
        """
        params=ast.literal_eval(data)
        outcome=params['outcome']
        context=params['context']
        #print("outcome: " + outcome)
        #print("context: " + context)

        if context == 'None':
            self.labjack_logger.execute_task( {'mode':'write','register':5000+0, 'write_value':0} )
            self.labjack_logger.execute_task( {'mode':'write','register':6000+2, 'write_value':0} )
        elif context == 'reward':
            self.labjack_logger.execute_task( {'mode':'write','register':5000+0, 'write_value':5} )
            self.labjack_logger.execute_task( {'mode':'write','register':6000+2, 'write_value':1} )
        elif context == 'reward_morph':
            self.labjack_logger.execute_task( {'mode':'write','register':5000+0, 'write_value':5} )
            self.labjack_logger.execute_task( {'mode':'write','register':6000+2, 'write_value':1} )
        elif context == 'reward_ambiguous':
            self.labjack_logger.execute_task( {'mode':'write','register':5000+0, 'write_value':5} )
            self.labjack_logger.execute_task( {'mode':'write','register':6000+2, 'write_value':1} )
        elif context == 'restart':
            self.labjack_logger.execute_task( {'mode':'write','register':5000+0, 'write_value':5} )
            self.labjack_logger.execute_task( {'mode':'write','register':6000+2, 'write_value':1} )
        elif context == 'restart_morph':
            self.labjack_logger.execute_task( {'mode':'write','register':5000+0, 'write_value':5} )
            self.labjack_logger.execute_task( {'mode':'write','register':6000+2, 'write_value':1} )
        elif context == 'restart_ambiguous':
            self.labjack_logger.execute_task( {'mode':'write','register':5000+0, 'write_value':5} )
            self.labjack_logger.execute_task( {'mode':'write','register':6000+2, 'write_value':1} )


        if outcome == 'correct':
            print "----------------CORRECT------------------"
            self.labjack_logger.execute_task( {'mode':'write','register':6000+0, 'write_value':1} )
            self.labjack_logger.execute_task( {'mode':'write','register':6000+1, 'write_value':0} )
            self.labjack_logger.execute_task( {'mode':'write','register':5000+0, 'write_value':5} )
        elif outcome == 'restart':
            print "----------------RESTART------------------"
            self.labjack_logger.execute_task( {'mode':'write','register':6000+0, 'write_value':0} )
            self.labjack_logger.execute_task( {'mode':'write','register':6000+1, 'write_value':1} )
            self.labjack_logger.execute_task( {'mode':'write','register':5000+0, 'write_value':5} )
        elif outcome == 'None':
            self.labjack_logger.execute_task( {'mode':'write','register':6000+0, 'write_value':0} )
            self.labjack_logger.execute_task( {'mode':'write','register':6000+1, 'write_value':0} )
            self.labjack_logger.execute_task( {'mode':'write','register':5000+0, 'write_value':0} )
            self.labjack_logger.execute_task( {'mode':'write','register':5000+0, 'write_value':0} )
        else:
            raise NotImplementedError("Events > 3 have yet to be implemented.")

    def run(self):
        """
        Run the server.
        """
        self.spawn_server()
        sys.exit()

if __name__ == "__main__":

    # Parse command line options
    from optparse import OptionParser
    
    parser = OptionParser()

    parser.add_option('-o', "--output-path", dest="output_path", default='./test.log',
                      help="Specify the output path for the log file.")
    parser.add_option('-p', "--port-num", dest="port_num", type=int, default=9012,
                      help="Specifies the port number to send message over.")
    parser.add_option('', "--debug",
                      action="store_true", dest="debug", default=True,
                      help="Turn on debugging output.")
    parser.add_option('', "--config-file", dest="config_file", default='./labjack_config.cfg',
                      help="Configuration file for Labjack registers.")

    (options, args) = parser.parse_args()

    if options.debug:
        import time
        tic = time.clock()

    ES = EventServer( options )
    ES.run()

    if options.debug:
        toc = time.clock() - tic
        print "Server ran for ", toc, "seconds."
    sys.exit(0)

