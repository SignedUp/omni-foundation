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
        /// Writes the log entry to a medium.
        /// </summary>
        /// <param name="entry">The entry to log.</param>
        void Write(LogEntry entry);
    }
}
