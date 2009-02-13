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
    public class GenericSingleCallService<TContract, TImplementation>: ServiceBase<TContract>
        where TImplementation: TContract, new()
    {
        /// <summary>
        /// Returns the implementation; instanciate a new one on each call.
        /// </summary>
        /// <returns>Service's implementation.</returns>
        public override TContract GetImplementation()
        {
            return new TImplementation();
        }
    }
}
