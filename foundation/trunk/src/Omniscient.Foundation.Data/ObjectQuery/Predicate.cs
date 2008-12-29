namespace Omniscient.Foundation.Data.ObjectQuery
{
    /// <summary>
    /// Base class for predicates
    /// </summary>
    public abstract class Predicate
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="path">The path to the property being filtered.</param>
        public Predicate(string path)
        {
            Path = path;
        }

        /// <summary>
        /// Gets the path to the property being filtered.
        /// </summary>
        public string Path { get; private set; }
    }
}
