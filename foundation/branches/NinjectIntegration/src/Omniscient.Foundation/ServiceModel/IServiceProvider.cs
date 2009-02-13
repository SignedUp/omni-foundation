using System;
using System.Collections.Generic;

namespace Omniscient.Foundation.ServiceModel
{
    /// <summary>
    /// Defines a service provider.
    /// </summary>
    public interface IServiceProvider: System.IServiceProvider
    {
        /// <summary>
        /// Gets a service that implements the contract <typeparamref name="TContract"/>.
        /// </summary>
        /// <typeparam name="TContract">The type of the contract implemented by the service.</typeparam>
        /// <returns>The implementation of the contract, or null if no service is found for this contract.</returns>
        TContract GetService<TContract>();

        /// <summary>
        /// Adds a service to the provider.
        /// </summary>
        /// <typeparam name="TContract">The type of contract that service implements.</typeparam>
        /// <param name="service">A service implementation.</param>
        void RegisterService<TContract>(IService service);

        /// <summary>
        /// Adds a service to the provider.
        /// </summary>
        /// <param name="contract">Service's contract type.</param>
        /// <param name="service">Service implementation (must support contract's type)</param>
        void RegisterService(Type contract, IService service);

        /// <summary>
        /// Gets all services, in the order they have been registered.
        /// </summary>
        IEnumerable<IService> AllServices { get; }

        /// <summary>
        /// Gets the number of services that are registered.
        /// </summary>
        int ServiceCount { get; }

        ///<summary>
        ///</summary>
        void Clear();
    }
}
