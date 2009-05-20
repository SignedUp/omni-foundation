using System;
using Omniscient.Foundation.Data;
using System.Collections.Generic;

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
            if (model.GetType().Name != "InvoicesModel") return null;

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

        #region IViewController Members

        public event SelectionChangedEventHandler CurrentViewSelectionChanged;

        #endregion
    }

    public class MainViewControllerMock : IViewController
    {
        private IView _current;

        #region IViewController Members

        public event EventHandler CurrentViewChanged;

        public IView OpenView(IModel model)
        {

            if (model.GetType().Name != "ClientInvoicesModel") return null;

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

        #region IViewController Members

        public event SelectionChangedEventHandler CurrentViewSelectionChanged;

        #endregion
    }

    #endregion

    #region Models

    public class ClientInvoicesModel : ModelBase<Client>
    {
        private Client _client;

        public ClientInvoicesModel(Client c) : base(c) 
        {
            _client = c;
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


        #region IView Members

        public event SelectionChangedEventHandler ViewContextChanged;

        #endregion

        #region IView Members

        event SelectionChangedEventHandler IView.SelectionChanged
        {
            add { throw new NotImplementedException(); }
            remove { throw new NotImplementedException(); }
        }

        IModel IView.Model
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        void IView.UpdateView()
        {
            throw new NotImplementedException();
        }

        string IView.Title
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region IView Members


        public object Selection
        {
            get { throw new NotImplementedException(); }
        }

        #endregion
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

        public string Title
        {
            get;
            set;
        }

        #endregion

        #region IView Members

        public event SelectionChangedEventHandler SelectionChanged;

        #endregion

        #region IView Members


        public object Selection
        {
            get { throw new NotImplementedException(); }
        }

        #endregion
    }

    #endregion
}
