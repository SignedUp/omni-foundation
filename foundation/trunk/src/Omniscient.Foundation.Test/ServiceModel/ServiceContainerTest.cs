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
        IServiceProvider _container;

        [SetUp()]
        public void Init()
        {
            _container = new ServiceProvider();
        }

        [Test()]
        public void TestRegisterService()
        {
            Assert.AreEqual(0, _container.ServiceCount);
            _container.RegisterService<IContract>(new ServiceMock());
            Assert.AreEqual(1, _container.ServiceCount);
        }

        [Test()]
        public void TestRegisterServiceNoGeneric()
        {
            ServiceMock s = new ServiceMock();
            _container.RegisterService(typeof(IContract), s);
            IContract c;
            c = _container.GetService<IContract>();
            Assert.IsNotNull(c);
            Assert.AreEqual("dave", c.Echo("dave"));
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
