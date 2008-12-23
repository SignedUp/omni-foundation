using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omniscient.Foundation.ApplicationModel.Modularity
{
    public class ExtensionPortManager: IExtensionPortManager
    {
        private IObjectContainer _container;

        public ExtensionPortManager(IObjectContainer container)
        {
            _container = container;
        }

        #region IExtensionPortManager Members

        public void RegisterExtensionPort<TContract>(IExtensionPort<TContract> port)
        {
            _container.Register(port);
        }

        public IExtensionPort<TContract> GetExtensionPort<TContract>()
        {
            return _container.Get<IExtensionPort<TContract>>();
        }

        #endregion
    }
}
