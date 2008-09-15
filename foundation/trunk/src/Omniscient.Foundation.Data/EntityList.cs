using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omniscient.Foundation.Data
{
    /// <summary>
    /// Generic entity list.
    /// </summary>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    public class EntityList<TEntity> : List<TEntity>, IEntityList<TEntity>
        where TEntity : IEntity, new()
    {
        /// <summary>
        /// Ctor.  Sets Status to <see cref="EntityStatus.NotLoadedYet"/>
        /// </summary>
        public EntityList()
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
