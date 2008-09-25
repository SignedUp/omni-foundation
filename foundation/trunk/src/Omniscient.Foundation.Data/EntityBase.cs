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
        /// Creates an entity with the status <see cref="EntityStatus.New"/>, and a brand new Id.
        /// </summary>
        public EntityBase()
        {
            this.Status = EntityStatus.New;
            _id = Guid.NewGuid();
        }

        /// <summary>
        /// Creates an existing Entity.  The <paramref name="id"/> must exist in the database for that Entity.  
        /// Depending on the value of <paramref name="entityIsLoaded"/>, the status is either set to
        /// <see cref="EntityStatus.Clean"/> (if the entity is being loaded with all its values), or 
        /// <see cref="EntityStatus.NotLoadedYet"/> (if the entity is not being loaded with its values)
        /// </summary>
        /// <param name="entityIsLoaded">Indicates whether the entity is being loaded with all its value, or is just assigned an Id for future load.</param>
        /// <param name="id">Id must correspond to what's in the database.  Entity is assigned status <see cref="EntityStatus.Clean"/> or <see cref="EntityStatus.NotLoadedYet"/>.</param>
        public EntityBase(Guid id, bool entityIsLoaded)
        {
            this.Status = entityIsLoaded? EntityStatus.Clean : EntityStatus.NotLoadedYet;
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

        /// <summary>
        /// Clones the entity.  The result is an entity with the same Id, same values, and status set to <see cref="EntityStatus.Clone"/>.
        /// </summary>
        /// <returns>A clone, with the same id and values.</returns>
        public IEntity Clone()
        {
            IEntity cl;
            cl = (IEntity)Activator.CreateInstance(this.GetType(), new object[] { this.Id, true });
            this.CopyValues(cl);
            cl.Status = EntityStatus.Clone;
            return cl;
        }
    }
}
