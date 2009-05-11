using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Omniscient.Foundation.Logging
{
    public class TextLogWriter: ILogWriter
    {
        public TextLogWriter(Stream stream)
        {
            Stream = stream;
            Writer = new StreamWriter(stream);
            IsEnabled = true;
        }

        /// <summary>
        /// Gets the underlying stream
        /// </summary>
        public Stream Stream { get; private set; }


        /// <summary>
        /// The level of log entries this writer accepts.  Anything below this level won't be passed to the writer.
        /// </summary>
        public LogLevel Level
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the writer is available to receive log entries.
        /// </summary>
        public bool IsEnabled
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the underlying TextWriter object.
        /// </summary>
        public TextWriter Writer
        {
            get;
            private set;
        }

        /// <summary>
        /// Writes the log entry to a medium.  Equivalent to calling Write(entry, false).
        /// </summary>
        /// <param name="entry">The entry to log.</param>
        public void Write(LogEntry entry)
        {
            this.Write(entry, false);
        }

        /// <summary>
        /// Writes the log entry to a medium.
        /// </summary>
        /// <param name="entry">The entry to log.</param>
        /// <param name="flush">A value indicating whether the writer should call Flush after writing to the TextWriter.</param>
        public void Write(LogEntry entry, bool flush)
        {
            if (!this.IsEnabled || this.Level > entry.Level || entry == null) return;
            Writer.WriteLine(entry.ToString());
            if (flush || this.AutoflushLevel <= entry.Level) Flush();
        }

        /// <summary>
        /// Flushes the writer to the underlying media.
        /// </summary>
        public void Flush()
        {
            Writer.Flush();
        }

        /// <summary>
        /// Minimum level at which Flush will be automatically called after a call to Write.  Equivalent to calling Write(entry, false).
        /// </summary>
        public LogLevel AutoflushLevel
        {
            get;
            set;
        }
    }
}
