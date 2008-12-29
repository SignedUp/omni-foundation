using System.ServiceModel;

namespace Omniscient.Foundation.Data
{
    /// <summary>
    /// Represents a class that manages a certain type of entities.
    /// </summary>
    /// <typeparam name="TEntity">The type of Entity to manage.</typeparam>
    [ServiceContract]
    public interface IEntityController<TEntity> where TEntity: IEntity
    {
        /// <summary>
        /// Starts editing the entity.  Sets the entity status to <see cref="EntityStatus.Dirty"/> 
        /// and prepares a clone for an eventual roll-back.
        /// </summary>
        /// <param name="entity">The entity to edit.</param>
        [OperationContract]
        void BeginChanges(TEntity entity);
        
        /// <summary>
        /// Cancels entity editing.  Takes the entity back to <see cref="EntityStatus.Clean"/>, and reset original values.
        /// Also cancels the deletion of an entity.
        /// </summary>
        /// <param name="entity">The entity whose status is <see cref="EntityStatus.Dirty"/> or <see cref="EntityStatus.ToBeDeleted"/>.</param>
        [OperationContract]
        void CancelChanges(TEntity entity);
        
        /// <summary>
        /// Use this fonction to accept changes on an entity.  Use <see cref="IEntityAdapter{TEntity}"/> to save it to the database.
        /// </summary>
        /// <remarks>
        /// If Status is <see cref="EntityStatus.Dirty"/>, then the status is set to <see cref="EntityStatus.Clean"/>.
        /// If the status is <see cref="EntityStatus.ToBeDeleted"/>, the status is set to <see cref="EntityStatus.NonExistent"/>.
        /// </remarks>
        /// <param name="entity">The entity to save.</param>
        [OperationContract]
        void AcceptChanges(TEntity entity);
        
        /// <summary>
        /// Marks an entity to be deleted at the next call to <see cref="AcceptChanges"/>.  If the entity's Status is <see cref="EntityStatus.Dirty"/>,
        /// then the entity is reset with original values, so that there's no ambiguity when calling <see cref="CancelChanges"/>.
        /// </summary>
        /// <param name="entity"></param>
        [OperationContract]
        void MarkAsDeleted(TEntity entity);

        /// <summary>
        /// Clones the entity.  The result is an entity with the same Id, same values, and status set to <see cref="EntityStatus.Clone"/>.
        /// See <see cref="IEntity.CopyTo"/> for details about <paramref name="copyReferences"/>.
        /// </summary>
        /// <param name="original">The entity to clone</param>
        /// <param name="copyReferences"><c>true</c> to copy references; Otherwise, <c>false</c>.</param>
        /// <returns>A clone, with the same id and values.</returns>
        [OperationContract]
        TEntity Clone(TEntity original, bool copyReferences);

    }
}
