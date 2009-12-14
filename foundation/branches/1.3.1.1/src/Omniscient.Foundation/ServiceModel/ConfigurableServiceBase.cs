#region

using System.Xml;
using System.Xml.Serialization;
using Omniscient.Foundation.ApplicationModel;

#endregion

namespace Omniscient.Foundation.ServiceModel
{
    /// <summary>
    /// Base class for services that are configurable. A configurable service will have an xml node (the "root")
    /// under the &lt;config&gt; child element of the &lt;serviceDefinition&gt; element in the config file.
    /// </summary>
    /// <typeparam name="TContract">
    /// The service's contract type.
    /// </typeparam>
    /// <typeparam name="TConfigRoot">
    /// The xml serializable class that is the root of the configuration for this service.
    /// </typeparam>
    public abstract class ConfigurableServiceBase<TContract, TConfigRoot>
        : ServiceBase<TContract>, IConfigurable
        where TConfigRoot : class
    {
        /// <summary>
        /// Gets Configuration as it has been deserialized.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        public TConfigRoot Configuration
        {
            get;
            private set;
        }

        #region IConfigurable Members

        /// <summary>
        /// Automatically called at load time by the ApplicationManager.  Or, can be manually called.  The parameter
        /// must be deserializable into type <typeparamref name="TConfigRoot"/>.
        /// </summary>
        /// <param name="config">
        /// The config.  Must be deserializable into type <typeparamref name="TConfigRoot"/>.
        /// </param>
        public void Configure(XmlElement config)
        {
            var serializer = new XmlSerializer(typeof(TConfigRoot));
            Configuration = serializer.Deserialize(new XmlNodeReader(config)) as TConfigRoot;
        }

        #endregion
    }
}
