using System;

namespace Omniscient.Foundation.Data
{
    /// <summary>
    /// Represents an entity.
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Gets or sets the status of the entity.
        /// </summary>
        EntityStatus Status { get; set; }
        
        /// <summary>
        /// Copies the values of the entity to another entity.  Copies on the data values, skipping the Id and Status.
        /// </summary>
        /// <param name="target">The entity to copy values to.</param>
        void CopyTo(IEntity target);
    }

    /// <summary>
    /// Generic IEntity interface of specify types
    /// </summary>
    /// <typeparam name="TId">
    /// The primary Key type of your entity 
    /// </typeparam>
    public interface IEntity<TId> : IEntity
    {
        /// <summary>
        /// Gets an id that uniquely represents an entity in a given space.  For example, and IEntity&lt;Guid&gt;
        /// will be unique in the universe, while an IEntity&lt;long&gt; will probably be unique amongst other
        /// entities of the same type.
        /// </summary>
        TId Id { get; }
    }
}
