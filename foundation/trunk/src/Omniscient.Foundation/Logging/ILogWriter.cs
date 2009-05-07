﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omniscient.Foundation.Logging
{
    /// <summary>
    /// Writer that ultimately receives log entries and write them to a media
    /// </summary>
    public interface ILogWriter
    {
        /// <summary>
        /// The level of log entries this writer accepts.  Anything below this level won't be passed to the writer.
        /// </summary>
        LogLevel Level { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether the writer is available to receive log entries.
        /// </summary>
        bool IsEnabled { get; set; }
        
        /// <summary>
        /// Writes the log entry to a medium.  Equivalent to calling Write(entry, false).
        /// </summary>
        /// <param name="entry">The entry to log.</param>
        void Write(LogEntry entry);

        /// <summary>
        /// Writes the log entry to a medium.
        /// </summary>
        /// <param name="entry">The entry to log.</param>
        /// <param name="flush">A value indicating whether the writer should call Flush after writing to the TextWriter.</param>
        void Write(LogEntry entry, bool flush);

        /// <summary>
        /// Flushes the writer to the underlying media.
        /// </summary>
        void Flush();

        /// <summary>
        /// Minimum level at which Flush will be automatically called after a call to Write.  Equivalent to calling Write(entry, false).
        /// </summary>
        LogLevel AutoflushLevel { get; set; }
    }
}
