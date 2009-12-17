namespace Omniscient.Foundation
{
    /// <summary>
    /// Represents something that is identifiable by a Name.
    /// </summary>
    public interface INamed
    {
        /// <summary>
        /// Gets or sets the name of this Named item.  Can be unique, or not 
        /// (this is not part of the contract; to be specified by concrete types)
        /// </summary>
        string Name { get; set; }
    }
}