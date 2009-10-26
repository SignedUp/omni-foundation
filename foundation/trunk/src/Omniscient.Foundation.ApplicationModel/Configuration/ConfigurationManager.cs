using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Omniscient.Foundation.ApplicationModel.Configuration
{
    /// <summary>
    /// Serves as a replacement to System.Configuration.ConfigurationManager for Silverlight.  By default, searches the .xap for a file named 
    /// Silverlight.config.  That file must have &lt;configuration&gt; as the root.  The first child element of that node must be 
    /// &lt;configSections&gt;.  Does not support section groups.  Each section's type must be a valid implementation of <see cref="IConfigurationSectionHandler"/>.
    /// </summary>
    public class ConfigurationManager
    {
        private static readonly XDocument XDocument;
        private static readonly Dictionary<string, object> Sections;
        private static readonly ConfigSections ConfigSections;
        private static readonly bool HasConfiguration;

        static ConfigurationManager()
        {
            var streamInfo = Application.GetResourceStream(new Uri("Silverlight.config", UriKind.RelativeOrAbsolute));
            if (streamInfo == null || streamInfo.Stream == null) return;

            Sections = new Dictionary<string, object>();
            XDocument = XDocument.Load(streamInfo.Stream);
            if (XDocument.Root == null || XDocument.Root.Name != "configuration")
                throw new ConfigurationErrorsException(
                    "The configuration file must contain a root element called <configuration>.");

            if (XDocument.Root.FirstNode == null)
                throw new ConfigurationErrorsException(
                    "The <configuration> node must contain a <configSections> node as its first child.");

            XElement configSections = (XElement)XDocument.Root.FirstNode;
            if (configSections.Name != "configSections")
                throw new ConfigurationErrorsException(
                    "The first element under <configuration> must be <configSections>.");

            XmlSerializer serializer = new XmlSerializer(typeof(ConfigSections));
            ConfigSections = (ConfigSections)serializer.Deserialize(configSections.CreateReader());

            HasConfiguration = true;
        }

        /// <summary>
        /// Reads a section from the configuration file and returns it as a deserialized object of type <typeparamref name="TSection"/>.
        /// </summary>
        /// <param name="sectionName">
        /// The name of the section in the configuration file to deserialize.  That name can appear anywhere in the configuration file, but
        /// will be used only once (if more than one elements with that name appear, only the first will be deserialized).
        /// </param>
        /// <typeparam name="TSection">
        /// The type of the deserialized object that represents the section.
        /// </typeparam>
        /// <typeparam name="TSectionHandler">
        /// An object of type <see cref="IConfigurationSectionHandler"/> that handles the section.
        /// </typeparam>
        /// <returns>
        /// </returns>
        [Obsolete("Use GetSection(object, object, string) instead.  Obsolete for versions higher than 1.3.1.")]
        public static TSection GetSection<TSection, TSectionHandler>(string sectionName)
            where TSectionHandler : IConfigurationSectionHandler
        {
            var handler = (IConfigurationSectionHandler) Activator.CreateInstance(typeof(TSectionHandler));
            var elements = ConfigurationManager.XDocument.Descendants(sectionName);
            if (elements == null) return default(TSection);

            foreach (XElement item in elements)
            {
                return (TSection)handler.Create(null, null, item);
            }
            
            return default(TSection);
        }

        [Obsolete("Use GetSection(object, object, string) instead.  Obsolete for versions higher than 1.3.1.")]
        public static TSection GetSection<TSection>(string sectionName)
        {
            if (string.IsNullOrEmpty(sectionName)) return default(TSection);

            // do we have a section for it?

            // has it been cached?
            if (ConfigurationManager.Sections.ContainsKey(sectionName))
                return (TSection) ConfigurationManager.Sections[sectionName];

            IEnumerable<XElement> elements;
            if (sectionName == "foundation.application")
                elements = ConfigurationManager.XDocument.Descendants(XName.Get("foundation.application", "http://schemas.omniscient.ca/foundation/applicationConfiguration.xsd"));
            else
                elements = ConfigurationManager.XDocument.Descendants(sectionName);

            if (elements == null) return default(TSection);

            foreach (XElement element in elements)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(TSection));
                return (TSection)serializer.Deserialize(element.CreateReader());
            }
            
            return default(TSection);
        }

        /// <summary>
        /// Reads a section from the configuration file.
        /// </summary>
        /// <param name="sectionName">The name of the section in the configuration file.</param>
        /// <returns>A deserialized configuration object.</returns>
        public static object GetSection(string sectionName)
        {
            if (!HasConfiguration) return null;
            if (string.IsNullOrEmpty(sectionName)) return null;
            if (Sections.ContainsKey(sectionName)) return Sections[sectionName];

            var sectionNode = GetSectionNode(sectionName);
            if (sectionNode == null) return null;

            Section section = (from s in ConfigSections
                               where s.Name == sectionName
                               select s).FirstOrDefault();

            if (section == null) return null;
            var sectionHandlerType = Type.GetType(section.Type, false, true);
            if (sectionHandlerType == null)
                throw new ConfigurationErrorsException(string.Format(
                                                           "Section {0} has an invalid section handler type.",
                                                           sectionName));
            if (!typeof(IConfigurationSectionHandler).IsAssignableFrom(sectionHandlerType))
                throw new ConfigurationErrorsException(string.Format(
                                                            "Type {0} does not implement IConfigurationSectionHandler",
                                                            sectionHandlerType.FullName));

            IConfigurationSectionHandler handler = (IConfigurationSectionHandler)Activator.CreateInstance(sectionHandlerType);
            return handler.Create(null, null, sectionNode);
        }

        private static XElement GetSectionNode(string sectionname)
        {
            var elements = from e in XDocument.Descendants(sectionname)
                       select e;
            
            return elements.FirstOrDefault();
        }
    }
}
