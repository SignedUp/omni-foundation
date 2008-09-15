using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omniscient.Foundation.Data
{
    /// <summary>
    /// Generic entity controller.
    /// </summary>
    /// <typeparam name="TEntity">The type of entities being managed by this controller.</typeparam>
    public class EntityController<TEntity>: IEntityController<TEntity>
        where TEntity: IEntity, new()
    {
        private Dictionary<Guid, TEntity> _clones;

        /// <summary>
        /// Ctor.
        /// </summary>
        public EntityController()
        {
            _clones = new Dictionary<Guid, TEntity>();
        }

        /// <summary>
        /// Starts editing the entity.  Sets the entity status to <see cref="EntityStatus.Dirty"/> 
        /// and prepares a clone for an eventual roll-back.
        /// </summary>
        /// <param name="entity">The entity to edit.</param>
        public void BeginChanges(TEntity entity)
        {
            TEntity clone;
            if (entity.Status != EntityStatus.New && entity.Status != EntityStatus.Clean)
                ThrowInvalidStatus(entity.Status);

            switch (entity.Status)
            {
                case EntityStatus.New:
                    //don't change the status.  Still a new object.
                    break;
                case EntityStatus.Clean:
                    entity.Status = EntityStatus.Dirty;
                    break;
                default:
                    ThrowInvalidStatus(entity.Status);
                    break;
            }

            clone = this.Clone(entity);
            _clones.Add(clone.Id, clone);
        }

        /// <summary>
        /// Cancels entity editing.  Takes the entity back to <see cref="EntityStatus.Clean"/>, and reset original values.
        /// Also cancels the deletion of an entity.
        /// </summary>
        /// <param name="entity">The entity whose status is <see cref="EntityStatus.Dirty"/> or <see cref="EntityStatus.ToBeDeleted"/>.</param>
        public void CancelChanges(TEntity entity)
        {
            switch (entity.Status)
            {
                case EntityStatus.Dirty:
                    RevertChanges(entity);
                    break;
                case EntityStatus.ToBeDeleted:
                    //do nothing; there are only two ways of being ToBeDeleted; coming from Clean (no revert to do)
                    //or coming from Dirty, in which case the rollback is already made.
                    break;
                default:
                    throw new InvalidOperationForStatusException(entity.Status);
            }
            entity.Status = EntityStatus.Clean;
        }

        /// <summary>
        /// Saves the entity to the database.  If Status is <see cref="EntityStatus.Dirty"/>, then the status is set to <see cref="EntityStatus.Clean"/>.
        /// If the status is <see cref="EntityStatus.ToBeDeleted"/>, the status is set to <see cref="EntityStatus.NonExistent"/>.
        /// </summary>
        /// <param name="entity">The entity to save.</param>
        public void AcceptChanges(TEntity entity)
        {
            if (entity.Status != EntityStatus.New && entity.Status != EntityStatus.Dirty && entity.Status != EntityStatus.ToBeDeleted) return;
            if (this.Adapter == null) throw new InvalidOperationException("Adapter is null.");
            Adapter.Save(entity);
            switch (entity.Status)
            {
                case EntityStatus.New:
                case EntityStatus.Dirty:
                    entity.Status = EntityStatus.Clean;
                    break;
                case EntityStatus.ToBeDeleted:
                    entity.Status = EntityStatus.NonExistent;
                    break;
                default:
                    throw new InvalidOperationForStatusException(entity.Status);
            }
        }

        /// <summary>
        /// Marks an entity to be deleted at the next call to <see cref="AcceptChanges"/>.  If the entity's Status is <see cref="EntityStatus.Dirty"/>,
        /// then the entity is reset with original values, so that there's no ambiguity when calling <see cref="CancelChanges"/>.
        /// </summary>
        /// <param name="entity"></param>
        public void MarkAsDeleted(TEntity entity)
        {
            switch (entity.Status)
            {
                case EntityStatus.New:
                    entity.Status = EntityStatus.NonExistent;
                    break;
                case EntityStatus.Clean:
                    entity.Status = EntityStatus.ToBeDeleted;
                    break;
                case EntityStatus.Dirty:
                    //start by rollbacking changes on the entity (before being Dirty, it was Clean)
                    //otherwise, there would be an ambiguity when calling CancelChanges.
                    RevertChanges(entity);
                    entity.Status = EntityStatus.ToBeDeleted;
                    break;
                default:
                    ThrowInvalidStatus(entity.Status);
                    break;
            }
        }

        private void RevertChanges(TEntity entity)
        {
            TEntity clone;
            clone = _clones[entity.Id];
            clone.CopyValues(entity);
            _clones.Remove(entity.Id);
        }

        private void ThrowInvalidStatus(EntityStatus status)
        {
            throw new InvalidOperationForStatusException(status);
        }

        /// <summary>
        /// Retrieves the entity with given Id.
        /// </summary>
        /// <param name="id">The Id to search for.</param>
        /// <returns>The entity having this Id, or null if no entity is found.</returns>
        public TEntity Fetch(Guid id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Retrieves a set of entities filtered by the given object query.
        /// </summary>
        /// <param name="query">The object query to filter the entities.</param>
        /// <returns>A set of entities (may be empty)</returns>
        public TEntity[] Fetch(Omniscient.Foundation.Data.ObjectQuery.OQuery<TEntity> query)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Clones the entity.  The result is an entity with the same Id, same values, and status set to <see cref="EntityStatus.Clone"/>.
        /// </summary>
        /// <param name="entity">The entity to clone.</param>
        /// <returns>A clone, with the same id and values.</returns>
        public TEntity Clone(TEntity entity)
        {
            TEntity clone;
            clone = new TEntity();
            clone.Status = EntityStatus.Clone;
            clone.Id = entity.Id;
            entity.CopyValues(clone);

            return clone;
        }

        /// <summary>
        /// Gets or sets the <see cref="IEntityAdapter{Tentity}"/> used to interact with the database.
        /// </summary>
        public IEntityAdapter<TEntity> Adapter
        {
            get;
            set;
        }
    }
}
