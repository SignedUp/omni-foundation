using System.Collections.Generic;
using System.IO;
using log4net;
using log4net.Config;
using Omniscient.Foundation.Logging;

namespace Omniscient.Foundation.Contrib.Logging
{
    ///<summary>
    /// Wrapper for log4net logger.
    ///</summary>
    public class Log4NetLogger : ILogger
    {
        protected ILog _logger;

        ///<summary>
        /// Constructor
        ///</summary>
        ///<param name="fileName">Log4Net configuration file name.</param>
        public Log4NetLogger(string fileName) : this(new FileInfo(fileName)) {}

        ///<summary>
        /// Constructor
        ///</summary>
        ///<param name="fileInfo">Log4Net configuration file information.</param>
        public Log4NetLogger(FileInfo fileInfo)
        {
            XmlConfigurator.ConfigureAndWatch(fileInfo);
            _logger = LogManager.GetLogger(typeof(Log4NetLogger));
        }

        public List<ILogWriter> Writers
        {
            get { throw new System.NotImplementedException(); }
        }

        public ILogWriter DefaultWriter
        {
            get { throw new System.NotImplementedException(); }
        }

        public void Log(LogEntry entry)
        {
            Log(entry.Message, entry.Level);
        }

        public void Log(object message, LogLevel level)
        {
            switch(level)
            {
                case LogLevel.Info:
                    Info(message);
                    break;
                case LogLevel.Debug:
                    Debug(message);
                    break;
                case LogLevel.Error:
                    Error(message);
                    break;
                case LogLevel.Fatal:
                    Fatal(message);
                    break;
            }
        }

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
    }
}