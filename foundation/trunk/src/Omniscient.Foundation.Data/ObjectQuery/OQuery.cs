namespace Omniscient.Foundation.Data.ObjectQuery
{
    /// <summary>
    /// Defines an object query that acts on an entity of type <typeparamref name="TEntity"/>.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity this query acts on.</typeparam>
    public class OQuery<TEntity> where TEntity: IEntity
    {
        /// <summary>
        /// Ctor.  Creates the original WHERE clause; that <see cref="WhereGroup"/> is a logical AND.
        /// </summary>
        public OQuery()
        {
            Where = new WhereGroup(LogicalGroupComposition.And);
        }

        /// <summary>
        /// Returns the root of the WHERE clause.
        /// </summary>
        public WhereGroup Where { get; private set; }

        /// <summary>
        /// Not defined yet.
        /// </summary>
        public object Join { get; set; }

        /// <summary>
        /// Not defined yet.
        /// </summary>
        public object OrderBy { get; set; }

        /// <summary>
        /// Not defined yet.
        /// </summary>
        public object GroupBy { get; set; }
    }
}
