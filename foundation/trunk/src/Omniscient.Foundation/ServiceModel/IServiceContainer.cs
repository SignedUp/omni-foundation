using System;
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
    }
}
