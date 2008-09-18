using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omniscient.Foundation.Data
{
    /// <summary>
    /// Defines a generic list of entities.  See <see cref="EntityList{TEntity}"/> for generic implementation.
    /// </summary>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    public interface IEntityList<TEntity>: IList<TEntity>
        where TEntity: IEntity, new()
    {
        /// <summary>
        /// Gets or sets the status of that collection.  Useful for lazy loading with <see cref="EntityStatus.NotLoadedYet"/>.
        /// </summary>
        EntityStatus Status { get; set; }
    }

    /// <summary>
    /// Defines a generic list of entities.  See <see cref="EntityList"/> for generic implementation.
    /// </summary>
    public interface IEntityList : IList<IEntity>
    {
        /// <summary>
        /// Gets or sets the status of that collection.  Useful for lazy loading with <see cref="EntityStatus.NotLoadedYet"/>.
        /// </summary>
        EntityStatus Status { get; set; }
    }

}
