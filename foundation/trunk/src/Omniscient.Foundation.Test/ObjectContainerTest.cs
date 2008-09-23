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

        public interface IObject { }

        public class Object1 : IObject { }

        public class Object2 : IObject { }
    }
}
