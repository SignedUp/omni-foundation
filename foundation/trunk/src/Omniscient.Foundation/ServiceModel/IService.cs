﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omniscient.Foundation.ServiceModel
{
    /// <summary>
    /// Defines the base interface for a service.
    /// </summary>
    public interface IService
    {
        /// <summary>
        /// Gets the name of the service.
        /// </summary>
        string Name { get; }
        
        /// <summary>
        /// Gets the contract type of the service.
        /// </summary>
        Type ContractType { get; }
    }

    /// <summary>
    /// Defines a service.  The service instance will be kept in memory for the application's lifetime.
    /// It is up to the service to decide to keep an instance of the implementation (singleton), or to provide a new
    /// instance each time it's being asked for the implementation (singlecall).
    /// </summary>
    /// <typeparam name="TContract">The type of contract implemented by this service.</typeparam>
    public interface IService<TContract>: IService
    {
        /// <summary>
        /// Returns the contract's implementation.
        /// </summary>
        /// <returns>Returns an instance of the contract's implementation.</returns>
        TContract GetImplementation();
    }
}
