using NUnit.Framework;

namespace Omniscient.Foundation.ServiceModel
{
    [TestFixture]
    public class GenericSingletonServiceTest
    {
        [Test]
        public void TestCreateGenericSingletonService()
        {
            ServiceBase<IContract> srv;
            srv = new GenericSingletonService<IContract, ContractImplementation>();
            Assert.AreEqual("salut", srv.GetImplementation().Echo("salut"));
        }

        [Test]
        public void TestDataPersistence()
        {
            IService srv;
            srv = new GenericSingletonService<IContract, ContractImplementation>();
            ServiceProvider container = new ServiceProvider();
            container.RegisterService<IContract>(srv);

            IContract imp;
            imp = container.GetService<IContract>();
            imp.Name = "asdf";
            Assert.AreEqual("asdf", imp.Name);
            imp = container.GetService<IContract>();
            //verify that the name has persisted.
            Assert.AreEqual("asdf", imp.Name);
        }

        public interface IContract
        {
            string Echo(string msg);
            string Name { get; set; }
        }

        public class ContractImplementation : IContract
        {
            public string Echo(string msg)
            {
                return msg;
            }

            public string Name
            {
                get;
                set;
            }
        }
    }

}
