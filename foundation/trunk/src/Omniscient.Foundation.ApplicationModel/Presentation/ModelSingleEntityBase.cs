using Omniscient.Foundation.Data;

namespace Omniscient.Foundation.ApplicationModel.Presentation
{
    /// <summary>
    /// Base class for models with a single entity as the root.
    /// </summary>
    public abstract class ModelSingleEntityBase<TEntity>: ModelBase
        where TEntity: IEntity
    {
        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="entity">The root entity.</param>
        protected ModelSingleEntityBase(TEntity entity)
        {
            Entity = entity;
        }

        /// <summary>
        /// Gets the root entity.
        /// </summary>
        public TEntity Entity
        {
            get;
            private set;
        }
    }
}
