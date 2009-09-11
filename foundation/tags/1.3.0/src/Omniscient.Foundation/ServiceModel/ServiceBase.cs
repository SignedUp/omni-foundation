using System;

namespace Omniscient.Foundation.ServiceModel
{
    ///<summary>
    /// Base class for services.
    ///</summary>
    ///<typeparam name="TContract">The service's contract type.</typeparam>
    public abstract class ServiceBase<TContract> : IService
    {
        #region IService Members

        public string Name
        {
            get { return GetType().Name; }
        }

        public Type ImplementationType
        {
            get { return typeof(TContract); }
        }

        object IService.GetImplementation()
        {
            return GetImplementation();
        }

        #endregion

        #region IService<TContract> Members

        ///<summary>
        /// Returns an implementation of the contract.
        ///</summary>
        ///<returns>An implementation of the contract.</returns>
        public abstract TContract GetImplementation();

        #endregion
    }
}
