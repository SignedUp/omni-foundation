using System.Xml;
using System.Xml.Serialization;

namespace Omniscient.Foundation.ApplicationModel.Configuration
{
    /// <summary>
    /// Placeholder for a service definition.
    /// </summary>
    [XmlType("ServiceDefinition", Namespace = "http://schemas.omniscient.ca/foundation/applicationConfiguration.xsd")]
    public class ServiceDefinition
    {
        /// <summary>
        /// The assembly-qualified name of the type that implements IService for that service.
        /// </summary>
        [XmlAttribute("service")]
        public string Service { get; set; }

        /// <summary>
        /// The assembly-qualified name of that service's contract.
        /// </summary>
        [XmlAttribute("contract")]
        public string Contract { get; set; }

        /// <summary>
        /// Optional.  Any configuration needed by a service that implements <see cref="IConfigurable"/>.
        /// </summary>
        [XmlElement("config", Namespace = "http://schemas.omniscient.ca/foundation/applicationConfiguration.xsd")]
#if SILVERLIGHT
        public System.Xml.Linq.XElement Config { get; set; }
#else
        public XmlElement Config { get; set; }
#endif
    }

}
