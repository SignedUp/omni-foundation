using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Omniscient.Foundation.ApplicationModel.Configuration
{
    /// <summary>
    /// Placeholder for modules configuration.  Contains a list of service definitions.
    /// </summary>
    [XmlType("ModulesConfiguration", Namespace = "http://schemas.omniscient.ca/foundation/applicationConfiguration.xsd")]
    public class ModulesConfiguration
    {
        private List<ModuleDefinition> _modules;

        public ModulesConfiguration()
        {
            _modules = new List<ModuleDefinition>();
        }

        [XmlElement("moduleDefinition", Namespace = "http://schemas.omniscient.ca/foundation/applicationConfiguration.xsd")]
        public List<ModuleDefinition> Modules
        {
            get { return _modules; }
        }
    }
}
