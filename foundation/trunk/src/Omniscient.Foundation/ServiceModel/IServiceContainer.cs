﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omniscient.Foundation.ServiceModel
{
    /// <summary>
    /// Defines a service container.
    /// </summary>
    public interface IServiceContainer
    {
        /// <summary>
        /// Gets a service that implements the contract <typeparamref name="TContract"/>.
        /// </summary>
        /// <typeparam name="TContract">The type of the contract implemented by the service.</typeparam>
        /// <returns>The implementation of the contract, or null if no service is found for this contract.</returns>
        TContract GetService<TContract>();

        /// <summary>
        /// Adds a pre-existing service to the container.
        /// </summary>
        /// <typeparam name="TContract">The type of contract that service implements.</typeparam>
        /// <param name="service">A service implementation.</param>
        void RegisterService<TContract>(IService<TContract> service);

        /// <summary>
        /// Method called when it's time to configure the application.
        /// </summary>
        /// <param name="config">Current Application's configuration.</param>
        void Configure(ApplicationModel.ApplicationConfiguration config);

        /// <summary>
        /// Gets all services, in the order they have been registered.
        /// </summary>
        IEnumerable<object> AllServices { get; }
    }
}
