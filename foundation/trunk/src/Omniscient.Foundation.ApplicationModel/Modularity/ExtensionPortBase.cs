using System;
using System.Collections.Generic;

namespace Omniscient.Foundation.ApplicationModel.Modularity
{
    ///<summary>
    ///</summary>
    ///<typeparam name="TContract"></typeparam>
    public class ExtensionPortBase<TContract>: IExtensionPort<TContract>
    {
        private List<IExtender<TContract>> _extenders;

        ///<summary>
        ///</summary>
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
