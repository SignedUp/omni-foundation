using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net.Mime;
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
        public Log4NetLogger()
        {
            XmlConfigurator.Configure();
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