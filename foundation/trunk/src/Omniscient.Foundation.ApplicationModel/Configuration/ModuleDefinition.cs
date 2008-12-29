using System.Xml.Serialization;

namespace Omniscient.Foundation.ApplicationModel.Configuration
{
    ///<summary>
    ///</summary>
    [XmlType("ModuleDefinition", Namespace = "http://schemas.omniscient.ca/foundation/applicationConfiguration.xsd")]
    public class ModuleDefinition
    {

        ///<summary>
        ///</summary>
        [XmlAttribute("type")]
        public string Type { get; set; }
    }
}
