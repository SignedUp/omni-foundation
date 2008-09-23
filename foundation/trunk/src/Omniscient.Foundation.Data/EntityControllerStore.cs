using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omniscient.Foundation.Data
{
    /// <summary>
    /// Default implementation for <see cref="IEntityControllerStore"/>.
    /// </summary>
    [Obsolete("Use ObjectContainer instead.")]
    public class EntityControllerStore: IEntityControllerStore
    {
        private Dictionary<Type, object> _controllers;

        /// <summary>
        /// Ctor.
        /// </summary>
        public EntityControllerStore()
        {
            _controllers = new Dictionary<Type, object>();
        }

        #region IEntityControllerStore Members

        /// <summary>
        /// Registers an instance of an entity controller.
        /// </summary>
        /// <typeparam name="TEntity">The type of entity controlled by <paramref name="controller"/>.</typeparam>
        /// <param name="controller">An instance of an entity controller.</param>
        public void Register<TEntity>(IEntityController<TEntity> controller) 
            where TEntity : IEntity, new()
        {
            _controllers.Add(typeof(TEntity), controller);
        }

        /// <summary>
        /// Gets an instance of an entity controller.
        /// </summary>
        /// <typeparam name="TEntity">The type of entity to get a controller for.</typeparam>
        /// <returns>An entity controller if one exists in the store; otherwise, returns null.</returns>
        public IEntityController<TEntity> Get<TEntity>() 
            where TEntity : IEntity, new()
        {
            if (!_controllers.ContainsKey(typeof(TEntity))) return null;
            return (IEntityController<TEntity>)_controllers[typeof(TEntity)];
        }

        #endregion
    }
}
