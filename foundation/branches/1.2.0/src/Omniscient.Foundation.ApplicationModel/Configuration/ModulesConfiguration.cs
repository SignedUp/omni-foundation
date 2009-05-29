using System.Collections.Generic;
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

        ///<summary>
        ///</summary>
        public ModulesConfiguration()
        {
            _modules = new List<ModuleDefinition>();
        }

        ///<summary>
        ///</summary>
        [XmlElement("moduleDefinition", Namespace = "http://schemas.omniscient.ca/foundation/applicationConfiguration.xsd")]
        public List<ModuleDefinition> Modules
        {
            get { return _modules; }
        }
    }
}
