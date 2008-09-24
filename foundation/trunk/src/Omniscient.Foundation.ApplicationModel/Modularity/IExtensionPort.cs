using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omniscient.Foundation.ApplicationModel.Modularity
{
    public interface IExtensionPort
    {
        Type ContractType { get; }
    }

    public interface IExtensionPort<TContract> : IExtensionPort
    { 
        void Register(IExtender<TContract> extender);
        IEnumerable<IExtender<TContract>> Extenders { get; }
    }
}
