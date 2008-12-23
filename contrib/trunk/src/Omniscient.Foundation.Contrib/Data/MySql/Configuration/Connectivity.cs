using System.Xml.Serialization;

namespace Omniscient.Foundation.Contrib.Data.MySql.Configuration
{
    [XmlRoot("foundation.data.mysql", Namespace = "http://schemas.omniscient.ca/foundation/applicationConfiguration.xsd")]
    [XmlType("MysqlConnectivity", Namespace = "http://schemas.omniscient.ca/foundation/applicationConfiguration.xsd")]
    public class Connectivity
    {
        [XmlAttribute("connectionString")]
        public string ConnectionString { get; set; }
    }
}