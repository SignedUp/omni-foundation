using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omniscient.Framework.Data
{
    
    /// <summary>
    /// Represents an adapter that manages a certain type of entities.  
    /// Responsible for connecting to the database, and executing queries.
    /// </summary>
    /// <typeparam name="TEntity">The type of entities to manage.</typeparam>
    public interface IEntityAdapter<TEntity> where TEntity: IEntity, new()
    {
        /// <summary>
        /// Retrieves an entity from the database by unique id.
        /// </summary>
        /// <param name="id">The unique id of the entity that is sought.</param>
        /// <returns>Returns the entity if found; Otherwise, returns Null.</returns>
        TEntity Fetch(Guid id);
        
        /// <summary>
        /// Retrieves an entity from the database by an object query.
        /// </summary>
        /// <param name="query">The object query to search entities.</param>
        /// <returns>An array of entities that are found using the given object query.</returns>
        TEntity[] Fetch(ObjectQuery.OQuery<TEntity> query);
        
        /// <summary>
        /// Saves the entity.  Executes different queries based on the <see cref="EntityStatus"/> of the entity.
        /// </summary>
        /// <param name="entity">The entity to save.</param>
        void Save(TEntity entity);
    }
}
