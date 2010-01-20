﻿using System.Xml.Serialization;

namespace Omniscient.Foundation.ApplicationModel.Configuration
{
    /// <summary>
    /// Placeholder for application configuration.  Will persist and unpersist against the schema defined in the file
    /// ApplicationConfiguration.xsd.
    /// In the configuration file, the config section must
    /// be called "foundation.application", and must be in the namespace 
    /// "http://schemas.omniscient.ca/foundation/applicationConfiguration.xsd".  See the xsd schema file ApplicationConfiguration.xsd
    /// for the complete schema.
    /// </summary>
    [XmlRoot("foundation.application", Namespace = "http://schemas.omniscient.ca/foundation/applicationConfiguration.xsd")]
    [XmlType("ApplicationConfiguration", Namespace = "http://schemas.omniscient.ca/foundation/applicationConfiguration.xsd")]
    public class ApplicationConfiguration
    {
        /// <summary>
        /// Ctor.  Creates empty childs for optional childs.
        /// </summary>
        public ApplicationConfiguration()
        {
            ServicesConfiguration = new ServicesConfiguration();
            ModulesConfiguration = new ModulesConfiguration();
        }

        /// <summary>
        /// Services section.
        /// </summary>
        [XmlElement("services", Namespace = "http://schemas.omniscient.ca/foundation/applicationConfiguration.xsd")]
        public ServicesConfiguration ServicesConfiguration
        {
            get;
            set;
        }

        ///<summary>
        ///</summary>
        [XmlElement("modules", Namespace = "http://schemas.omniscient.ca/foundation/applicationConfiguration.xsd")]
        public ModulesConfiguration ModulesConfiguration
        {
            get;
            set;
        }

    }
}
