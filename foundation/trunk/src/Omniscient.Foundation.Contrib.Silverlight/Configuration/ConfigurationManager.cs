using System;
using System.Collections.Generic;
using System.Windows;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Omniscient.Foundation.Contrib.Silverlight.Configuration
{
    /// <summary>
    /// Serves as a replacement to System.Configuration.ConfigurationManager for Silverlight.  
    /// By default, searches the .xap for a file named Silverlight.config
    /// </summary>
    public class ConfigurationManager
    {
        private static readonly XDocument XDocument;
        private static readonly Dictionary<string, object> Sections;

        static ConfigurationManager()
        {
            var streamInfo = Application.GetResourceStream(new Uri("Silverlight.config", UriKind.RelativeOrAbsolute));
            ConfigurationManager.XDocument = XDocument.Load(streamInfo.Stream);
            //ConfigurationManager.XDocument.
            ConfigurationManager.Sections = new Dictionary<string, object>();
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
        /// An object of type <see cref="IConfigurationSectionHandler{TSection}"/> that handles the section.
        /// </typeparam>
        /// <returns>
        /// </returns>
        public static TSection GetSection<TSection, TSectionHandler>(string sectionName)
            where TSectionHandler : IConfigurationSectionHandler<TSection>
        {
            var handler = (IConfigurationSectionHandler<TSection>) Activator.CreateInstance(typeof(TSectionHandler));
            var elements = ConfigurationManager.XDocument.Descendants(sectionName);
            if (elements == null) return default(TSection);

            foreach (XElement item in elements)
            {
                return handler.Create(item);
            }
            
            return default(TSection);
        }

        public static TSection GetSection<TSection>(string sectionName)
        {
            if (string.IsNullOrEmpty(sectionName)) return default(TSection);

            // do we have a section for it?

            // has it been cached?
            if (ConfigurationManager.Sections.ContainsKey(sectionName))
                return (TSection) ConfigurationManager.Sections[sectionName];

            var elements = ConfigurationManager.XDocument.Descendants(sectionName);
            if (elements == null) return default(TSection);

            XmlSerializer serializer = new XmlSerializer(typeof(TSection));
            foreach (XElement element in elements)
            {
                return (TSection) serializer.Deserialize(element.CreateReader());
            }
            
            return default(TSection);
        }
    }
}
