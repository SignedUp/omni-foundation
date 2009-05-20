using System;
using Omniscient.Foundation.Data;
using System.Collections.Generic;

namespace Omniscient.Foundation.ApplicationModel.Presentation
{
    /// <summary>
    /// Base class for models.
    /// </summary>
    public abstract class ModelBase<TEntity>: IModel<TEntity>
        where TEntity: IEntity, new()
    {
        private TEntity _clone;
        private List<string> _changedProperties;

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged = delegate { };

        public ModelBase(TEntity entity)
        {
            Entity = entity;
        }

        TEntity IModel<TEntity>.Entity
        {
            get { return (TEntity)Entity; }
        }

        public IEntity Entity
        {
            get;
            private set;
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
            _changedProperties = new List<string>();
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
                _changedProperties.ForEach(propertyName => RaisePropertyChanged(propertyName));
            }
            else
            {
                OnRecopyOriginalValues();
            }
            _clone = default(TEntity);
        }

        protected virtual void OnValueChanged(string propertyName)
        {
            if (IsBeingEdited)
            {
                _changedProperties.Add(propertyName);
            }
            else
            {
                RaisePropertyChanged(propertyName);
            }
        }

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
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
