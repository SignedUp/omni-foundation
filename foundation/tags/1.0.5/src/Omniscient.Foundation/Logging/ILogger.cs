using System.Collections.Generic;
namespace Omniscient.Foundation.Logging
{
    ///<summary>
    /// Represents a logger.  It consists of a list of writers, and methods to log messages.
    ///</summary>
    public interface ILogger: IEnumerable<ILogWriter>
    {
        /// <summary>
        /// Registers a log writer against the logger.
        /// </summary>
        /// <param name="writer">The log writer to register</param>
        void Register(ILogWriter writer);

        /// <summary>
        /// Unregister a log writer from the logger.
        /// </summary>
        /// <param name="writer">The log writer to unregister.</param>
        void Unregister(ILogWriter writer);

        /// <summary>
        /// Logs a log entry directly
        /// </summary>
        /// <param name="entry">The entry to log</param>
        void Log(LogEntry entry);

        /// <summary>
        /// Creates and logs an entry of the specified level.
        /// </summary>
        /// <param name="message">The message to log</param>
        /// <param name="level">The level of the resulting log entry</param>
        void Log(object message, LogLevel level);

        /// <summary>
        /// Creates and logs an entry with the specified message, at the Debug level.
        /// </summary>
        /// <param name="message">The message to log</param>
        void Debug(object message);

        /// <summary>
        /// Creates and logs an entry with the specified message, at the Info level.
        /// </summary>
        /// <param name="message">The message to log</param>
        void Info(object message);

        /// <summary>
        /// Creates and logs an entry with the specified message, at the Error level.
        /// </summary>
        /// <param name="message">The message to log</param>
        void Error(object message);

        /// <summary>
        /// Creates and logs an entry with the specified message, at the Fatal level.
        /// </summary>
        /// <param name="message">The message to log</param>
        void Fatal(object message);
    }
}
