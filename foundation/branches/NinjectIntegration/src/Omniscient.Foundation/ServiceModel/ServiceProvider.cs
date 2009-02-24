using System;
using System.Collections.Generic;

namespace Omniscient.Foundation.ServiceModel
{
    /// <summary>
    /// Default implementation of an <see cref="IServiceProvider"/>.  Reads services from the configuration file.
    /// </summary>
    public class ServiceProvider: IServiceProvider
    {
        private Dictionary<Type, IService> _services;
        private List<IService> _sequentialServices;

        /// <summary>
        /// Ctor
        /// </summary>
        public ServiceProvider()
        {
            _services = new Dictionary<Type, IService>();
            _sequentialServices = new List<IService>();
        }

        /// <summary>
        /// Returns the contract implementation of a service, if any, that implements the contract <typeparamref name="TContract"/>.
        /// </summary>
        /// <typeparam name="TContract">The contract type.</typeparam>
        /// <returns>The contract's implementation, or null if no service found for that contract.</returns>
        public virtual TContract GetService<TContract>()
        {
            return (TContract)GetService(typeof(TContract));
        }

        public virtual object GetService(Type TContract)
        {
            IService srv;

            if (!_services.ContainsKey(TContract)) return null;
            srv = _services[TContract];

            object imp;
            imp = srv.GetImplementation();
            if (!TContract.IsAssignableFrom(imp.GetType()))
                throw new InvalidOperationException(string.Format("Service {0} does not return implementation of type {1}.", srv, TContract));
            
            return imp;
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
        public virtual void RegisterService<TContract>(IService service)
        {
            RegisterService(typeof(TContract), service);
        }

        /// <summary>
        /// Adds a service to the provider.
        /// </summary>
        /// <param name="contractType">Service's contract type.</param>
        /// <param name="service">Service implementation (must support contract's type)</param>
        public virtual void RegisterService(Type contractType, IService service)
        {
            if (!contractType.IsAssignableFrom(service.ImplementationType))
                throw new InvalidOperationException(string.Format("Cannot convert implementation type {0} to contract type {1}.", service.ImplementationType, contractType));

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

        public void Clear()
        {
            _services.Clear();
            _sequentialServices.Clear();
        }
    }
}
