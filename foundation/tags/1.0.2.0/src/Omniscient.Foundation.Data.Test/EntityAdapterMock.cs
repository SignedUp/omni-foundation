using System;
using System.Collections.Generic;

namespace Omniscient.Foundation.Data
{
    public class EntityAdapterMock: IEntityAdapter<EntityMock>
    {
        public EntityMock LoadByKey(Guid id)
        {
            throw new NotImplementedException();
        }

        public EntityList<EntityMock> Fetch(ObjectQuery.OQuery<EntityMock> query)
        {
            throw new NotImplementedException();
        }

        public void Save(EntityMock entity)
        {
            //generate sql and save the entity.
        }

        #region IEntityAdapter<EntityMock> Members


        public EntityList<EntityMock> LoadByForeignKey(string propertyName, Guid id)
        {
            throw new NotImplementedException();
        }

        public EntityList<EntityMock> LoadByQuery(string queryName)
        {
            throw new NotImplementedException();
        }

        public EntityList<EntityMock> LoadByObjectQuery(ObjectQuery.OQuery<EntityMock> query)
        {
            throw new NotImplementedException();
        }

        public EntityList<EntityMock> LoadByValueProperty(string propertyName, object value)
        {
            throw new NotImplementedException();
        }

        public EntityList<EntityMock> LoadAll()
        {
            throw new NotImplementedException();
        }

        public void Save(IEnumerable<EntityMock> entities)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
