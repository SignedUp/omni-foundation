using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omniscient.Foundation.Data
{
    /// <summary>
    /// Defines a contract for an Entity Controller store.  Stores instances of entity controllers.
    /// </summary>
    [Obsolete("Use ObjectContainer instead.")]
    public interface IEntityControllerStore
    {
        /// <summary>
        /// Registers an instance of an entity controller.
        /// </summary>
        /// <typeparam name="TEntity">The type of entity controlled by <paramref name="controller"/>.</typeparam>
        /// <param name="controller">An instance of an entity controller.</param>
        void Register<TEntity>(IEntityController<TEntity> controller) where TEntity: IEntity, new();
        
        /// <summary>
        /// Gets an instance of an entity controller.
        /// </summary>
        /// <typeparam name="TEntity">The type of entity to get a controller for.</typeparam>
        /// <returns>An entity controller if one exists in the store; otherwise, returns null.</returns>
        IEntityController<TEntity> Get<TEntity>() where TEntity: IEntity, new();
    }
}
