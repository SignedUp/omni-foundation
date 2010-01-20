namespace Omniscient.Foundation.ApplicationModel
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines a component whose behavior can be extended by means of other components
    /// that "plug" into the first, coupled only against contract <typeparamref name="TContract"/>.
    /// </summary>
    /// <typeparam name="TContract">Contract type that extenders must implement when pluging into that extendable.</typeparam>
    public interface IExtendable<TContract>
    {
        /// <summary>
        /// Gets a list of all extenders.
        /// </summary>
        IEnumerable<TContract> AllExtenders { get; }

        /// <summary>
        /// Registers an implementation of <typeparamref name="TContract"/> against the extendable object.
        /// </summary>
        /// <param name="implementation">The extender's implementation</param>
        void RegisterExtender(TContract implementation);

        /// <summary>
        /// Unregisters all extenders of a certain type on the extendable object.
        /// </summary>
        /// <param name="type">The extender's type to unregister</param>
        void UnregisterExtenders(Type type);
    }
}
