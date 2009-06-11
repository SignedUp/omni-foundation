using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Omniscient.Foundation.ApplicationModel;

namespace Omniscient.Foundation.ServiceModel
{
    public abstract class ExtendableServiceBase<TServiceContract, TExtensionContract>
        : ServiceBase<TServiceContract>, IExtendable<TExtensionContract>
    {
        private List<TExtensionContract> _extenders;

        public ExtendableServiceBase()
        {
            _extenders = new List<TExtensionContract>();
        }

        #region IExtendable<TExtensionContract> Members

        public void RegisterExtender(TExtensionContract extender)
        {
            _extenders.Add(extender);
        }

        public IEnumerable<TExtensionContract> AllExtenders
        {
            get 
            { 
                foreach (TExtensionContract ext in _extenders) yield return ext; 
            }
        }

        #endregion
    }
}
