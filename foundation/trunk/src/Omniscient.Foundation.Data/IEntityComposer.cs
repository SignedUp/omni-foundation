using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Omniscient.Foundation.Data.ObjectQuery;

namespace Omniscient.Foundation.Data
{
    interface IEntityComposer<TEntity> where TEntity: IEntity
    {
        IEntityList<TEntity> Load(IEntityComposer<IEntity> composer, OQuery<TEntity> query);
    }
}
