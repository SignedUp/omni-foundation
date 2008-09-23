﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Omniscient.Foundation.Data;

namespace Omniscient.Foundation.ApplicationModel.Presentation
{
    /// <summary>
    /// Base class for models with multiple entities as the root.
    /// </summary>
    public abstract class ModelMultiEntitiesBase<TEntity> : ModelBase
        where TEntity: IEntity, new()
    {
        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="entities">The list of root entities.</param>
        protected ModelMultiEntitiesBase(EntityList<TEntity> entities)
            : base()
        {
            Entities = entities;
        }

        /// <summary>
        /// Gets the list of root entities.
        /// </summary>
        public EntityList<TEntity> Entities
        {
            get;
            private set;
        }
    }
}
