using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Omniscient.Foundation
{
    [TestFixture()]
    public class ObjectContainerTest
    {
        private IObjectContainer _container;

        [SetUp()]
        public void Init()
        {
            _container = new ObjectContainer();
        }

        [Test()]
        public void RegisterObjectByInferingType()
        {
            _container.Register(new Object1());

            IObject obj;
            obj = _container.Get<Object1>();
            Assert.IsNotNull(obj);
            obj = _container.Get<IObject>();
            Assert.IsNull(obj);
        }

        [Test()]
        public void RegisterObjectByGivingType()
        {
            _container.Register<IObject>(new Object1());

            IObject obj;
            obj = _container.Get<Object1>();
            Assert.IsNull(obj);
            obj = _container.Get<IObject>();
            Assert.IsNotNull(obj);
        }

        [Test()]
        public void RegisterContainerItemWithExistingItem()
        {
            ContainerItem<IObject> item;
            item = new ContainerItem<IObject>(new Object1());
            _container.Register(item);

            IObject obj;
            obj = _container.Get<Object1>();
            Assert.IsNull(obj);
            obj = _container.Get<IObject>();
            Assert.IsNotNull(obj);
        }

        [Test()]
        public void RegisterContainerItemForNewInstances()
        {
            NewObjectContainerItem item;
            item = new NewObjectContainerItem();
            _container.Register<IObject>(item);

            IObject obj;
            obj = _container.Get<Object1>();
            Assert.IsNull(obj);
            obj = _container.Get<IObject>();
            Assert.IsNotNull(obj);
            Assert.AreEqual("new from container item", obj.Name);
        }

        public interface IObject 
        {
            string Name { get; set; }
        }

        public class Object1 : IObject 
        {
            public string Name { get; set; }
        }

        public class Object2 : IObject
        {
            public string Name { get; set; }
        }

        public class NewObjectContainerItem : ContainerItem<IObject>
        {
            public override IObject GetInstance()
            {
                return new Object1() { Name = "new from container item"};
            }
        }
    }
}
