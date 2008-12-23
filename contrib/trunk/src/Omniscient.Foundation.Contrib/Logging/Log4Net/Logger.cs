using Omniscient.Foundation.Logging;

namespace Omniscient.Foundation.Contrib.Logging.Log4Net
{
    class Logger: ILogger
    {
        private log4net.ILog _logger;

        public Logger(log4net.ILog logger)
        {
            _logger = logger;
        }

        #region ILogger Members

        public void Debug(object message)
        {
            _logger.Debug(message);
        }

        public void Info(object message)
        {
            _logger.Info(message);
        }

        public void Error(object message)
        {
            _logger.Error(message);
        }

        public void Fatal(object message)
        {
            _logger.Fatal(message);
        }

        #endregion
    }
}