using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Omniscient.Foundation.ApplicationModel.Modularity;

namespace Omniscient.Foundation.ApplicationModel.Configuration
{
    [TestFixture()]
    public class ConfigManagerTest
    {
        [Test()]
        public void ConfigureModulesWith2Modules()
        {
            ApplicationConfiguration config = new ApplicationConfiguration();
            config.ModulesConfiguration.Modules.Add(new ModuleDefinition() { Type = "Omniscient.Foundation.ApplicationModel.Configuration.ConfigManagerTest+Module1, Omniscient.Foundation.ApplicationModel.Test" });
            config.ModulesConfiguration.Modules.Add(new ModuleDefinition() { Type = "Omniscient.Foundation.ApplicationModel.Configuration.ConfigManagerTest+Module2, Omniscient.Foundation.ApplicationModel.Test" });

            ObjectContainer container = new ObjectContainer();
            ConfigManager.ConfigureModules(container, config);

            Assert.AreEqual(2, container.AllObjects.Length);
        }

        public class Module1 : IModule
        {
            public Omniscient.Foundation.ApplicationModel.Presentation.IPresentationController PresentationController { get; set; }
        }

        public class Module2 : IModule
        {
            public Omniscient.Foundation.ApplicationModel.Presentation.IPresentationController PresentationController { get; set; }
        }
    }       
}
