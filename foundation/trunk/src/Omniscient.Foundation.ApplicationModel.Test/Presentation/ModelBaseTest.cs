using System;
using NUnit.Framework;
using Omniscient.Foundation.Data;

namespace Omniscient.Foundation.ApplicationModel.Presentation
{
    [TestFixture]
    public class ModelBaseTest
    {
        [Test]
        public void CreateModelBase()
        {
            Client c;
            c = new Client { Name = "dave" };
            ClientModel cm;
            cm = new ClientModel(c);

            Assert.AreEqual(c, cm.Entity);
        }

        public class Client : EntityBase
        {
            public string Name { get; set; }
        }

        public class ClientModel: ModelBase<Client>
        {
            public ClientModel(Client client)
                : base(client)
            { }

        }
    }
}
