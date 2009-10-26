using System;
using System.Xml;
using Omniscient.Foundation.ApplicationModel;

namespace Omniscient.Foundation.ServiceModel
{
    /// <summary>
    /// That service create a WCF communication channel
    /// </summary>
    /// <typeparam name="TContract"></typeparam>
    public class ServiceProxy<TContract> : ServiceBase<TContract>, IConfigurable
    {
        /// <summary>
        /// Returns an implementation of the contract.
        /// </summary>
        /// <returns>An implementation of the contract.</returns>
        public override TContract GetImplementation()
        {
            throw new NotImplementedException();
        }

        public void Configure(XmlElement config)
        {
            throw new NotImplementedException();
        }
    }
}
                                                                                              