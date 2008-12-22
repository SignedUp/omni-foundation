using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Omniscient.Foundation.Data
{
    [TestFixture()]
    public class EntityListTest
    {
        [Test()]
        public void TestCreate()
        {
            EntityList<EntityMock> list;
            list = new EntityList<EntityMock>();
            Assert.AreEqual(0, list.Count);
        }

        [Test()]
        public void TestAddOne()
        {
            EntityList<EntityMock> list;
            list = new EntityList<EntityMock>();
            list.Add(new EntityMock());
            Assert.AreEqual(1, list.Count);            
        }

        [Test()]
        public void TestConvert()
        {
            EntityList list;
            list = new EntityList();
            list.Add(new EntityMock());

            IList<IEntity> converted;
            converted = (IList<IEntity>)list;
        }
    }
}
