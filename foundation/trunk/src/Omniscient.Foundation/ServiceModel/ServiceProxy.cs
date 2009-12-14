using System;
using System.ServiceModel;

namespace Omniscient.Foundation.ServiceModel
{
    /// <summary>
    /// That service create a WCF communication channel
    /// </summary>
    /// <typeparam name="TContract">The type of the WCF contract</typeparam>
    public class ServiceProxy<TContract> : ConfigurableServiceBase<TContract, Configuration.Proxy>
    {
        /// <summary>
        /// Returns an implementation of the contract.
        /// </summary>
        /// <returns>An implementation of the contract.</returns>
        public override TContract GetImplementation()
        {
            if (this.Configuration == null) throw new InvalidOperationException("The service proxy is not configured.");

            var factory = new ChannelFactory<TContract>(this.Configuration.Endpoint);
            return factory.CreateChannel();
        }
    }
}
                                                                                              