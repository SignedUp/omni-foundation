using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omniscient.Framework.Data
{
    /// <summary>
    /// Defines a generic dictionary of entities.  See <see cref="EntityDictionaries"/> for generic implementation.
    /// </summary>
    /// <typeparam name="TKey">The key to use.</typeparam>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    public interface IEntityDictionary<TKey, TEntity> : IDictionary<TKey, TEntity>
    {
        /// <summary>
        /// Gets or sets the status of that collection.  Useful for lazy loading with <see cref="EntityStatus.NotLoadedYet"/>.
        /// </summary>
        EntityStatus Status { get; set; }
    }
}
