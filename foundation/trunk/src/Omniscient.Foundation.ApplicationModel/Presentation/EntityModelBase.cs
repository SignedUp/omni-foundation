using System;
using Omniscient.Foundation.Data;
using System.Collections.Generic;
using Omniscient.Foundation.Patterns;

namespace Omniscient.Foundation.ApplicationModel.Presentation
{
    /// <summary>
    /// Base class for models.
    /// </summary>
    public abstract class EntityModelBase<TEntity>: IEntityModel<TEntity>
        where TEntity: IEntity, new()
    {
        private TEntity _clone;

        public EntityModelBase() { }

        public EntityModelBase(TEntity entity)
        {
            Entity = entity;
        }

        public void Wrap(TEntity entity)
        {
            if (Entity != null) throw new InvalidOperationException("An entity is already wrapped.");
            Entity = entity;
        }

        public TEntity Entity
        {
            get;
            private set;
        }

        public TEntity Adapt()
        {
            return default(TEntity);
        }

        public EntityStatus EntityStatus
        {
            get { return Entity.Status; }
            set { Entity.Status = value; }
        }

        public virtual void BeginEdit()
        {
            if (IsBeingEdited) throw new InvalidOperationException("Already in edit mode.");
            if (EntityStatus != EntityStatus.Clean && EntityStatus != EntityStatus.New && EntityStatus != EntityStatus.Dirty)
                throw new InvalidOperationException("Invalid entity status for this operation.");

            _clone = OnGetClone();
        }

        protected virtual TEntity OnGetClone()
        {
            TEntity clone = new TEntity();
            Entity.CopyTo(clone);
            clone.Status = EntityStatus.Clone;
            return clone;
        }

        public virtual void EndEdit(bool acceptChanges)
        {
            if (!IsBeingEdited) throw new InvalidOperationException("Not in edit mode.");

            if (acceptChanges)
            {
                if (EntityStatus == EntityStatus.Clean) Entity.Status = EntityStatus.Dirty;
            }
            else
            {
                OnRecopyOriginalValues();
            }
            _clone = default(TEntity);
        }

        protected virtual void OnRecopyOriginalValues()
        {
            _clone.CopyTo(Entity);
        }

        public bool IsBeingEdited
        {
            get { return (_clone != null); }
        }

        public void MarkAsDeleted()
        {
            if (EntityStatus != EntityStatus.Clean && EntityStatus != EntityStatus.Dirty && EntityStatus != EntityStatus.ToBeDeleted)
                throw new InvalidOperationException("Invalid entity status for this operation.");
            
            EntityStatus = EntityStatus.ToBeDeleted;
        }
    }
}
