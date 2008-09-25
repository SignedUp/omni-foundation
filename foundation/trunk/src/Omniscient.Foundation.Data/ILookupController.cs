using System;
using System.Collections.Generic;
using System.Text;

namespace Omniscient.Foundation.Data
{
    /// <summary>
    /// Defines a controller that manages a certain type of lookups.
    /// </summary>
    /// <typeparam name="TLookup">The type of lookups</typeparam>
    public interface ILookupController<TLookup>
        where TLookup: ILookup
    {
        /// <summary>
        /// Returns all available lookups of type <typeparamref name="TLookup"/>.
        /// </summary>
        /// <returns>An array of all available lookups.</returns>
        TLookup[] FetchAll();
    }
}
