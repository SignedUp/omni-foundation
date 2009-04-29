using System;

namespace Omniscient.Foundation.ServiceModel
{
    ///<summary>
    ///</summary>
    ///<typeparam name="TContract"></typeparam>
    public abstract class ServiceBase<TContract>: IService
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
        ///</summary>
        ///<returns></returns>
        public abstract TContract GetImplementation();

        #endregion
    }
}
