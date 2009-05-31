using System;

namespace Omniscient.Foundation.Logging
{
    /// <summary>
    /// A log entry.
    /// </summary>
    public class LogEntry
    {
        /// <summary>
        /// Creates a log entry whose time is set to Now.
        /// </summary>
        public LogEntry()
        {
            Time = DateTime.Now;
        }

        /// <summary>
        /// Creates a log entry at specified time.
        /// </summary>
        /// <param name="time">The time of the entry</param>
        public LogEntry(DateTime time)
        {
            Time = time;
        }

        /// <summary>
        /// Creates a log entry whose time is set to Now.
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="level">The log level</param>
        public LogEntry(object message, LogLevel level)
            : this()
        {
            Message = message;
            Level = level;
        }

        /// <summary>
        /// A message to log
        /// </summary>
        public object Message { get; set; }
        
        /// <summary>
        /// The level at which the entry has been created
        /// </summary>
        public LogLevel Level { get; set; }
        
        /// <summary>
        /// The local time at which the entry has been created
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// Returns the string representation of the entry; this is what will be actually logged.
        /// </summary>
        /// <returns>A string representatino of the entry.</returns>
        public override string ToString()
        {
            return string.Format("{0,-5} - {1} {2} - {3}", Level.ToString().ToUpper(), Time.ToShortDateString(), Time.ToShortTimeString(), Message);
        }
    }
}
