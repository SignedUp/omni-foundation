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

        public Client(Guid id, bool isLoaded) : base(id, isLoaded)
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

        public Invoice(Guid id, bool isLoaded) : base(id, isLoaded) { }

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
            _list.Add(new Client(Guid.NewGuid(), true) { Name = "dave" });
            _list.Add(new Client(Guid.NewGuid(), true) { Name = "frank" });
            _list.Add(new Client(Guid.NewGuid(), true) { Name = "steve" });
            
        }

        #region IEntityAdapter<Client> Members

        public Client LoadByKey(Guid id)
        {
            foreach (Client c in _list)
                if (c.Id == id) return c;
            return null;
        }

        public EntityList<Client> LoadByQuery(Omniscient.Foundation.Data.ObjectQuery.OQuery<Client> query)
        {
            return _list;
        }

        public void Save(Client entity)
        {
            throw new NotImplementedException();
        }

        public EntityList<Client> LoadByForeignKey(Guid id)
        {
            throw new NotImplementedException();
        }

        public EntityList<Client> LoadByQuery(string queryName)
        {
            throw new NotImplementedException();
        }

        public EntityList<Client> LoadAll()
        {
            throw new NotImplementedException();
        }

        public EntityList<Client> LoadByValueProperty(string propertyName, object value)
        {
            throw new NotImplementedException();
        }

        public void Save(IEnumerable<Client> entities)
        {
            throw new NotImplementedException();
        }

        #endregion
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

        public Invoice LoadByKey(Guid id)
        {
            return null;
        }

        public EntityList<Invoice> LoadByQuery(Omniscient.Foundation.Data.ObjectQuery.OQuery<Invoice> query)
        {
            EntityList<Invoice> invoices;

            invoices = new EntityList<Invoice>();
            for (int i = 0; i < 9; i++)
            {
                invoices.Add(new Invoice(_ids[i], true) { Amount = i + 1});
            }
            return invoices;
        }

        public void Save(Invoice entity)
        {
            throw new NotImplementedException();
        }

        public EntityList<Invoice> LoadByForeignKey(Guid id)
        {
            throw new NotImplementedException();
        }

        public EntityList<Invoice> LoadByQuery(string queryName)
        {
            throw new NotImplementedException();
        }

        public EntityList<Invoice> LoadByValueProperty(string propertyName, object value)
        {
            throw new NotImplementedException();
        }

        public EntityList<Invoice> LoadAll()
        {
            throw new NotImplementedException();
        }

        public void Save(IEnumerable<Invoice> entities)
        {
            throw new NotImplementedException();
        }

        #endregion
    }


    #endregion
}
