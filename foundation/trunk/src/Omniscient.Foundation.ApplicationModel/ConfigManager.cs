using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Omniscient.Foundation;
using Omniscient.Foundation.ServiceModel;
using Omniscient.Foundation.ApplicationModel.Configuration;

namespace Omniscient.Foundation.ApplicationModel
{
    internal class ConfigManager
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
    }
}
