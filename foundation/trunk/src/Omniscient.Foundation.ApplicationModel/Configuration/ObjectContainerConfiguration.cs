using System.Collections;
using System.Xml.Serialization;

namespace Omniscient.Foundation.ApplicationModel.Configuration
{
    ///<summary>
    ///</summary>
    [XmlType]
    public class ObjectContainerConfiguration
    {
        ///<summary>
        ///</summary>
        public ObjectContainerConfiguration()
        {
            Items = new ArrayList();
        }

        ///<summary>
        ///</summary>
        [XmlElement("add", typeof(ObjectContainerAdd))]
        [XmlElement("remove", typeof(ObjectContainerRemove))]
        [XmlElement("clear", typeof(ObjectContainerClear))]
        public ArrayList Items { get; set; }
    }

    ///<summary>
    ///</summary>
    [XmlType(AnonymousType = true)]
    public class ObjectContainerAdd
    {
        ///<summary>
        ///</summary>
        [XmlAttribute("keyType")]
        public string KeyType { get; set; }
        ///<summary>
        ///</summary>
        [XmlAttribute("objectType")]
        public string ObjectType { get; set; }
    }

    ///<summary>
    ///</summary>
    [XmlType(AnonymousType = true)]
    public class ObjectContainerRemove
    {
        ///<summary>
        ///</summary>
        [XmlAttribute("type")]
        public string TypeName { get; set; }
    }

    ///<summary>
    ///</summary>
    [XmlType(AnonymousType = true)]
    public class ObjectContainerClear
    { }
}
