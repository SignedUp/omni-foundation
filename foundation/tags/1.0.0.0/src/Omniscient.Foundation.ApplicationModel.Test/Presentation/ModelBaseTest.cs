using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Omniscient.Foundation.Data;

namespace Omniscient.Foundation.ApplicationModel.Presentation
{
    [TestFixture()]
    public class ModelBaseTest
    {
        [Test()]
        public void CreateModelBase()
        {
            Client c;
            c = new Client() { Name = "dave" };
            ClientModel cm;
            cm = new ClientModel(c);

            Assert.AreEqual(c, cm.Entity);
        }

        public class Client : EntityBase
        {
            public string Name { get; set; }
        }

        public class ClientModel: ModelSingleEntityBase<Client>
        {
            public ClientModel(Client client)
                : base(client)
            { }

            public override bool HasEntity(Guid id)
            {
                throw new NotImplementedException();
            }

            public override bool ContainsEntitiesThatNeedToBeSaved()
            {
                throw new NotImplementedException();
            }

            public override IEntity GetEntity(Guid id)
            {
                throw new NotImplementedException();
            }
        }
    }
}
