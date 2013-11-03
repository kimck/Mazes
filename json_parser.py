import sys
import json
import ast

#---------------------------------------------------------------------

class JsonParser( object ):
    """
    A class for parsing JSON log files written by json_logging.py.
    """
    def __init__( self, logfile, options ):
        self.logfile = logfile
        self.options = options

    def parse( self ):
        """
        Callable parsing method that loads self.logfile and parses
        it line-by-line into a list of dicts stored in self.log_list.
        """
        # Open the file and parse the first line to extract the first timestamp.
        self.log_list = []
        f = open(self.logfile)
        for l in f:
            try:
                json_line = json.loads(l)
                parsed_dict = ast.literal_eval(json_line['message'])
                self.log_list.append(parsed_dict)
            except:
                print json_line
                print json_line.keys()
        return self.log_list

    def _get_values(self, key_list):
        # recursively get values for sequence of keys
        out_list = self.log_list
        for key in key_list:
            out_list = [l[key] for l in out_list]

#---------------------------------------------------------------------

if __name__ == "__main__":
    
    # Parse command line options
    from optparse import OptionParser

    parser = OptionParser()
    parser.add_option("-o", "--output-file", dest="output_filename",
                      help="Specify the output filename.")

    (options, args) = parser.parse_args()

    if len(args) < 1:
        print 'You must supply the path to a JSON logfile.'
        sys.exit(1)

    json_parser = JsonParser( args[0], options )
    parsed = json_parser.parse()
    if options.output_filename:
        file = open(options.output_filename,'w')
        for item in parsed:
            file.write("%s\n" % item)
        file.close()

#    1/0
    
# EOF



