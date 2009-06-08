using log4net;
using Omniscient.Foundation.Logging;

namespace Omniscient.Foundation.Contrib.Logging
{
    /// <summary>
    /// Wraps a log4net logger under the <see cref="ILogWriter"/> interface.
    /// </summary>
    public class Log4NetWriter: ILogWriter
    {
        private ILog _logger;

        /// <summary>
        /// Automatically configures log4net and creates a log4net logger from this namespace.
        /// Refer to constructor overload for default values.
        /// </summary>
        public Log4NetWriter()
        {
            log4net.Config.XmlConfigurator.Configure();
            Initialize(LogManager.GetLogger(typeof(Log4NetWriter)));
        }

        /// <summary>
        /// Creates a writer with a valid log4net logger.  Defaults to <see cref="Level"/> equal to
        /// <see cref="LogLevel.Fatal"/> and <see cref="IsEnabled"/> equal to False.
        /// </summary>
        /// <param name="log4netLogger">A valid log4net logger.</param>
        public Log4NetWriter(ILog log4netLogger)
        {
            Initialize(log4netLogger);
        }

        private void Initialize(ILog log4netLogger)
        {
            _logger = log4netLogger;
            Level = LogLevel.Fatal;
            IsEnabled = false;
        }

        /// <summary>
        /// Gets or sets the level under which no logs will make it to the underlying log4net logger.
        /// </summary>
        public LogLevel Level
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the writer is enabled or not.  If not, nothing
        /// will be logged.
        /// </summary>
        public bool IsEnabled
        {
            get;
            set;
        }

        /// <summary>
        /// Writes a log entry to the underlying log4net logger.
        /// </summary>
        /// <param name="entry">The entry to log.</param>
        public void Write(LogEntry entry)
        {
            if (!this.IsEnabled || this.Level > entry.Level || entry == null) return;
            switch (entry.Level)
            {
                case LogLevel.Debug:
                    _logger.Debug(entry.Message);
                    break;
                case LogLevel.Info:
                    _logger.Info(entry.Message);
                    break;
                case LogLevel.Error:
                    _logger.Error(entry.Message);
                    break;
                case LogLevel.Fatal:
                    _logger.Fatal(entry.Message);
                    break;
                default:
                    break;
            }
        }
    }
}
