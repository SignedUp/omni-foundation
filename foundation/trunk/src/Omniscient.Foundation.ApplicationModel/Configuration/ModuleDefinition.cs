using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Omniscient.Foundation.ApplicationModel.Configuration
{
    [XmlType("ModuleDefinition", Namespace = "http://schemas.omniscient.ca/foundation/applicationConfiguration.xsd")]
    public class ModuleDefinition
    {

        [XmlAttribute("type")]
        public string Type { get; set; }
    }
}
