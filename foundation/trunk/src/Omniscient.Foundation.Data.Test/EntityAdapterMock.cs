using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omniscient.Foundation.Data
{
    public class EntityAdapterMock: IEntityAdapter<EntityMock>
    {
        public EntityMock Fetch(Guid id)
        {
            throw new NotImplementedException();
        }

        public EntityMock[] Fetch(Omniscient.Framework.Data.ObjectQuery.OQuery<EntityMock> query)
        {
            throw new NotImplementedException();
        }

        public void Save(EntityMock entity)
        {
            //generate sql and save the entity.
        }
    }
}
