using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Omniscient.Foundation.Logging
{
    public class TextWriterBasedWriter: ILogWriter
    {
        public TextWriterBasedWriter(TextWriter writer)
        {
            Writer = writer;
            IsEnabled = true;
        }

        public LogLevel Level
        {
            get;
            set;
        }

        public bool IsEnabled
        {
            get;
            set;
        }

        public TextWriter Writer
        {
            get;
            set;
        }

        public void Write(LogEntry entry)
        {
            if (!this.IsEnabled || this.Level > entry.Level || entry == null) return;
            Writer.WriteLine(entry.ToString());
        }
    }
}
