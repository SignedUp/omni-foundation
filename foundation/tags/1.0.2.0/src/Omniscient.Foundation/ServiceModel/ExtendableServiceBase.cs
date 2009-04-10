using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Omniscient.Foundation.ApplicationModel;

namespace Omniscient.Foundation.ServiceModel
{
    public abstract class ExtendableServiceBase<TServiceContract, TExtensionContract>: ServiceBase<TServiceContract>, IExtendable<TExtensionContract>
    {
        private List<IExtender<TExtensionContract>> _extenders;

        public ExtendableServiceBase()
        {
            _extenders = new List<IExtender<TExtensionContract>>();
        }

        #region IExtendable<TExtensionContract> Members

        public void Register(IExtender<TExtensionContract> extender)
        {
            _extenders.Add(extender);
        }

        public IEnumerable<IExtender<TExtensionContract>> Extenders
        {
            get 
            { 
                foreach (IExtender<TExtensionContract> ext in _extenders) yield return ext; 
            }
        }

        #endregion
    }
}
