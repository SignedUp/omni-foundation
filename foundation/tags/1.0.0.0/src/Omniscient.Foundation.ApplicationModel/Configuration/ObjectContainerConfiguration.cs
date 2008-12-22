using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Omniscient.Foundation.ApplicationModel.Configuration
{
    [XmlType()]
    public class ObjectContainerConfiguration
    {
        public ObjectContainerConfiguration()
        {
            Items = new ArrayList();
        }

        [XmlElement("add", typeof(ObjectContainerAdd))]
        [XmlElement("remove", typeof(ObjectContainerRemove))]
        [XmlElement("clear", typeof(ObjectContainerClear))]
        public ArrayList Items { get; set; }
    }

    [XmlType(AnonymousType = true)]
    public class ObjectContainerAdd
    {
        [XmlAttribute("keyType")]
        public string KeyType { get; set; }
        [XmlAttribute("objectType")]
        public string ObjectType { get; set; }
    }

    [XmlType(AnonymousType = true)]
    public class ObjectContainerRemove
    {
        [XmlAttribute("type")]
        public string TypeName { get; set; }
    }

    [XmlType(AnonymousType = true)]
    public class ObjectContainerClear
    { }
}
