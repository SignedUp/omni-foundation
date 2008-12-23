using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omniscient.Foundation.ApplicationModel.Modularity
{
    public class ExtensionPortBase<TContract>: IExtensionPort<TContract>
    {
        private List<IExtender<TContract>> _extenders;

        public ExtensionPortBase()
        {
            _extenders = new List<IExtender<TContract>>();
        }

        #region IExtensionPort<TContract> Members

        public void Register(IExtender<TContract> extender)
        {
            _extenders.Add(extender);
        }

        public IEnumerable<IExtender<TContract>> Extenders
        {
            get { return _extenders.ToArray(); }
        }

        #endregion

        #region IExtensionPort Members

        public Type ContractType
        {
            get { return typeof(TContract); }
        }

        #endregion
    }
}
