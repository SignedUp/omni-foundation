using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omniscient.Foundation.ServiceModel
{
    /// <summary>
    /// Default implementation of an <see cref="IServiceContainer"/>.  Reads services from the configuration file.
    /// </summary>
    public class ServiceContainer: IServiceContainer
    {
        private Dictionary<Type, IService> _services;
        private List<IService> _sequentialServices;

        /// <summary>
        /// Ctor
        /// </summary>
        public ServiceContainer()
        {
            _services = new Dictionary<Type, IService>();
            _sequentialServices = new List<IService>();
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
        /// Gets all services, in the order they have been registered.
        /// </summary>
        public IEnumerable<IService> AllServices
        {
            get { return _sequentialServices.ToArray(); }
        }

        /// <summary>
        /// Registers a service at run-time.
        /// </summary>
        /// <typeparam name="TContract">The contract type.</typeparam>
        /// <param name="service">An instance of a service class.</param>
        public void RegisterService<TContract>(IService<TContract> service)
        {
            _services.Add(typeof(TContract), service);
            _sequentialServices.Add(service);
        }

        /// <summary>
        /// Adds a service to the container.
        /// </summary>
        /// <param name="contractType">Service's contract type.</param>
        /// <param name="service">Service implementation (must support contract's type)</param>
        public void RegisterService(Type contractType, IService service)
        {
            _services.Add(contractType, service);
            _sequentialServices.Add(service);
        }

        /// <summary>
        /// Gets the number of services that are registered.
        /// </summary>
        public int ServiceCount
        {
            get { return _sequentialServices.Count; }
        }

    }
}
