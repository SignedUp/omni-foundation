using System;
using System.Xml;
using System.Xml.Serialization;
using Omniscient.Foundation.Contrib.Data.MySql.Configuration;
using Omniscient.Foundation.Data;
using Omniscient.Foundation.ServiceModel;

namespace Omniscient.Foundation.Contrib.Data.MySql
{
    internal class ConnectivityService: ServiceBase<IConnectionProvider>, IConfigurable
    {
        private ConnectivityServiceImpl _implementation;

        #region IConfigurable Members

        public void Configure(XmlElement config)
        {
            Connectivity mysqlConfig;
            XmlSerializer ser;
            ser = new XmlSerializer(typeof(Connectivity));
            mysqlConfig = (Connectivity)ser.Deserialize(new XmlNodeReader(config));
            _implementation = new ConnectivityServiceImpl(mysqlConfig);
        }

        #endregion


        public override IConnectionProvider GetImplementation()
        {
            if (_implementation == null) throw new InvalidOperationException("Service hasn't been configured.");
            return _implementation;
        }
    }
}
