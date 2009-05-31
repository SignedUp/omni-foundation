using Omniscient.Foundation.Data;
using Omniscient.Foundation.ApplicationModel;

namespace Omniscient.Foundation.ApplicationModel.Presentation
{
    /// <summary>
    /// Represents a model.  Models are logical wrappers around entities; they define relations between entities
    /// and are responsible for managing the state and status of entities.
    /// </summary>
    public interface IEntityModel : IModel, IObjectWrapper<IEntity>
    {
    }

    /// <summary>
    /// Represents a model.  Models are logical wrappers around entities; they define relations between entities
    /// and are responsible for managing the state and status of entities.
    /// </summary>
    public interface IEntityModel<TEntity> : IModel, IObjectWrapper<TEntity>
        where TEntity : IEntity, new()
    {
    }
}