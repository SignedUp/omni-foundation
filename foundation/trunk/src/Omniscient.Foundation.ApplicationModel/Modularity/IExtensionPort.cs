using System;
using System.Collections.Generic;

namespace Omniscient.Foundation.ApplicationModel.Modularity
{
    /// <summary>
    /// Defines a general extension port
    /// </summary>
    public interface IExtensionPort
    {
        /// <summary>
        /// Contract type that extenders must implement when connecting into that extension port.
        /// </summary>
        Type ContractType { get; }
    }

    /// <summary>
    /// Defines an extension port for contract <typeparamref name="TContract"/>.
    /// </summary>
    /// <typeparam name="TContract">Contract type that extenders must implement when connecting into that extension port.</typeparam>
    public interface IExtensionPort<TContract> : IExtensionPort
    { 
        /// <summary>
        /// Registers an extender into the extension port.  The extender must implement contract <typeparamref name="TContract"/>.
        /// </summary>
        /// <param name="extender"></param>
        void Register(IExtender<TContract> extender);
        
        /// <summary>
        /// Gets the list of plugged-in extenders
        /// </summary>
        IEnumerable<IExtender<TContract>> Extenders { get; }        
    }
}
