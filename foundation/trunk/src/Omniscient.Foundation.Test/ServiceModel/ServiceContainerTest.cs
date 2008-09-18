using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Omniscient.Foundation.ServiceModel
{
    [TestFixture()]
    public class ServiceContainerTest
    {
        IServiceContainer _container;

        [SetUp()]
        public void Init()
        {
            _container = new ServiceContainer();
        }

        [Test()]
        public void TestRegisterService()
        {
            Assert.AreEqual(0, _container.ServiceCount);
            _container.RegisterService<IContract>(new ServiceMock());
            Assert.AreEqual(1, _container.ServiceCount);
        }

        [Test()]
        public void TestGetService()
        {
            _container.RegisterService(typeof(IContract), new ServiceMock());
            IContract imp;
            imp = _container.GetService<IContract>();
            Assert.IsNotNull(imp);
            string echo;
            echo = imp.Echo("salut");
            Assert.AreEqual("salut", echo);
        }

    }
}
