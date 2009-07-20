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

        public IList<EntityMock> Fetch(ObjectQuery.OQuery<EntityMock> query)
        {
            throw new NotImplementedException();
        }

        public void Save(EntityMock entity)
        {
            //generate sql and save the entity.
        }

        #region IEntityAdapter<EntityMock> Members


        public IList<EntityMock> LoadByForeignKey(string propertyName, Guid id)
        {
            throw new NotImplementedException();
        }

        public IList<EntityMock> LoadByQuery(string queryName)
        {
            throw new NotImplementedException();
        }

        public IList<EntityMock> LoadByObjectQuery(ObjectQuery.OQuery<EntityMock> query)
        {
            throw new NotImplementedException();
        }

        public IList<EntityMock> LoadByValueProperty(string propertyName, object value)
        {
            throw new NotImplementedException();
        }

        public IList<EntityMock> Load()
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
