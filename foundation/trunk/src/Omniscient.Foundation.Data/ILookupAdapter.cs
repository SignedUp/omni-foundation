using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omniscient.Foundation.Data
{
    /// <summary>
    /// Represents an entity adapter to bridge with the database.
    /// </summary>
    /// <typeparam name="TLookup">The type of entity to manage.</typeparam>
    public interface ILookupAdapter<TLookup>
        where TLookup: ILookup, new()
    {
        /// <summary>
        /// Returns all available lookups for that type.
        /// </summary>
        /// <returns>All available lookups of that type.</returns>
        TLookup[] FetchAll();
    }
}
