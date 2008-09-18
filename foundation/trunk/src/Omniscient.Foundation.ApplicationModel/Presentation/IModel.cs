using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Omniscient.Foundation.Data;

namespace Omniscient.Foundation.ApplicationModel.Presentation
{
    public interface IModel
    {
        string Name { get; }
        IEntity RootEntity { get; set; }
        
        /// <summary>
        /// Returns true if the Model has an entity with given Id in its entity graph.
        /// </summary>
        /// <param name="id">Id of the entity sought.</param>
        /// <returns>True if the Model has the entity in its entity graph.</returns>
        bool HasEntity(Guid id);
    }
}
