﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Omniscient.Foundation.ApplicationModel.Configuration
{
    /// <summary>
    /// Placeholder for application configuration.  Will persist and unpersist against the schema defined in the file
    /// ApplicationConfiguration.xsd.
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
            ContainerConfiguration = new ObjectContainerConfiguration();
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

        [XmlElement("modules", Namespace = "http://schemas.omniscient.ca/foundation/applicationConfiguration.xsd")]
        public ModulesConfiguration ModulesConfiguration
        {
            get;
            set;
        }

        [XmlElement("container", Namespace = "http://schemas.omniscient.ca/foundation/applicationConfiguration.xsd")]
        public ObjectContainerConfiguration ContainerConfiguration
        {
            get;
            set;
        }
    }
}
