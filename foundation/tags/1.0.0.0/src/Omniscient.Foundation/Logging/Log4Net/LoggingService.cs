using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using Omniscient.Foundation.ServiceModel;

namespace Omniscient.Foundation.Logging.Log4Net
{
    class LoggingService: ServiceBase<ILoggingService>, ILoggingService, IConfigurable
    {
        #region ILoggingService Members

        public ILogger GetLogger(string name)
        {
            return new Logger(log4net.LogManager.GetLogger(name));
        }

        public ILogger GetLogger(Type logSource)
        {
            return new Logger(log4net.LogManager.GetLogger(logSource));
        }

        #endregion

        #region IConfigurable Members

        public void Configure(System.Xml.XmlElement config)
        {
            log4net.Config.XmlConfigurator.Configure(config);
        }

        #endregion

        public override ILoggingService GetImplementation()
        {
            return this;
        }
    }
}
