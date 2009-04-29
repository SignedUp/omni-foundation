using System;
using NUnit.Framework;

namespace Omniscient.Foundation.Data
{
    [TestFixture]
    public class EntityTest
    {
        [Test]
        public void TestCreateNew()
        {
            IEntity entity = new EntityMock();
            Assert.IsNotNull(entity.Id);
            Assert.AreNotEqual(Guid.Empty, entity.Id);

            Assert.AreEqual(EntityStatus.New, entity.Status);
            
            Assert.AreEqual("EntityMock", entity.GetType().Name);
        }

        [Test]
        public void TestCreateExisting()
        {
            Guid id;
            IEntity entity;

            id = Guid.NewGuid();
            entity = new EntityMock(id, true);

            Assert.AreEqual(id, entity.Id);
            Assert.AreEqual(EntityStatus.Clean, entity.Status);
        }

    }
}
