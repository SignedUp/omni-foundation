using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Omniscient.Foundation;
using Omniscient.Foundation.ServiceModel;
using Omniscient.Foundation.ApplicationModel.Configuration;
using Omniscient.Foundation.ApplicationModel.Modularity;

namespace Omniscient.Foundation.ApplicationModel.Configuration
{
    class ConfigManager
    {
        public static void ConfigureServices(IServiceContainer container, ApplicationConfiguration config)
        {
            if (config == null) return;
            if (container == null) return;

            foreach (ServiceDefinition srvDef in config.ServicesConfiguration.ServiceDefinitions)
            {
                Type contractType = Type.GetType(srvDef.Contract, true, true);
                Type serviceType = Type.GetType(srvDef.Service, true, true);
                Type generic = typeof(IService<>).MakeGenericType(contractType);
                if (!generic.IsAssignableFrom(serviceType))
                    throw new InvalidCastException(string.Format("Type {0} does not implement type {1}.", serviceType, generic));
                
                IService service = (IService)Activator.CreateInstance(serviceType);
                container.RegisterService(contractType, service);

                //configure any IConfigurable service.
                if (typeof(IConfigurable).IsAssignableFrom(serviceType) && srvDef.Config != null)
                    ((IConfigurable)service).Configure(srvDef.Config);
            }

        }

        public static void ConfigureModules(IObjectContainer container, ApplicationConfiguration config)
        {
            if (config == null) return;
            if (container == null) return;

            foreach (ModuleDefinition moduleDef in config.ModulesConfiguration.Modules)
            {
                Type moduleType = Type.GetType(moduleDef.Type, false, true);
                if (moduleType == null) 
                    throw new InvalidOperationException(
                        string.Format("Cannot load type from string \"{0}\".", moduleDef.Type));
                if (!typeof(IModule).IsAssignableFrom(moduleType))
                    throw new InvalidCastException(string.Format("Type {0} does not implements type {1}.", moduleType, typeof(IModule)));

                object module = Activator.CreateInstance(moduleType);
                container.Register(moduleType, module);
            }
        }

        public static void ConfigureContainer(IObjectContainer container, ApplicationConfiguration config)
        {
            if (config == null || config.ContainerConfiguration == null) return;
            if (container == null) return;

            foreach (object item in config.ContainerConfiguration.Items)
            {
                ObjectContainerAdd add = item as ObjectContainerAdd;
                if (item != null)
                {
                    Type tKey = Type.GetType(add.KeyType, true, true);
                    Type tObj = Type.GetType(add.ObjectType, true, true);

                    container.Register(tKey, Activator.CreateInstance(tObj));
                    continue;
                }

                ObjectContainerRemove remove = item as ObjectContainerRemove;
                if (item != null)
                {
                    throw new NotImplementedException("Removing an object is not supported yet.");
                }

                ObjectContainerClear clear = item as ObjectContainerClear;
                if (item != null)
                {
                    container.Clear();
                    continue;
                }
            }
        }
    }
}
