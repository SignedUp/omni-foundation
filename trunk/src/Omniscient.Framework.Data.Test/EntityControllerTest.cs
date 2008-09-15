using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Omniscient.Framework.Data
{
    [TestFixture()]
    public class EntityControllerTest
    {
        private EntityMock _entityNew;
        private EntityMock _entityClean;
        private Guid _cleanId;
        private IEntityController<EntityMock> _controller;

        [TestFixtureSetUp()]
        public void Prepare()
        {
            _cleanId = Guid.NewGuid();
        }

        [SetUp()]
        public void Setup()
        {
            _controller = new EntityController<EntityMock>();
            _entityNew = new EntityMock() { Name = "I'm new", Age = 1 };
            _entityClean = new EntityMock(_cleanId) { Name = "I'm clean", Age = 2 };
        }

#region Tests on MarkAsDeleted
        [Test()]
        public void TestMarkDeletedOnNew()
        {
            _controller.MarkAsDeleted(_entityNew);
            Assert.AreEqual(EntityStatus.NonExistent, _entityNew.Status);
        }

        [Test()]
        public void TestMarkDeletedOnClean()
        {
            _controller.MarkAsDeleted(_entityClean);
            Assert.AreEqual(EntityStatus.ToBeDeleted, _entityClean.Status);
        }

        [Test()]
        public void TestMarkDeletedOnDirty()
        {
            _controller.BeginChanges(_entityClean);
            _entityClean.Name = "new name";
            _entityClean.Age = 99;
            Assert.AreEqual("new name", _entityClean.Name);
            Assert.AreEqual(99, _entityClean.Age);

            _controller.MarkAsDeleted(_entityClean);
            Assert.AreEqual(EntityStatus.ToBeDeleted, _entityClean.Status);
            
            //Deleting a dirty entity must result in it being reset to its original values,
            //so that a call to CancelChanges can safely turn it back to clean.
            Assert.AreEqual("I'm clean", _entityClean.Name);
            Assert.AreEqual(2, _entityClean.Age);
        }

        [Test()]
        [ExpectedException(typeof(InvalidOperationForStatusException))]
        public void TestMarkDeletedOnDeleted()
        {
            _controller.MarkAsDeleted(_entityClean);
            _controller.MarkAsDeleted(_entityClean);
        }

        [Test()]
        [ExpectedException(typeof(InvalidOperationForStatusException))]
        public void TestMarkDeletedOnNonExistent()
        {
            _controller.MarkAsDeleted(_entityNew);
            _controller.MarkAsDeleted(_entityNew);
        }

        [Test()]
        [ExpectedException(typeof(InvalidOperationForStatusException))]
        public void TestMarkDeletedOnClone()
        {
            _entityNew.Status = EntityStatus.Clone;
            _controller.MarkAsDeleted(_entityNew);
        }

#endregion

#region tests on BeginChanges operation

        /// <summary>
        /// A New entity is always New, even if we start modifying it.
        /// </summary>
        [Test()]
        public void TestBeginChangesOnNew()
        {
            _controller.BeginChanges(_entityNew);
            Assert.AreEqual(EntityStatus.New, _entityNew.Status);
        }

        /// <summary>
        /// A Clean entity will become Dirty when we start modifying it.
        /// </summary>
        [Test()]
        public void TestBeginChangesOnClean()
        {
            _controller.BeginChanges(_entityClean);
            Assert.AreEqual(EntityStatus.Dirty, _entityClean.Status);            
        }

        /// <summary>
        /// We don't allow starting new changes on a Dirty entity, since that would change the original clone
        /// and prevent a clean rollback.
        /// </summary>
        [Test()]
        [ExpectedException(typeof(InvalidOperationForStatusException))]
        public void TestBeginChangesOnDirty()
        {
            _controller.BeginChanges(_entityClean);
            _controller.BeginChanges(_entityClean);
        }

        /// <summary>
        /// We don't allow starting new changes on a Deleted entity.  Start by calling CancelChanges to
        /// return the entity to the Clean state.
        /// </summary>
        [Test()]
        [ExpectedException(typeof(InvalidOperationForStatusException))]
        public void TestBeginChangesOnDeleted()
        {
            _controller.MarkAsDeleted(_entityClean);
            _controller.BeginChanges(_entityClean);
        }

        /// <summary>
        /// We don't allow changes on a non-existent entity, since it's considered dead.
        /// </summary>
        [Test()]
        [ExpectedException(typeof(InvalidOperationForStatusException))]
        public void TestBeginChangesOnNonExistent()
        {
            _controller.MarkAsDeleted(_entityNew);
            _controller.BeginChanges(_entityNew);
        }

        /// <summary>
        /// We don't allow changes on an entity clone, since the purpose of a clone is to stay unchanged.
        /// </summary>
        [Test()]
        [ExpectedException(typeof(InvalidOperationForStatusException))]
        public void TestBeginChangesOnClone()
        {
            IEntity clone;
            clone = _controller.Clone(_entityNew);
            Assert.AreEqual(_entityNew.Id, clone.Id);
            Assert.AreEqual(EntityStatus.Clone, clone.Status);

            _controller.BeginChanges((EntityMock)clone);
        }
#endregion

#region Tests on CancelChanges operation
        
        [Test()]
        [ExpectedException(typeof(InvalidOperationForStatusException))]
        public void TestCancelChangesOnNew()
        {
            _controller.CancelChanges(_entityNew);
        }

        [Test()]
        [ExpectedException(typeof(InvalidOperationForStatusException))]
        public void TestCancelChangesOnClean()
        {
            _controller.CancelChanges(_entityClean);
        }

        [Test()]
        [ExpectedException(typeof(InvalidOperationForStatusException))]
        public void TestCancelChangesOnNonExistent()
        {
            _controller.MarkAsDeleted(_entityNew);
            _controller.CancelChanges(_entityNew);
        }

        [Test()]
        [ExpectedException(typeof(InvalidOperationForStatusException))]
        public void TestCancelChangesOnClone()
        {
            IEntity clone;
            clone = _controller.Clone(_entityClean);
            _controller.CancelChanges(clone as EntityMock);
        }

        [Test()]
        public void TestCancelChangesOnDirty()
        {
            _controller.BeginChanges(_entityClean);

            _entityClean.Age = 99;
            _entityClean.Name = "new name";

            Assert.AreEqual(99, _entityClean.Age);
            Assert.AreEqual("new name", _entityClean.Name);
            Assert.AreEqual(EntityStatus.Dirty, _entityClean.Status);

            _controller.CancelChanges(_entityClean);
            Assert.AreEqual(2, _entityClean.Age);
            Assert.AreEqual("I'm clean", _entityClean.Name);
            Assert.AreEqual(EntityStatus.Clean, _entityClean.Status);

        }

        [Test()]
        public void TestCancelChangesOnDeleted()
        {
            _controller.MarkAsDeleted(_entityClean);
            Assert.AreEqual(EntityStatus.ToBeDeleted, _entityClean.Status);
            _controller.CancelChanges(_entityClean);
            Assert.AreEqual(EntityStatus.Clean, _entityClean.Status);
        }

#endregion
    }
}
