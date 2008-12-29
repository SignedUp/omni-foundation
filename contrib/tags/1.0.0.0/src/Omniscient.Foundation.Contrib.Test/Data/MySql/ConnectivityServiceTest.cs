using System;
using System.Xml;
using NUnit.Framework;
using Omniscient.Foundation.ApplicationModel;
using Omniscient.Foundation.ApplicationModel.Configuration;
using Omniscient.Foundation.Contrib.Data.MySql;

namespace Omniscient.Foundation.Contrib.Test.Data.MySql
{
    [TestFixture]
    public class ConnectivityServiceTest
    {
        [TearDown]
        public void TearDown()
        {
            ApplicationManager.Current.CloseApplication();
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ConfigureFromConfigObjectNoSubConfig()
        {
            ApplicationConfiguration config;
            config = new ApplicationConfiguration();

            ServiceDefinition def;
            def = new ServiceDefinition();
            def.Contract = "Omniscient.Foundation.Contrib.Data.MySql.IConnectivity, Omniscient.Foundation.Contrib";
            def.Service = "Omniscient.Foundation.Contrib.Data.MySql.ConnectivityService, Omniscient.Foundation.Contrib";

            config.ServicesConfiguration.ServiceDefinitions.Add(def);

            ApplicationManager.Current.StartApplication(config);
            Assert.IsNotNull(ApplicationManager.Current.ServiceProvider.GetService<IConnectivity>());
        }

        [Test]
        public void ConfigureFromConfigObject()
        {
            ApplicationConfiguration config;
            config = new ApplicationConfiguration();

            ServiceDefinition def;
            def = new ServiceDefinition();
            def.Contract = "Omniscient.Foundation.Contrib.Data.MySql.IConnectivity, Omniscient.Foundation.Contrib";
            def.Service = "Omniscient.Foundation.Contrib.Data.MySql.ConnectivityService, Omniscient.Foundation.Contrib";
            config.ServicesConfiguration.ServiceDefinitions.Add(def);

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(@"<foundation.data.mysql xmlns='http://schemas.omniscient.ca/foundation/applicationConfiguration.xsd' connectionstring='' />");
            def.Config = doc.DocumentElement;

            ApplicationManager.Current.StartApplication(config);
            Assert.IsNotNull(ApplicationManager.Current.ServiceProvider.GetService<IConnectivity>());
        }
    }
}