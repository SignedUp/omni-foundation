using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Omniscient.Foundation.Data;

namespace Omniscient.Foundation.ApplicationModel.Presentation
{
    /// <summary>
    /// Base class for models.
    /// </summary>
    public abstract class ModelBase: IModel
    {
        private readonly string _name;

        /// <summary>
        /// Ctor; sets the name to this type's name
        /// </summary>
        protected ModelBase()
        {
            _name = this.GetType().Name;
        }

        /// <summary>
        /// Gets the name of the model.  Overridable.
        /// </summary>
        public virtual string Name
        {
            get { return _name; }
        }

        /// <summary>
        /// Returns true if the Model has an entity with given Id in its entity graph.
        /// </summary>
        /// <param name="id">Id of the entity sought.</param>
        /// <returns>True if the Model has the entity in its entity graph.</returns>
        public abstract bool HasEntity(Guid id);

        /// <summary>
        /// Returns true if the model contains an entity that needs to be saved.  The model will not dig the graph
        /// more than required by the boundary that it represents (e.g. a "Client" model will not check for client's addresses, 
        /// while a "ClientAddress" model would.
        /// </summary>
        /// <returns></returns>
        public abstract bool ContainsEntitiesThatNeedToBeSaved();

        /// <summary>
        /// Returns a child entity that has the given Id.
        /// </summary>
        /// <param name="id">The id of the entity sought.</param>
        /// <returns>The entity, or null if no entity is found.</returns>
        public abstract IEntity GetEntity(Guid id);

    }
}
