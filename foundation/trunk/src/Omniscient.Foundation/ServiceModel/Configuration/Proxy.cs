using System.Xml.Serialization;

namespace Omniscient.Foundation.ServiceModel.Configuration
{
    /// <summary>
    /// Deserializes a proxy configuration.
    /// </summary>
    [XmlType]
    [XmlRoot("proxy")]
    public class Proxy
    {
        /// <summary>
        /// Gets or sets the name of a WCF endpoint to use.
        /// </summary>
        [XmlAttribute("endpoint")]
        public string Endpoint { get; set; }
    }
}
