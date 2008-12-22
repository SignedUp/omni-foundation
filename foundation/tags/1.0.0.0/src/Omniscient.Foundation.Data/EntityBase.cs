using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Runtime.Serialization;

namespace Omniscient.Foundation.Data
{
    /// <summary>
    /// Base class for entities.
    /// </summary>
    [DataContract()]
    [Serializable()]
    public class EntityBase: IEntity
    {
        /// <summary>
        /// Creates an entity with the status <see cref="EntityStatus.New"/>, and a brand new Id.
        /// </summary>
        public EntityBase()
        {
            this.Status = EntityStatus.New;
            Id = Guid.NewGuid();
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
            Id = id;
        }

        /// <summary>
        /// Gets or sets the Status.
        /// </summary>
        [DataMember()]
        public EntityStatus Status
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Id.  Setting the id is possible only when Status == <see cref="EntityStatus.NotLoadedYet" />.
        /// </summary>
        [DataMember()]
        public Guid Id
        {
            get;
            set;
        }

        /// <summary>
        /// Returns a string that uniquely identifies the type of the Entity.  By default, this is the name of the class.  Can be overriden.
        /// </summary>
        public virtual string Type
        {
            get { return this.GetType().Name; }
        }

        /// <summary>
        /// Copies the values of the entity to another entity.  By default, all properties marked with <c>EntityPropertyAttribute</c>
        /// and <c>EntityPropertyType.Value</c> will be copied.  If <paramref name="copyReferences"/> is <c>true</c>, then by default
        /// all properties marked with <c>EntityPropertyAttribute</c> and <c>EntityPropertyType.Reference</c> or 
        /// <c>EntityPropertyType.ReferenceList</c> will be copied as well.  Note that this is only a pointer copy, not a deep copy of 
        /// the reference.
        /// </summary>
        /// <param name="copyReferences"><c>true</c> to copy references; Otherwise, <c>false</c>.</param>
        /// <param name="target">The entity to copy values to.</param>
        public virtual void CopyTo(IEntity target, bool copyReferences)
        {
            this.CopyInternal(target, copyReferences);
        }

        private void CopyInternal(IEntity target, bool copyReferences)
        {
            foreach (PropertyInfo p in this.GetType().GetProperties())
            {
                object[] attributes;
                attributes = p.GetCustomAttributes(typeof(EntityPropertyAttribute), true);
                if (attributes.Length == 0) continue;
                EntityPropertyAttribute att = (EntityPropertyAttribute) attributes[0];
                if (att.Type == EntityPropertyType.Value || copyReferences)
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
