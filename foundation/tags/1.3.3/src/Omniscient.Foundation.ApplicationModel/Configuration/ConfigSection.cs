namespace Omniscient.Foundation.ApplicationModel.Configuration
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    /// <summary>
    /// Represents a section in the configuration file.
    /// </summary>
    [XmlType("section")]
    public class Section
    {
        /// <summary>
        /// Gets or sets the name of the section
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the type of the section handler.  Must implement <see cref="IConfigurationSectionHandler" />.
        /// </summary>
        [XmlAttribute("type")]
        public string Type { get; set; }
    }

    /// <summary>
    /// Represents a &lt;configSections&gt; element in the configuration file.
    /// </summary>
    [XmlType("configSections")]
    public class ConfigSections : List<Section> { }
}
