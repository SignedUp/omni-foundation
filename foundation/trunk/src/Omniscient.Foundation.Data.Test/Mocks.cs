using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omniscient.Foundation.Data
{
    #region Entities

    public class Client : EntityBase
    {
        public Client() : base() 
        {
            Invoices = new EntityList<Invoice>();
        }

        public Client(Guid id) : base(id)
        {
            Invoices = new EntityList<Invoice>();
        }

        [EntityProperty(EntityPropertyType.Value)]
        public string Name { get; set; }

        [EntityProperty(EntityPropertyType.ReferenceList)]
        public EntityList<Invoice> Invoices { get; private set; }
    }

    public class Invoice : EntityBase
    {
        public Invoice() : base() { }

        public Invoice(Guid id) : base(id) { }

        [EntityProperty(EntityPropertyType.Value)]
        public double Amount { get; set; }
    }

    #endregion

    #region Entity Adapters

    public class ClientAdapter : IEntityAdapter<Client>
    {
        private EntityList<Client> _list;

        public ClientAdapter()
        {
            _list = new EntityList<Client>();
            _list.Add(new Client(Guid.NewGuid()) { Name = "dave" });
            _list.Add(new Client(Guid.NewGuid()) { Name = "frank" });
            _list.Add(new Client(Guid.NewGuid()) { Name = "steve" });
            
        }

        public Client Fetch(Guid id)
        {
            foreach (Client c in _list)
                if (c.Id == id) return c;
            return null;
        }

        public EntityList<Client> Fetch(Omniscient.Foundation.Data.ObjectQuery.OQuery<Client> query)
        {
            return _list;
        }

        public void Save(Client entity)
        {
            throw new NotImplementedException();
        }
    }

    public class InvoiceAdapter : IEntityAdapter<Invoice>
    {
        private List<Guid> _ids;

        public InvoiceAdapter()
        {
            _ids = new List<Guid>();
            for (int i = 1; i < 10; i++)
            {
                _ids.Add(Guid.NewGuid());
            }
        }

        #region IEntityAdapter<Invoice> Members

        public Invoice Fetch(Guid id)
        {
            return null;
        }

        public EntityList<Invoice> Fetch(Omniscient.Foundation.Data.ObjectQuery.OQuery<Invoice> query)
        {
            EntityList<Invoice> invoices;

            invoices = new EntityList<Invoice>();
            for (int i = 0; i < 9; i++)
            {
                invoices.Add(new Invoice(_ids[i]) { Amount = i + 1});
            }
            return invoices;
        }

        public void Save(Invoice entity)
        {
            throw new NotImplementedException();
        }

        #endregion
    }


    #endregion
}
