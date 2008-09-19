using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Omniscient.Foundation.Data;

namespace Omniscient.Foundation.ApplicationModel.Presentation
{
    public abstract class ModelMultiEntitiesBase<TEntity>: ModelBase
        where TEntity: IEntity, new()
    {
        protected ModelMultiEntitiesBase(EntityList<TEntity> entities)
            : base()
        {
            Entities = entities;
        }

        public EntityList<TEntity> Entities
        {
            get;
            private set;
        }
    }
}
