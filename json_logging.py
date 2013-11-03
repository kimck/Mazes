# Threading libraries
import threading, Queue

# Numpy
import numpy as np

# Logging
import logging, jsonlogger, json

# Other libraries
import time

# -----------------------------------------------------------------------------
#                              JsonLogThread Class
# -----------------------------------------------------------------------------

class JsonLogThread( threading.Thread ):

    def __init__(self):
        super( JsonLogThread, self).__init__()

        self._should_shutdown = False
        
        self.json_logger = logging.getLogger('uStim')
        self.json_logger.setLevel(logging.DEBUG)
        self.json_logger.propagate = False  # Prevents messages going to the console.
        self.formatter = jsonlogger.JsonFormatter()

        # Initially the fileHandler in None.  Set it using the set_filename() method.
        self.fileHandler = None

        # Create a persistent, thread-safe queue to use when logging
        # things to disk.
        self.log_queue = Queue.Queue()

        # Mutex for controlling access to self.json_logger
        self.log_lock = threading.Lock()

        # Flag determines whether items are accepted into the queue
        self.recording = False

        # Time base
        self.T0 = time.time()

    def set_filename(self, filename, log_level = logging.DEBUG):
        '''
        Set the name of the file to log data to.  
        '''
        # Before changing the filename, be sure to flush the queue!!
        self._flush_queue()
        
        self.log_lock.acquire()
        if self.fileHandler != None:
            self.json_logger.removeHandler(self.fileHandler)
            self.fileHandler.close()

        # Create a new log file
        self.filename = filename
        self.fileHandler = logging.FileHandler(filename)
        self.fileHandler.setLevel(log_level)
        self.fileHandler.setFormatter(self.formatter)
        self.json_logger.addHandler(self.fileHandler)
        self.log_lock.release()

    def log(self, data, object_type='standard'):
        if self.recording:
            data_dict = {'timestamp': time.time()-self.T0,
                         'data': data,
                         'data_type': str(data.__class__),
                         'object_type': object_type }
            self.log_queue.put(data_dict)

    def shutdown(self):
        '''
        Shut down the logging thread.
        '''
        self._should_shutdown = True
        self.join()
        self._flush_queue()
        print 'JSON thread exiting gracefully.'
        
    def _flush_queue(self):
        # Flush the queue!
        while self.log_queue.empty() is False:
            data_dict = self.log_queue.get()
            if type(data_dict['data']) == np.ndarray:
                data_dict['data'] = data_dict['data'].tolist()
            data_json = json.dumps(data_dict)

            self.log_lock.acquire()
            self.json_logger.info(data_json)
            self.log_lock.release()

    def run(self):
        """
        Run method called by threading.Thread(*args).start()
        """
        while self._should_shutdown == False:
            self._flush_queue()
            time.sleep(1/30.0)   # only run 30 times per second


