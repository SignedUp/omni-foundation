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
        /// Gets the type of the entity, a string that uniquelly identifies this type of entity.
        /// </summary>
        string Type { get; }
        
        /// <summary>
        /// Copies the values of the entity to another entity.  By default, all properties marked with <c>EntityPropertyAttribute</c>
        /// and <c>EntityPropertyType.Value</c> will be copied.  If <paramref name="copyReferences"/> is <c>true</c>, then by default
        /// all properties marked with <c>EntityPropertyAttribute</c> and <c>EntityPropertyType.Reference</c> or 
        /// <c>EntityPropertyType.ReferenceList</c> will be copied as well.  Note that this is only a pointer copy, not a deep copy of 
        /// the reference.
        /// </summary>
        /// <param name="copyReferences"><c>true</c> to copy references; Otherwise, <c>false</c>.</param>
        /// <param name="target">The entity to copy values to.</param>
        void CopyTo(IEntity target, bool copyReferences);
    }
}
