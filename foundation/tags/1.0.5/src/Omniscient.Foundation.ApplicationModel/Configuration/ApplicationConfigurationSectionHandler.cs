using System.Configuration;
using System.Xml;
using System.Xml.Serialization;

namespace Omniscient.Foundation.ApplicationModel.Configuration
{
    /// <summary>
    /// Section handler for "foundation.application" configuration section.
    /// </summary>
    public class ApplicationConfigurationSectionHandler: IConfigurationSectionHandler
    {
        /// <summary>
        /// Standard implementation (see <see cref="System.Configuration.IConfigurationSectionHandler"/>).
        /// </summary>
        /// <seealso cref="System.Xml.Serialization.XmlSerializer"/>
        /// <param name="parent"></param>
        /// <param name="configContext"></param>
        /// <param name="section"></param>
        /// <returns>A deserialized configuration.</returns>
        public object Create(object parent, object configContext, XmlNode section)
        {
            XmlSerializer ser;
            ser = new XmlSerializer(typeof(ApplicationConfiguration));
            return ser.Deserialize(new XmlNodeReader(section));
        }
    }
}
