﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Omniscient.Foundation.ApplicationModel
{
    [TestFixture()]
    public class ApplicationConfigurationTest
    {
        [Test()]
        public void TestReadConfig()
        {
            string xml =
                @"<foundation.application xmlns='http://schemas.omniscient.ca/foundation/applicationConfiguration.xsd'>
                    <services>
                        <serviceDefinition contract='namespace.contract, assembly' service='namespace.service, assembly' />
                        <serviceDefinition contract='namespace.contract2, assembly' service='namespace.service2, assembly'>
                            <config>
                                <superdave>yes</superdave>
                            </config>
                        </serviceDefinition>
                    </services>
                </foundation.application>";

            System.Xml.Serialization.XmlSerializer ser = new System.Xml.Serialization.XmlSerializer(typeof(ApplicationConfiguration));
            ApplicationConfiguration config = ser.Deserialize(new System.Xml.XmlTextReader(new System.IO.StringReader(xml))) as ApplicationConfiguration;
            Assert.IsNotNull(config);
            Assert.IsNotNull(config.ServicesConfiguration);
            Assert.AreEqual(2, config.ServicesConfiguration.ServiceDefinitions.Count);
            Assert.AreEqual("namespace.contract, assembly", config.ServicesConfiguration.ServiceDefinitions[0].Contract);
            Assert.AreEqual("namespace.service2, assembly", config.ServicesConfiguration.ServiceDefinitions[1].Service);
            Assert.IsNotNull(config.ServicesConfiguration.ServiceDefinitions[1].Config);
            Assert.AreEqual("yes", config.ServicesConfiguration.ServiceDefinitions[1].Config.InnerText);
        }
    }
}
