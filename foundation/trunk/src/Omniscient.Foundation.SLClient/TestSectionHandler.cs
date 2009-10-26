using System;
using System.Xml.Linq;
using System.Xml.Serialization;
using Omniscient.Foundation.ApplicationModel.Configuration;

namespace Omniscient.Foundation.SLClient
{
    public class TestSectionHandler : IConfigurationSectionHandler
    {
        /// <summary>
        /// Deserializes configuration section <paramref name="section"/> and returns the deserialized object.
        /// </summary>
        /// <param name="parent">The parameter is not used.</param>
        /// <param name="configContext">The parameter is not used.</param>
        /// <param name="section">The section to deserialize.</param>
        /// <returns>Deserialized object.</returns>
        public object Create(object parent, object configContext, XNode section)
        {
            var ser = new XmlSerializer(typeof(Test));
            return ser.Deserialize(section.CreateReader());
        }
    }

    [XmlType]
    [XmlRoot("test")]
    public class Test
    {
        [XmlElement("someElement")]
        public SomeElement SomeElement { get; set; }
    }

    [XmlType]
    public class SomeElement
    {
        [XmlAttribute("param")]
        public string Param { get; set; }

        [XmlText]
        public string Value { get; set; }
    }
}
