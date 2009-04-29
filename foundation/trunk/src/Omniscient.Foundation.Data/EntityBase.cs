using System;
using System.Reflection;
using System.Runtime.Serialization;

namespace Omniscient.Foundation.Data
{
    /// <summary>
    /// Base class for entities.
    /// </summary>
    [DataContract]
#if (!SILVERLIGHT)
    [Serializable]
#endif
    public class EntityBase: IEntity
    {
        /// <summary>
        /// Creates an entity with the status <see cref="EntityStatus.New"/>, and a brand new Id.
        /// </summary>
        public EntityBase()
        {
            Status = EntityStatus.New;
            Id = Guid.NewGuid();
        }

        /// <summary>
        /// Creates an existing Entity.  The <paramref name="id"/> must exist in the database for that Entity.  
        /// </summary>
        /// <param name="id">Id must correspond to what's in the database.  Entity is assigned status <see cref="EntityStatus.Clean"/>.</param>
        public EntityBase(Guid id)
        {
            Status = EntityStatus.Clean;
            Id = id;
        }

        /// <summary>
        /// Gets or sets the Status.
        /// </summary>
        [DataMember]
        public EntityStatus Status
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Id.  Setting the id is possible only when Status == <see cref="EntityStatus.NotLoadedYet" />.
        /// </summary>
        [DataMember]
        public Guid Id
        {
            get;
            set;
        }

        /// <summary>
        /// Copies the values of the entity to another entity.  Copies on the data values, skipping the Id and Status.
        /// </summary>
        /// <param name="target">The entity to copy values to.</param>
        public virtual void CopyTo(IEntity target)
        {
            foreach (PropertyInfo p in this.GetType().GetProperties())
            {
                if (p.Name != "Id" && p.Name != "Status")
                    p.SetValue(target, p.GetValue(this, null), null);
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
            return Id.Equals(comp.Id);
        }

        /// <summary>
        /// Computes an hash code based on the ID of the Entity.
        /// </summary>
        /// <returns>An hash code that can be used in hashtables and dictionaries.</returns>
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        /// <summary>
        /// Returns a string representation of the Entity.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("Entity type:{0} id:{1} status:{2}", this.GetType().Name, Id, Status);
        }
    }
}
