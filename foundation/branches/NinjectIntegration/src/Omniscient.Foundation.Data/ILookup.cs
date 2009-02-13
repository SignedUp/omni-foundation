namespace Omniscient.Foundation.Data
{
    /// <summary>
    /// Represents a read-only value.  Generally used to represent a static set of values (e.g. provinces, countries)
    /// </summary>
    public interface ILookup
    {
        /// <summary>
        /// Gets a string that uniquelly identifies that lookup.
        /// </summary>
        string Code { get; }
        
        /// <summary>
        /// An English short description of that lookup.
        /// </summary>
        string ShortDescription { get; }

        /// <summary>
        /// An English long description of that lookup.
        /// </summary>
        string LongDescription { get; }
    }
}
