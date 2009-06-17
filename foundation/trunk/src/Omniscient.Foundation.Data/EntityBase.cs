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
    public class EntityBase : IEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityBase"/> class. 
        /// Creates an entity with the status <see cref="EntityStatus.New"/>, and a brand new Id.
        /// </summary>
        public EntityBase()
        {
            Status = EntityStatus.New;
        }

        /// <summary>
        /// Gets or sets the Status.
        /// </summary>
        [DataMember]
        [SystemProperty]
        public EntityStatus Status
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
                if (p.Name != "Status" && p.GetSetMethod() != null)
                    p.SetValue(target, p.GetValue(this, null), null);
            }
        }

        /// <summary>
        /// Returns a string representation of the Entity.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("Entity type:{0} status:{1}", this.GetType().Name, Status);
        }
    }
}
