using Omniscient.Foundation.Logging;
using System;

namespace Omniscient.Foundation.Contrib.Logging.Log4Net
{
    public class LogWriter: ILogWriter
    {
        private log4net.ILog _logger;

        public LogWriter(log4net.ILog logger)
        {
            _logger = logger;
            IsEnabled = true;
        }

        public LogLevel Level
        {
            get
            {
                if (_logger.IsFatalEnabled) return LogLevel.Fatal;
                if (_logger.IsErrorEnabled) return LogLevel.Error;
                if (_logger.IsInfoEnabled) return LogLevel.Info;
                return LogLevel.Debug;
            }
            set { throw new InvalidOperationException("Can't assign to log4net"); }
        }

        public bool IsEnabled
        {
            get;
            set;
        }

        public void Write(LogEntry entry)
        {
            if (!IsEnabled || this.Level > entry.Level || entry == null) return;
            switch (this.Level)
            {
                case LogLevel.Debug:
                    _logger.Debug(entry);
                    break;
                case LogLevel.Info:
                    _logger.Info(entry);
                    break;
                case LogLevel.Error:
                    _logger.Error(entry);
                    break;
                case LogLevel.Fatal:
                    _logger.Fatal(entry);
                    break;
                default:
                    break;
            }
        }
    }
}
