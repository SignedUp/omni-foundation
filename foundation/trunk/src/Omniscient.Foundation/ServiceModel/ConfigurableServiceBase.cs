using System.Xml.Serialization;
using Omniscient.Foundation.ApplicationModel;

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

        /// <summary>
        /// Automatically called at load time by the ApplicationManager.  Or, can be manually called.  The parameter
        /// must be deserializable into type <typeparamref name="TConfigRoot"/>.
        /// </summary>
        /// <param name="config">
        /// The config.  Must be deserializable into type <typeparamref name="TConfigRoot"/>.
        /// </param>
#if SILVERLIGHT
        public void Configure(System.Xml.Linq.XElement config)
        {
            var serializer = new XmlSerializer(typeof(TConfigRoot));
            Configuration = serializer.Deserialize(config.CreateReader()) as TConfigRoot;
        }
#else
        public void Configure(System.Xml.XmlElement config)
        {
            var serializer = new XmlSerializer(typeof(TConfigRoot));
            Configuration = serializer.Deserialize(new System.Xml.XmlNodeReader(config)) as TConfigRoot;
        }
#endif
    }
}
