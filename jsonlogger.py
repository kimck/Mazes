# --------------------------------------------------------------------------
# This file is a modification of code originally from:
# https://github.com/madzak/python-json-logger/blob/master/src/jsonlogger.py
#
# This code came with the following copyright:
#
#   Copyright (c) 2011, Zakaria Zajac 
#   All rights reserved.
#
#   Redistribution and use in source and binary forms, with or without modification,
#   are permitted provided that the following conditions are met:
#
#   * Redistributions of source code must retain the above copyright notice, this
#   list of conditions and the following disclaimer.
#
#   * Redistributions in binary form must reproduce the above copyright notice,
#   this list of conditions and the following disclaimer in the documentation and/or
#   other materials provided with the distribution.
#
#   THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
#   ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
#   WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED.
#   IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT,
#   INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING,
#   BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
#   DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF
#   LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE
#   OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED
#   OF THE POSSIBILITY OF SUCH DAMAGE.
#
# End copyright. 

import logging
import json
import re
from datetime import datetime

class JsonFormatter(logging.Formatter):
    """A custom formatter to format logging records as json objects"""

    def parse(self):
        standard_formatters = re.compile(r'\((.*?)\)', re.IGNORECASE)
        return standard_formatters.findall(self._fmt)

    def format(self, record):
        """Formats a log record and serializes to json"""
        mappings = {
            'asctime': create_timestamp,
            'message': lambda r: r.msg,
        }

        formatters = self.parse()

        log_record = {}
        for formatter in formatters:
            try:
                log_record[formatter] = mappings[formatter](record)
            except KeyError:
                log_record[formatter] = record.__dict__[formatter]

        return json.dumps(log_record)

def create_timestamp(record):
    """Creates a human readable timestamp for a log records created date"""

    timestamp = datetime.fromtimestamp(record.created)
    return timestamp.strftime("%y-%m-%d %H:%M:%S,%f"),

# --------------------------------------------------------------------------
# EOF
