using System;

namespace Omniscient.Foundation.Data
{
    /// <summary>
    /// Generic lookup controller.
    /// </summary>
    /// <typeparam name="TLookup">Type of lookup to manage.</typeparam>
    public class LookupController<TLookup>: ILookupController<TLookup>
        where TLookup: ILookup
    {
        /// <summary>
        /// Returns all available lookups of type <typeparamref name="TLookup"/>.
        /// </summary>
        /// <returns>An array of all available lookups.</returns>
        public TLookup[] FetchAll()
        {
            if (Adapter == null) throw new InvalidOperationException("Adapter is null.");
            return Adapter.FetchAll();
        }

        /// <summary>
        /// Gets or sets the lookup adapter used by this controller.
        /// </summary>
        public ILookupAdapter<TLookup> Adapter
        {
            get;
            set;
        }
    }
}
