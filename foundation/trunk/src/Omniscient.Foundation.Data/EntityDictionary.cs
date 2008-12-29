using System.Collections.Generic;

namespace Omniscient.Foundation.Data
{
    /// <summary>
    /// Generic dictionary for Entities.
    /// </summary>
    /// <typeparam name="TKey">The type to use as the key.</typeparam>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    public class EntityDictionary<TKey, TEntity>: Dictionary<TKey, TEntity>, IEntityDictionary<TKey, TEntity>
        where TEntity: IEntity
    {
        /// <summary>
        /// Ctor.  Sets Status to <see cref="EntityStatus.NotLoadedYet"/>
        /// </summary>
        public EntityDictionary()
        {
            Status = EntityStatus.NotLoadedYet;    
        }

        /// <summary>
        /// Gets or sets the status of that collection.  Useful for lazy loading with <see cref="EntityStatus.NotLoadedYet"/>.
        /// </summary>
        public EntityStatus Status
        {
            get;
            set;
        }
    }
}
