using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omniscient.Foundation.Data
{
    /// <summary>
    /// Represents a class that manages a certain type of entities.
    /// </summary>
    /// <typeparam name="TEntity">The type of Entity to manage.</typeparam>
    public interface IEntityController<TEntity> where TEntity: IEntity
    {
        /// <summary>
        /// Retrieves the entity with given Id.
        /// </summary>
        /// <param name="id">The Id to search for.</param>
        /// <returns>The entity having this Id, or null if no entity is found.</returns>
        TEntity Fetch(Guid id);


        /// <summary>
        /// Retrieves a set of entities filtered by the given object query.
        /// </summary>
        /// <param name="query">The object query to filter the entities.</param>
        /// <returns>A set of entities (may be empty)</returns>
        EntityList<TEntity> Fetch(ObjectQuery.OQuery<TEntity> query);

        /// <summary>
        /// Starts editing the entity.  Sets the entity status to <see cref="EntityStatus.Dirty"/> 
        /// and prepares a clone for an eventual roll-back.
        /// </summary>
        /// <param name="entity">The entity to edit.</param>
        void BeginChanges(TEntity entity);
        
        /// <summary>
        /// Cancels entity editing.  Takes the entity back to <see cref="EntityStatus.Clean"/>, and reset original values.
        /// Also cancels the deletion of an entity.
        /// </summary>
        /// <param name="entity">The entity whose status is <see cref="EntityStatus.Dirty"/> or <see cref="EntityStatus.ToBeDeleted"/>.</param>
        void CancelChanges(TEntity entity);
        
        /// <summary>
        /// Saves the entity to the database.  If Status is <see cref="EntityStatus.Dirty"/>, then the status is set to <see cref="EntityStatus.Clean"/>.
        /// If the status is <see cref="EntityStatus.ToBeDeleted"/>, the status is set to <see cref="EntityStatus.NonExistent"/>.
        /// </summary>
        /// <param name="entity">The entity to save.</param>
        void AcceptChanges(TEntity entity);
        
        /// <summary>
        /// Marks an entity to be deleted at the next call to <see cref="AcceptChanges"/>.  If the entity's Status is <see cref="EntityStatus.Dirty"/>,
        /// then the entity is reset with original values, so that there's no ambiguity when calling <see cref="CancelChanges"/>.
        /// </summary>
        /// <param name="entity"></param>
        void MarkAsDeleted(TEntity entity);

        /// <summary>
        /// Gets or sets the <see cref="IEntityAdapter{TEntity}"/> used to interact with the database.
        /// </summary>
        IEntityAdapter<TEntity> Adapter { get; set; }
    }
}
