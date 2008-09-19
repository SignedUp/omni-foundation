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
    public abstract class ModelSingleEntityBase<TEntity>: ModelBase
        where TEntity: IEntity
    {
        protected ModelSingleEntityBase(TEntity entity): base()
        {
            Entity = entity;
        }

        public TEntity Entity
        {
            get;
            private set;
        }
    }
}
