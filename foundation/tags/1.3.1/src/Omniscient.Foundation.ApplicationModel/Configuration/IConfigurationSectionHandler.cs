using System.Xml.Linq;

namespace Omniscient.Foundation.ApplicationModel.Configuration
{
    /// <summary>
    /// Deserializes an XElement configuration element to type <typeparam name="TSection" />.
    /// </summary>
    /// <typeparam name="TSection">
    /// The type of the object to be deserialized from the configuration section.
    /// </typeparam>
    public interface IConfigurationSectionHandler<TSection>
    {
        /// <summary>
        /// Creates an object of type <typeparamref name="TSection"/> from <see cref="XElement"/> <paramref name="element"/>.
        /// </summary>
        /// <param name="element">The xml element containing the section.</param>
        /// <returns>Deserialized object.</returns>
        TSection Create(XElement element);
    }
}
