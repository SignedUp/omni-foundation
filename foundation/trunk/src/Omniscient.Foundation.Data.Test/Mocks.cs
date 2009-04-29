using System;
using System.Collections.Generic;

namespace Omniscient.Foundation.Data
{
    #region Entities

    public class Client : EntityBase
    {
        public Client()
        {
            Invoices = new List<Invoice>();
        }

        public Client(Guid id) : base(id)
        {
            Invoices = new List<Invoice>();
        }

        public string Name { get; set; }

        public List<Invoice> Invoices { get; private set; }
    }

    public class Invoice : EntityBase
    {
        public Invoice()
        { }

        public Invoice(Guid id) : base(id) { }

        public double Amount { get; set; }
    }

    #endregion

    #region Entity Adapters

    public class ClientAdapter : IEntityAdapter<Client>
    {
        private List<Client> _list;

        public ClientAdapter()
        {
            _list = new List<Client>();
            _list.Add(new Client(Guid.NewGuid()) { Name = "dave" });
            _list.Add(new Client(Guid.NewGuid()) { Name = "frank" });
            _list.Add(new Client(Guid.NewGuid()) { Name = "steve" });
            
        }

        #region IEntityAdapter<Client> Members

        public Client LoadByKey(Guid id)
        {
            foreach (Client c in _list)
                if (c.Id == id) return c;
            return null;
        }

        public List<Client> LoadByObjectQuery(ObjectQuery.OQuery<Client> query)
        {
            return _list;
        }

        public void Save(Client entity)
        {
            throw new NotImplementedException();
        }

        public List<Client> LoadByForeignKey(string propertyName, Guid id)
        {
            throw new NotImplementedException();
        }

        public List<Client> LoadByQuery(string queryName)
        {
            throw new NotImplementedException();
        }

        public IList<Client> LoadAll()
        {
            throw new NotImplementedException();
        }

        public List<Client> LoadByValueProperty(string propertyName, object value)
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

        public List<Invoice> LoadByObjectQuery(ObjectQuery.OQuery<Invoice> query)
        {
            List<Invoice> invoices;

            invoices = new List<Invoice>();
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

        public List<Invoice> LoadByForeignKey(string propertyName, Guid id)
        {
            throw new NotImplementedException();
        }

        public List<Invoice> LoadByQuery(string queryName)
        {
            throw new NotImplementedException();
        }

        public List<Invoice> LoadByValueProperty(string propertyName, object value)
        {
            throw new NotImplementedException();
        }

        public IList<Invoice> LoadAll()
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
