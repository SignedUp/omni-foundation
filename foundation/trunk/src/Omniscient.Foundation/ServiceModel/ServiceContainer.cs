using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Omniscient.Foundation.ApplicationModel;

namespace Omniscient.Foundation.ServiceModel
{
    /// <summary>
    /// Default implementation of an <see cref="IServiceContainer"/>.  Reads services from the configuration file.
    /// </summary>
    public class ServiceContainer: IServiceContainer
    {
        private Dictionary<Type, object> _services;

        /// <summary>
        /// Ctor
        /// </summary>
        public ServiceContainer()
        {
            _services = new Dictionary<Type, object>();
        }

        /// <summary>
        /// Returns the contract implementation of a service, if any, that implements the contract <typeparamref name="TContract"/>.
        /// </summary>
        /// <typeparam name="TContract">The contract type.</typeparam>
        /// <returns>The contract's implementation, or null if no service found for that contract.</returns>
        public TContract GetService<TContract>()
        {
            IService<TContract> srv;

            if (!_services.ContainsKey(typeof(TContract))) return default(TContract);
            srv = (IService<TContract>)_services[typeof(TContract)];
            return srv.GetImplementation();
        }

        /// <summary>
        /// Registers a service at run-time.
        /// </summary>
        /// <typeparam name="TContract">The contract type.</typeparam>
        /// <param name="service">An instance of a service class.</param>
        public void RegisterService<TContract>(IService<TContract> service)
        {
            _services.Add(typeof(TContract), service);
        }

        /// <summary>
        /// Method called when it's time to configure the application.
        /// </summary>
        /// <param name="config">Current Application's configuration.</param>
        public void Configure(ApplicationConfiguration config)
        {
            foreach (ServiceDefinition srvDef in config.ServicesConfiguration.ServiceDefinitions)
            {
                Type contractType = Type.GetType(srvDef.Contract, true, true);
                Type serviceType = Type.GetType(srvDef.Service, true, true);
                Type generic = typeof(IService<>).MakeGenericType(contractType);
                if (!generic.IsAssignableFrom(serviceType))
                    throw new InvalidCastException(string.Format("Type {0} does not implement type {1}.", serviceType, generic));
                object service = Activator.CreateInstance(serviceType);
                _services.Add(contractType, service);
            }
        }
    }
}
