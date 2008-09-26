using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using NUnit.Framework;
using Omniscient.Foundation;
using Omniscient.Foundation.ApplicationModel;
using Omniscient.Foundation.ApplicationModel.Configuration;

namespace Omniscient.Foundation.Data.MySql
{
    [TestFixture()]
    public class ConnectivityServiceTest
    {
        [TearDown()]
        public void TearDown()
        {
            ApplicationManager.Current.CloseApplication();
        }

        [Test()]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ConfigureFromConfigObjectNoSubConfig()
        {
            ApplicationConfiguration config;
            config = new ApplicationConfiguration();

            ServiceDefinition def;
            def = new ServiceDefinition();
            def.Contract = "Omniscient.Foundation.Data.MySql.IConnectivity, Omniscient.Foundation.Data";
            def.Service = "Omniscient.Foundation.Data.MySql.ConnectivityService, Omniscient.Foundation.Data";

            config.ServicesConfiguration.ServiceDefinitions.Add(def);

            ApplicationManager.Current.StartApplication(config);
            Assert.IsNotNull(ApplicationManager.Current.ServiceContainer.GetService<IConnectivity>());
        }

        [Test()]
        public void ConfigureFromConfigObject()
        {
            ApplicationConfiguration config;
            config = new ApplicationConfiguration();

            ServiceDefinition def;
            def = new ServiceDefinition();
            def.Contract = "Omniscient.Foundation.Data.MySql.IConnectivity, Omniscient.Foundation.Data";
            def.Service = "Omniscient.Foundation.Data.MySql.ConnectivityService, Omniscient.Foundation.Data";
            config.ServicesConfiguration.ServiceDefinitions.Add(def);

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(@"<foundation.data.mysql xmlns='http://schemas.omniscient.ca/foundation/applicationConfiguration.xsd' connectionstring='' />");
            def.Config = doc.DocumentElement;

            ApplicationManager.Current.StartApplication(config);
            Assert.IsNotNull(ApplicationManager.Current.ServiceContainer.GetService<IConnectivity>());
        }
    }
}
