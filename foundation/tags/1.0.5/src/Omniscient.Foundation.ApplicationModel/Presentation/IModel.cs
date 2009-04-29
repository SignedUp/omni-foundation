using System;
using Omniscient.Foundation.Data;

namespace Omniscient.Foundation.ApplicationModel.Presentation
{
    /// <summary>
    /// Represents a model.  Models are logical wrappers around entities; they define a boundary around the object graph
    /// of the underlying entity.
    /// </summary>
    public interface IModel
    {
        /// <summary>
        /// Gets the name of the model.
        /// </summary>
        string Name { get; }
        
        /// <summary>
        /// Returns true if the Model has an entity with given Id in its entity graph.
        /// </summary>
        /// <param name="id">Id of the entity sought.</param>
        /// <returns>True if the Model has the entity in its entity graph.</returns>
        bool HasEntity(Guid id);

        /// <summary>
        /// Returns true if the model contains an entity that needs to be saved.  The model will not dig the graph
        /// more than required by the boundary that it represents (e.g. a "Client" model will not check for client's addresses, 
        /// while a "ClientAddress" model would.
        /// </summary>
        /// <returns></returns>
        bool ContainsEntitiesThatNeedToBeSaved();

        /// <summary>
        /// Returns a child entity that has the given Id.
        /// </summary>
        /// <param name="id">The id of the entity sought.</param>
        /// <returns>The entity, or null if no entity is found.</returns>
        IEntity GetEntity(Guid id);
    }
}
