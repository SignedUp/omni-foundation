using System.Collections.Generic;
namespace Omniscient.Foundation.Data
{
    /// <summary>
    /// Represents an entity adapter to bridge with the database.
    /// </summary>
    /// <typeparam name="TLookup">The type of entity to manage.</typeparam>
    public interface ILookupAdapter<TLookup>
        where TLookup: ILookup
    {
        /// <summary>
        /// Loads a lookup by its code.
        /// </summary>
        /// <param name="code">The code to look for</param>
        /// <returns>A lookup with given code.</returns>
        TLookup LoadByCode(string code);

        /// <summary>
        /// Returns all available lookups for that type.
        /// </summary>
        /// <returns>All available lookups of that type.</returns>
        List<TLookup> LoadAll();
    }
}
