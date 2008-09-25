using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        /// Gets the type of the entity, a string that uniquelly identifies this type of entity.
        /// </summary>
        string Type { get; }
        
        /// <summary>
        /// Copies the values of the entity to another entity.
        /// </summary>
        /// <param name="target">The entity to copy values to.</param>
        void CopyValues(IEntity target);

        /// <summary>
        /// Clones the entity.  The result is an entity with the same Id, same values, and status set to <see cref="EntityStatus.Clone"/>.
        /// </summary>
        /// <returns>A clone, with the same id and values.</returns>
        IEntity Clone();
    }
}
