using System;
using System.Reflection;
using System.Runtime.Serialization;

namespace Omniscient.Foundation.Data
{
    /// <summary>
    /// Base class for unique entities.
    /// </summary>
    [DataContract]
#if (!SILVERLIGHT)
    [Serializable]
#endif
    public class UniqueEntityBase : EntityBase, IEntity<Guid>
    {
        /// <summary>
        /// Creates an entity with the status <see cref="EntityStatus.New"/>, and a brand new Id.
        /// </summary>
        public UniqueEntityBase()
        {
            Id = Guid.NewGuid();
        }

        /// <summary>
        /// Creates an existing Entity.  The <paramref name="id"/> must exist in the database for that Entity.  
        /// </summary>
        /// <param name="id">Id must correspond to what's in the database.  Entity is assigned status <see cref="EntityStatus.Clean"/>.</param>
        public UniqueEntityBase(Guid id)
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
        public override void CopyTo(IEntity target)
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
            UniqueEntityBase comp;
            comp = obj as UniqueEntityBase;
            if (comp == null) return false;
            return Id.Equals(comp.Id);
        }

        /// <summary>
        /// Compares two entities based on their Id.  Returns true if both Id are equal.
        /// </summary>
        /// <param name="left">The first entity to compare.</param>
        /// <param name="right">The second entity to compare.</param>
        /// <returns>True if the entities' Id are equal.  Otherwise, false.</returns>
        public static bool operator ==(UniqueEntityBase left, UniqueEntityBase right)
        {
            return (left.Id == right.Id);
        }

        /// <summary>
        /// Compares two entities based on their Id.  Returns true if both Id are different.
        /// </summary>
        /// <param name="left">The first entity to compare.</param>
        /// <param name="right">The second entity to compare.</param>
        /// <returns>True if the entities' Id are different.  Otherwise, false.</returns>
        public static bool operator !=(UniqueEntityBase left, UniqueEntityBase right)
        {
            return (left.Id != right.Id);
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
