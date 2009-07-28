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
        /// Creates an entity with the status <see cref="EntityStatus.New"/>.
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
        /// Copies the values of the entity to another entity.  Copies the data values, except the <see cref="Status"/>.
        /// </summary>
        /// <param name="target">The entity to copy values to.</param>
        public virtual void CopyTo(IEntity target)
        {
            foreach (PropertyInfo p in this.GetType().GetProperties())
            {
                if (p.Name != "Status" && p.GetSetMethod() != null)
                {
                    p.SetValue(target, p.GetValue(this, null), null);
                }
            }
        }

        /// <summary>
        /// Returns a string representation of the Entity.
        /// </summary>
        /// <returns>
        /// A string representation of the Entity.
        /// </returns>
        public override string ToString()
        {
            return string.Format("Entity type:{0} status:{1}", this.GetType().Name, Status);
        }
    }

    /// <summary>
    /// Provides a base class for entities which have an Id.  Adds a constructor to <see cref="EntityBase"/> that
    /// sets the <see cref="IEntity.Status"/> value to <see cref="EntityStatus.Clean"/>.
    /// </summary>
    /// <typeparam name="TKey">The type used for the <see cref="IEntity{TId}.Id"/> property.</typeparam>
    public class EntityBase<TKey> : EntityBase, IEntity<TKey>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityBase"/> class. Puts the <see cref="IEntity.Status"/> 
        /// value to <see cref="EntityStatus.New"/> (delegates to base class' ctor).
        /// </summary>
        public EntityBase()
            : this(default(TKey))
        {
            this.Status = EntityStatus.New;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityBase"/> class. Puts the <see cref="IEntity.Status"/> 
        /// value to <see cref="EntityStatus.Clean"/>
        /// </summary>
        /// <param name="id">
        /// The id of the entity.  With this id, the entity is considered being loaded from a data store and thus be <see cref="EntityStatus.Clean"/>.
        /// </param>
        public EntityBase(TKey id)
        {
            this.Status = EntityStatus.Clean;
            this.Id = id;
        }

        /// <summary>
        /// Gets an id that uniquely represents an entity in a given space.  For example, and <see cref="IEntity{Guid}"/>
        /// will be unique in the universe, while an <see cref="IEntity{Long}"/> will probably be unique amongst other
        /// entities of the same type.
        /// </summary>
        public TKey Id
        {
            get; private set;
        }

        /// <summary>
        /// Copies the values of the entity to another entity.  Copies the data values, except <see cref="IEntity.Status"/>
        /// and <see cref="Id"/>.
        /// </summary>
        /// <param name="target">The entity to copy values to.</param>
        public override void CopyTo(IEntity target)
        {
            foreach (PropertyInfo p in this.GetType().GetProperties())
            {
                if (p.Name != "Status" && p.Name != "Id" && p.GetSetMethod() != null)
                {
                    p.SetValue(target, p.GetValue(this, null), null);
                }
            }
        }
    }

}
