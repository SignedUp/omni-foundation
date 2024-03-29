﻿using System;
using System.Collections.Generic;

namespace Omniscient.Foundation.Data
{
    /// <summary>
    /// Generic entity controller.
    /// </summary>
    /// <typeparam name="TEntity">The type of entities being managed by this controller.</typeparam>
    public class EntityController<TEntity>: IEntityController<TEntity>
        where TEntity: IEntity
    {
        private Dictionary<Guid, TEntity> _clones;
        private Dictionary<IEntityModificationObserver<TEntity>, EntityList<TEntity>> _observers;

        /// <summary>
        /// Ctor.
        /// </summary>
        public EntityController()
        {
            _clones = new Dictionary<Guid, TEntity>();
            _observers = new Dictionary<IEntityModificationObserver<TEntity>, EntityList<TEntity>>();
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

            clone = Clone(entity);
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
        /// Saves the entity to the database.  If Status is <see cref="EntityStatus.Dirty"/> or <see cref="EntityStatus.New"/>, then the status is set to <see cref="EntityStatus.Clean"/>.
        /// If the status is <see cref="EntityStatus.ToBeDeleted"/>, the status is set to <see cref="EntityStatus.NonExistent"/>.
        /// </summary>
        /// <param name="entity">The entity to save.</param>
        public void AcceptChanges(TEntity entity)
        {
            if (entity.Status != EntityStatus.New && entity.Status != EntityStatus.Dirty && entity.Status != EntityStatus.ToBeDeleted) return;
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
            if (_clones.ContainsKey(entity.Id)) _clones.Remove(entity.Id);

            foreach(IEntityModificationObserver<TEntity> observer in _observers.Keys)
            {
                if(_observers[observer].Contains(entity))
                {
                    observer.Notify(entity);
                }
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
            clone.CopyTo(entity);
            _clones.Remove(entity.Id);
        }

        private static void ThrowInvalidStatus(EntityStatus status)
        {
            throw new InvalidOperationForStatusException(status);
        }

        /// <summary>
        /// Clones the entity.  The result is an entity with the same Id, same values, and status set to <see cref="EntityStatus.Clone"/>.
        /// </summary>
        /// <param name="original">The entity to clone</param>
        /// <returns>A clone, with the same id and values.</returns>
        public TEntity Clone(TEntity original)
        {
            TEntity clone;

            clone = (TEntity)Activator.CreateInstance(typeof(TEntity), new object[] {original.Id, true});
            original.CopyTo(clone);
            clone.Status = EntityStatus.Clone;
            return clone;
        }

        public void RegisterObserver(IEntityModificationObserver<TEntity> observer, EntityList<TEntity> list)
        {
            if(_observers.ContainsKey(observer))
            {
                throw new ArgumentException("Observer is already registered.");
            }
            _observers.Add(observer, list);
        }
    }
}
