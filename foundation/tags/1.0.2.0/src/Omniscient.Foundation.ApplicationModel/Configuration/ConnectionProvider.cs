using System.Xml.Serialization;

namespace Omniscient.Foundation.ApplicationModel.Configuration
{
    [XmlRoot("foundation.data.connectionProvider", Namespace = "http://schemas.omniscient.ca/foundation/applicationConfiguration.xsd")]
    [XmlType("ConnectionProvider", Namespace = "http://schemas.omniscient.ca/foundation/applicationConfiguration.xsd")]
    public class ConnectionProvider
    {
        [XmlAttribute("connectionStringName")]
        public string ConnectionStringName { get; set; }
    }
}
