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
        public LogEntry() : this(DateTime.Now)
        {
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
            : this(DateTime.Now)
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
        /// <param name="format">Format of the line.  Defaults to "{l,-5} - {d} - {m}".  All params (l, d, m) are optional.
        /// {l}: replaced with the level of the log entry.
        /// {d}: replaced with the date and time of the log entry.
        /// {m}: replaced with the message of the log entry.
        /// </param>
        /// <param name="dateFormat">Optional.  If not null or empty, will call Time.ToString(dateFormat).  Otherwise, will use
        /// default date format.</param>
        /// <returns>A string representatino of the entry.</returns>
        public string ToString(string format, string dateFormat)
        {
            if (string.IsNullOrEmpty(format))
                format = "{l,-5} - {d} - {m}";
            string newFormat = format.Replace('l', '0').Replace('d', '1').Replace('m', '2');
            if (!string.IsNullOrEmpty(dateFormat))
                return string.Format(newFormat, this.Level.ToString().ToUpper(), this.Time.ToString(dateFormat), this.Message);
            else
                return string.Format(newFormat, this.Level.ToString().ToUpper(), this.Time, this.Message);
        }
    }
}
