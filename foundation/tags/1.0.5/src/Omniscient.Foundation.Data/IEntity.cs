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
        /// The Id is the surrogate key of the entity in the database.
        /// </summary>
        Guid Id { get; }
        
        /// <summary>
        /// Copies the values of the entity to another entity.  Copies on the data values, skipping the Id and Status.
        /// </summary>
        /// <param name="target">The entity to copy values to.</param>
        void CopyTo(IEntity target);
    }
}
