using System;
using Omniscient.Foundation.Data;

namespace Omniscient.Foundation.ApplicationModel.Presentation
{
    #region View Controllers
    
    public class SidePanelViewControllerMock : IViewController
    {
        IView _current;

        #region IViewController Members

        public event EventHandler CurrentViewChanged;

        public IView OpenView(IModel model)
        {
            if (model.Name != "InvoicesModel") return null;

            InvoiceList view;
            view = new InvoiceList();
            view.Model = model;
            CurrentView = view;

            return CurrentView;
        }

        public IView CurrentView
        {
            get
            {
                return _current;
            }
            set
            {
                _current = value;
                if (CurrentViewChanged != null) CurrentViewChanged(this, EventArgs.Empty);
            }
        }

        #endregion

        #region IViewController Members


        public void Focus(IView view)
        {
            throw new NotImplementedException();
        }

        public bool CloseView(IView view)
        {
            throw new NotImplementedException();
        }

        public bool CloseViewRange(System.Collections.Generic.IEnumerable<IView> views)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    public class MainViewControllerMock : IViewController
    {
        private IView _current;

        #region IViewController Members

        public event EventHandler CurrentViewChanged;

        public IView OpenView(IModel model)
        {

            if (model.Name != "ClientInvoicesModel") return null;

            ClientDetail view;
            view = new ClientDetail();
            view.Model = model;
            CurrentView = view;

            return CurrentView;
        }

        public IView CurrentView
        {
            get
            {
                return _current;
            }
            set
            {
                _current = value;
                if (CurrentViewChanged != null) CurrentViewChanged(this, EventArgs.Empty);
            }
        }

        #endregion

        #region IViewController Members


        public void Focus(IView view)
        {
            throw new NotImplementedException();
        }

        public bool CloseView(IView view)
        {
            throw new NotImplementedException();
        }

        public bool CloseViewRange(System.Collections.Generic.IEnumerable<IView> views)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    #endregion

    #region Models

    public class ClientInvoicesModel : ModelSingleEntityBase<Client>
    {
        private Client _client;

        public ClientInvoicesModel(Client c) : base(c) 
        {
            _client = c;
        }

        public override bool HasEntity(Guid id)
        {
            if (_client.Id == id) return true;

            foreach (Invoice inv in _client.Invoices)
            {
                if (inv.Id == id) return true;
            }
            return false;
        }

        public override IEntity GetEntity(Guid id)
        {
            if (_client.Id == id) return _client;

            foreach (Invoice inv in _client.Invoices)
            {
                if (inv.Id == id) return inv;
            }
            return null;
        }

        public override bool ContainsEntitiesThatNeedToBeSaved()
        {
            throw new NotImplementedException();
        }
    }

    public class InvoicesModel : ModelMultiEntitiesBase<Invoice>
    {
        public InvoicesModel(EntityList<Invoice> invoices) : base(invoices) { }

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


    #endregion

    #region Views

    public class ClientDetail : IView
    {

        public IModel Model
        {
            get;
            set;
        }

        public void UpdateView()
        {
            //update controls here...
        }

        public string Title
        {
            get;
            set;
        }

        public object Icon
        {
            get;
            set;
        }
    }

    public class InvoiceList : IView
    {
        #region IView Members

        public IModel Model
        {
            get;
            set;
        }

        public void UpdateView()
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    #endregion
}
