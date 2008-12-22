using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Omniscient.Foundation.ServiceModel;

namespace Omniscient.Foundation.Data.MySql
{
    internal class ConnectivityService: ServiceBase<IConnectivity>, IConfigurable
    {
        private ConnectivityServiceImpl _implementation;

        #region IConfigurable Members

        public void Configure(System.Xml.XmlElement config)
        {
            Configuration.Connectivity mysqlConfig;
            XmlSerializer ser;
            ser = new XmlSerializer(typeof(Configuration.Connectivity));
            mysqlConfig = (Configuration.Connectivity)ser.Deserialize(new XmlNodeReader(config));
            _implementation = new ConnectivityServiceImpl(mysqlConfig);
        }

        #endregion


        public override IConnectivity GetImplementation()
        {
            if (_implementation == null) throw new InvalidOperationException("Service hasn't been configured.");
            return _implementation;
        }
    }
}
