using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omniscient.Foundation.ServiceModel
{
    /// <summary>
    /// Defines a generic "single call" service for contract type <typeparamref name="TContract"/> 
    /// and implementation type <typeparamref name="TImplementation"/>.
    /// 
    /// This service's implementation will be instaciated each time the service is requested.
    /// </summary>
    /// <typeparam name="TContract">Service's contract type.</typeparam>
    /// <typeparam name="TImplementation">Implementation type.</typeparam>
    public class GenericSingleCallService<TContract, TImplementation>: IService<TContract>
        where TImplementation: TContract, new()
    {
        /// <summary>
        /// Returns the implementation; instanciate a new one on each call.
        /// </summary>
        /// <returns>Service's implementation.</returns>
        public TContract GetImplementation()
        {
            return new TImplementation();
        }

        /// <summary>
        /// Gets the name of the service, which is the type name of the contract.
        /// </summary>
        public string Name
        {
            get { return typeof(TContract).Name; }
        }

        /// <summary>
        /// Returns the contract type.
        /// </summary>
        public Type ContractType
        {
            get { return typeof(TContract); }
        }
    }
}
