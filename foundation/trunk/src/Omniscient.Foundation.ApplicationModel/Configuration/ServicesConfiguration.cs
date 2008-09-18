using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Omniscient.Foundation.ApplicationModel.Configuration
{
    /// <summary>
    /// Placeholder for services configuration.  Contains a list of service definitions.
    /// </summary>
    [XmlType("ServicesConfiguration", Namespace = "http://schemas.omniscient.ca/foundation/applicationConfiguration.xsd")]
    public class ServicesConfiguration
    {
        private List<ServiceDefinition> _services;

        /// <summary>
        /// Ctor.  Creates an empty list of service definitions.
        /// </summary>
        public ServicesConfiguration()
        {
            _services = new List<ServiceDefinition>();
        }

        /// <summary>
        /// Gets the list of service definitions.
        /// </summary>
        [XmlElement("serviceDefinition", Namespace = "http://schemas.omniscient.ca/foundation/applicationConfiguration.xsd")]
        public List<ServiceDefinition> ServiceDefinitions
        {
            get { return _services; }
        }
    }
}
