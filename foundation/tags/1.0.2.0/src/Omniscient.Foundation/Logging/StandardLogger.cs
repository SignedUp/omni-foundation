using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Omniscient.Foundation.Logging
{
    public class StandardLogger: ILogger
    {
        private TextWriter _writer;
        private bool _autoflush;

        public StandardLogger(TextWriter writer, bool autoflush)
        {
            _writer = writer;
            _autoflush = autoflush;
        }

        #region ILogger Members

        private void Write(object message)
        {
            _writer.WriteLine(message);
            if (_autoflush) _writer.Flush();
        }

        public void Debug(object message)
        {
            Write(message);
        }

        public void Info(object message)
        {
            Write(message);
        }

        public void Error(object message)
        {
            Write(message);
        }

        public void Fatal(object message)
        {
            Write(message);
        }

        #endregion
    }
}
