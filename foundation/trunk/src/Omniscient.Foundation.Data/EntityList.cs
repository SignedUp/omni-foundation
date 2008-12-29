using System.Collections.Generic;

namespace Omniscient.Foundation.Data
{
    /// <summary>
    /// Generic entity list.
    /// </summary>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    public class EntityList<TEntity> : List<TEntity>, IEntityList<TEntity>
        where TEntity : IEntity
    {
        /// <summary>
        /// Ctor.  Sets Status to <see cref="EntityStatus.NotLoadedYet"/>
        /// </summary>
        public EntityList()
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

    public class EntityList : List<IEntity>, IEntityList
    {
        public EntityList()
        {
            Status = EntityStatus.NotLoadedYet;
        }

        public EntityStatus Status
        {
            get;
            set;
        }
    }

}
