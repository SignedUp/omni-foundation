using System;
using System.Collections.Generic;
using System.Collections;

namespace Omniscient.Foundation.ApplicationModel.Modularity
{
    /// <summary>
    /// Defines a component whose behavior can be expanded by means of other components
    /// that "plug" into the first, coupled only against contract <typeparamref name="TContract"/>.
    /// </summary>
    /// <typeparam name="TContract">Contract type that extenders must implement when pluging into that expandable.</typeparam>
    public interface IExpandable<TContract>
    { 
        /// <summary>
        /// Plugs an extender into the expandable.  The extender must implement contract <typeparamref name="TContract"/>.
        /// </summary>
        /// <param name="extender"></param>
        new void Register(IExtender<TContract> extender);
        
        /// <summary>
        /// Gets the list of plugged-in extenders
        /// </summary>
        new IEnumerable<IExtender<TContract>> Extenders { get; }        
    }
}
