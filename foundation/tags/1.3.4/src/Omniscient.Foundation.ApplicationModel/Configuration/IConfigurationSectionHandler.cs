using System.Xml.Linq;

namespace Omniscient.Foundation.ApplicationModel.Configuration
{
    /// <summary>
    /// Deserializes an XNode configuration element to a usable object.
    /// </summary>
    public interface IConfigurationSectionHandler
    {
        /// <summary>
        /// Deserializes configuration section <paramref name="section"/> and returns the deserialized object.
        /// </summary>
        /// <param name="parent">The parameter is not used.</param>
        /// <param name="configContext">The parameter is not used.</param>
        /// <param name="section">The section to deserialize.</param>
        /// <returns>Deserialized object.</returns>
        object Create(object parent, object configContext, XNode section);
    }
}
