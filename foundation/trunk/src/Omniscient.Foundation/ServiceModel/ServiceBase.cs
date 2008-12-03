using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omniscient.Foundation.ServiceModel
{
    public abstract class ServiceBase<TContract>: IService
    {
        #region IService Members

        public string Name
        {
            get { return this.GetType().Name; }
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

        public abstract TContract GetImplementation();

        #endregion
    }
}
