using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Omniscient.Foundation.Data.ObjectQuery;

namespace Omniscient.Foundation.Data
{
    public interface IEntityComposer<TEntity> where TEntity: IEntity
    {
        IEntityList<TEntity> Compose();
    }
}
