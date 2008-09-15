using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omniscient.Framework.Data
{
    /// <summary>
    /// Generic dictionary for Entities.
    /// </summary>
    /// <typeparam name="TKey">The type to use as the key.</typeparam>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    public class EntityDictionary<TKey, TEntity>: Dictionary<TKey, TEntity>, IEntityDictionary<TKey, TEntity>
        where TEntity: IEntity, new()
    {
        /// <summary>
        /// Ctor.  Sets Status to <see cref="EntityStatus.NotLoadedYet"/>
        /// </summary>
        public EntityDictionary()
            : base()
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
