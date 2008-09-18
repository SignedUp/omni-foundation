using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Omniscient.Foundation.Data
{
    /// <summary>
    /// Base class for entities.
    /// </summary>
    public class EntityBase: IEntity
    {
        private Guid _id;

        /// <summary>
        /// Creates an entity with the status <see cref="EntityStatus.New"/>, and a brand new Guid.
        /// </summary>
        public EntityBase() : this(EntityStatus.New) { }

        /// <summary>
        /// Creates an entity with specific status.  Status may be either <see cref="EntityStatus.New"/>, 
        /// <see cref="EntityStatus.NotLoadedYet"/> or <see cref="EntityStatus.Clone"/>.  If status is New,
        /// then a new Guid is assigned to the Id.  Otherwise, an empty Guid is used.
        /// </summary>
        /// <param name="status">The status of the new entity.  Either <see cref="EntityStatus.New"/>, 
        /// <see cref="EntityStatus.NotLoadedYet"/> or <see cref="EntityStatus.Clone"/>.  If status is New,
        /// then a new Guid is assigned to the Id.  Otherwise, an empty Guid is used.</param>
        public EntityBase(EntityStatus status)
        {
            if (status != EntityStatus.New && status != EntityStatus.NotLoadedYet && status != EntityStatus.Clone)
                throw new InvalidOperationForStatusException(status);

            this.Status = status;
            if (status == EntityStatus.New) _id = Guid.NewGuid();
            else _id = Guid.Empty;
        }

        /// <summary>
        /// Creates an existing Entity.  The <paramref name="id"/> must exist in the database for that Entity.  
        /// Status is set to <see cref="EntityStatus.Clean"/>.
        /// </summary>
        /// <param name="id">Id must correspond to what's in the database.  Entity is assigned status <see cref="EntityStatus.Clean"/>.</param>
        public EntityBase(Guid id)
        {
            this.Status = EntityStatus.Clean;
            _id = id;
        }

        /// <summary>
        /// Gets or sets the Status.
        /// </summary>
        public EntityStatus Status
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Id.  Setting the id is possible only when Status == <see cref="EntityStatus.NotLoadedYet" />.
        /// </summary>
        public Guid Id
        {
            get { return _id; }
            set
            {
                if (this.Status != EntityStatus.NotLoadedYet && this.Status != EntityStatus.Clone) throw new InvalidOperationForStatusException(this.Status);
                _id = value;
            }
        }

        /// <summary>
        /// Returns a string that uniquely identifies the type of the Entity.  By default, this is the name of the class.  Can be overriden.
        /// </summary>
        public virtual string Type
        {
            get { return this.GetType().Name; }
        }

        /// <summary>
        /// Copies the value properties to a target entity.  By default, this is done automatically by assigning all properties
        /// marked with attribute EntityProperty, if the type of this attribute is <see cref="EntityPropertyType.Value"/>.
        /// </summary>
        /// <param name="target">The Entity to copy values to.</param>
        public virtual void CopyValues(IEntity target)
        {
            this.CopyInternal(target);
        }

        private void CopyInternal(IEntity target)
        {
            foreach (PropertyInfo p in this.GetType().GetProperties())
            {
                object[] att;
                att = p.GetCustomAttributes(typeof(EntityPropertyAttribute), true);
                if (att.Length > 0 && ((EntityPropertyAttribute)att[0]).Type == EntityPropertyType.Value)
                {
                    p.SetValue(target, p.GetValue(this, null), null);
                }
            }
        }

        /// <summary>
        /// Compares two Entities based on their IDs.  Same as operator ==.
        /// </summary>
        /// <param name="obj">The Entity to compare with.</param>
        /// <returns>Returns True if the two entities have the same ID.</returns>
        public override bool Equals(object obj)
        {
            EntityBase comp;
            comp = obj as EntityBase;
            if (comp == null) return false;
            return this.Id.Equals(comp.Id);
        }

        /// <summary>
        /// Computes an hash code based on the ID of the Entity.
        /// </summary>
        /// <returns>An hash code that can be used in hashtables and dictionaries.</returns>
        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        /// <summary>
        /// Returns a string representation of the Entity.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("Entity type:{0} id:{1} status:{2}", this.Type, this.Id, this.Status);
        }        
    }
}
