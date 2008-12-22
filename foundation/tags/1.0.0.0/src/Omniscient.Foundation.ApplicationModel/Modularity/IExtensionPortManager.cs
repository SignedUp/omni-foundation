using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omniscient.Foundation.ApplicationModel.Modularity
{
    public interface IExtensionPortManager
    {
        void RegisterExtensionPort<TContract>(IExtensionPort<TContract> port);
        IExtensionPort<TContract> GetExtensionPort<TContract>();
    }
}
