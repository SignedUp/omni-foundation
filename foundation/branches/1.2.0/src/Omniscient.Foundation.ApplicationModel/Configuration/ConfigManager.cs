using System;
using Omniscient.Foundation.ServiceModel;
using Omniscient.Foundation.ApplicationModel.Modularity;
using System.Collections.Generic;
using System.Xml;

namespace Omniscient.Foundation.ApplicationModel.Configuration
{
    internal class ConfigManager
    {
        internal static void LoadAndConfigureServices(ServiceModel.IServiceProvider container, ApplicationConfiguration config)
        {
            if (config == null) return;
            if (container == null) return;

            foreach (ServiceDefinition srvDef in config.ServicesConfiguration.ServiceDefinitions)
            {
                Type contractType = Type.GetType(srvDef.Contract, true, true);
                Type serviceType = Type.GetType(srvDef.Service, true, true);                
                IService service = (IService)Activator.CreateInstance(serviceType);
                container.RegisterService(contractType, service);

                //configure any IConfigurable service.
                if (typeof(IConfigurable).IsAssignableFrom(serviceType) && srvDef.Config != null)
                    ((IConfigurable)service).Configure(srvDef.Config);
            }

        }

        internal static void LoadModules(IApplicationModuleManager container, ApplicationConfiguration config)
        {
            if (config == null) return;
            if (container == null) return;

            foreach (ModuleDefinition moduleDef in config.ModulesConfiguration.Modules)
            {
                Type moduleType = Type.GetType(moduleDef.Type, false, true);
                if (moduleType == null) 
                    throw new InvalidOperationException(
                        string.Format("Cannot load type from string \"{0}\".", moduleDef.Type));
                if (!typeof(IApplicationModule).IsAssignableFrom(moduleType))
                    throw new InvalidCastException(string.Format("Type {0} does not implements type {1}.", moduleType, typeof(IApplicationModule)));

                IApplicationModule module = (IApplicationModule)Activator.CreateInstance(moduleType);
                container.Load(module);
            }
        }
    }
}
