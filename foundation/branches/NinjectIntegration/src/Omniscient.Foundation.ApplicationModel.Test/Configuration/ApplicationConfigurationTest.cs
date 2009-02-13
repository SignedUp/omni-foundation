using NUnit.Framework;
using Omniscient.Foundation.ApplicationModel.Configuration;

namespace Omniscient.Foundation.ApplicationModel
{
    [TestFixture]
    public class ApplicationConfigurationTest
    {
        [Test]
        public void TestReadConfig()
        {
            string xml =
                @"<foundation.application xmlns='http://schemas.omniscient.ca/foundation/applicationConfiguration.xsd'>
                    <modules>
                        <moduleDefinition type='namespace.contract, assembly' />
                        <moduleDefinition type='namespace.contract2, assembly' />
                    </modules>
                    <services>
                        <serviceDefinition contract='namespace.contract, assembly' service='namespace.service, assembly' />
                        <serviceDefinition contract='namespace.contract2, assembly' service='namespace.service2, assembly'>
                            <config>
                                <superdave>yes</superdave>
                            </config>
                        </serviceDefinition>
                    </services>
                    <container>
                        <clear />
                        <add keyType='namespace.interface, assembly' objectType='namespace.class, assembly' />
                    </container>
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

            Assert.IsNotNull(config.ModulesConfiguration);
            Assert.AreEqual(2, config.ModulesConfiguration.Modules.Count);

            Assert.IsNotNull(config.ContainerConfiguration);
            Assert.IsNotNull(config.ContainerConfiguration.Items);
            Assert.AreEqual(2, config.ContainerConfiguration.Items.Count);
            Assert.AreEqual(typeof(ObjectContainerClear), config.ContainerConfiguration.Items[0].GetType());
            Assert.AreEqual(typeof(ObjectContainerAdd), config.ContainerConfiguration.Items[1].GetType());
            Assert.AreEqual("namespace.interface, assembly", ((ObjectContainerAdd)config.ContainerConfiguration.Items[1]).KeyType);
            Assert.AreEqual("namespace.class, assembly", ((ObjectContainerAdd)config.ContainerConfiguration.Items[1]).ObjectType);
        }
    }
}
