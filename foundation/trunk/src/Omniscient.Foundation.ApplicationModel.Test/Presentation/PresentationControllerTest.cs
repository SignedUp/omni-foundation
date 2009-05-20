using System;
using NUnit.Framework;
using Omniscient.Foundation.Data;
using Omniscient.Foundation.Data.ObjectQuery;

namespace Omniscient.Foundation.ApplicationModel.Presentation
{
    [TestFixture]
    public class PresentationControllerTest
    {
        IPresentationController _controller;
        SidePanelViewControllerMock _sideController;
        MainViewControllerMock _mainController;
        EntityController<Client> _clientController;
        EntityController<Invoice> _invoiceController;
        IEntityAdapter<Client> _clientAdapter;
        IEntityAdapter<Invoice> _invoiceAdapter;

        [SetUp]
        public void Init()
        {
            _controller = new PresentationController(false);
            _sideController = new SidePanelViewControllerMock();
            _mainController = new MainViewControllerMock();
            _controller.RegisterViewController(_mainController);
            _controller.RegisterViewController(_sideController);

            //ApplicationManager.Current.ObjectContainer = new ObjectContainer();

            //_clientAdapter = new ClientAdapter();
            //_clientController = new EntityController<Client>();
            //ApplicationManager.Current.ObjectContainer.Register<IEntityController<Client>>(_clientController);

            //_invoiceAdapter = new InvoiceAdapter();
            //_invoiceController = new EntityController<Invoice>();
            //ApplicationManager.Current.ObjectContainer.Register<IEntityController<Invoice>>(_invoiceController);
        }

        [Test]
        public void TestCreateController()
        {
            Client client;
            client = GetSomeClient();
            ClientInvoicesModel model = new ClientInvoicesModel(client);

            _controller.OpenView(model);

            Assert.IsNotNull(_mainController.CurrentView);
            Assert.AreEqual(client, ((ModelBase<Client>)_mainController.CurrentView.Model).Entity);
        }

        [Test]
        public void TestBeginEdit()
        {
            Client client;

            client = GetSomeClient();
            ClientInvoicesModel model = new ClientInvoicesModel(client);
            _controller.OpenView(model);

            Assert.AreEqual(EntityStatus.Clean, client.Status);
            model.BeginEdit();
            Assert.AreEqual(EntityStatus.Dirty, client.Status);
        }

        [Test]
        public void TestCancelEdit()
        {
            Client client;
            string originalName;

            client = GetSomeClient();
            originalName = client.Name;
            ClientInvoicesModel model = new ClientInvoicesModel(client);
            _controller.OpenView(model);

            //_controller.BeginEdit(_mainController.CurrentView, client);
            client.Name = "new name";
            Assert.AreEqual("new name", client.Name);
            //_controller.CancelEdit(_mainController.CurrentView, client);
            Assert.AreEqual(originalName, client.Name);
            Assert.AreEqual(EntityStatus.Clean, client.Status);
        }

        [Test]
        public void TestEndEdit()
        {
            //List<Invoice> invoices;
            //invoices = _invoiceAdapter.LoadByObjectQuery(new OQuery<Invoice>());
            //InvoicesModel invModel = new InvoicesModel(invoices);
            //_controller.OpenView(invModel);
            //Assert.IsNotNull(_sideController.CurrentView);
            //Assert.AreSame(invModel, _sideController.CurrentView.Model);

            //Client client;
            //client = GetSomeClient();
            //client.Invoices.AddRange(_invoiceAdapter.LoadByObjectQuery(new OQuery<Invoice>()));
            //ClientInvoicesModel model = new ClientInvoicesModel(client);
            //_controller.OpenView(model);

            //Invoice inv;
            //inv = invoices[3];
            //Assert.IsTrue(model.HasEntity(inv.Id));

            //_controller.BeginEdit(_sideController.CurrentView, inv);
            //inv.Amount = 99.0;

            //_controller.EndEdit(_sideController.CurrentView, inv);
            //Invoice inv2;
            //inv2 = (Invoice)model.GetEntity(inv.Id);
            //Assert.IsNotNull(inv2);
            //Assert.AreEqual(inv, inv2);
            //Assert.AreNotSame(inv2, inv);
            //Assert.AreEqual(99.0, inv2.Amount);

        }

        [Test]
        public void GetNoPresenter()
        {
            IPresenter presenter;
            presenter = _controller.GetPresenter("no");
            Assert.IsNull(presenter);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RegisterNullPresenter()
        {
            _controller.RegisterPresenter(null);
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RegisterDoublePresenter()
        {
            IPresenter p;
            p = new Presenter("double", false);
            _controller.RegisterPresenter(p);
            _controller.RegisterPresenter(p);
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RegisterInteractive()
        {
            IPresenter p;
            p = new Presenter("interactive", true);
            _controller.RegisterPresenter(p);
        }

        [Test]
        public void GetRegisteredByName()
        {
            IPresenter p;

            p = new Presenter("a", false);
            _controller.RegisterPresenter(p);

            p = new Presenter("b", false);
            _controller.RegisterPresenter(p);

            p = new Presenter("c", false);
            _controller.RegisterPresenter(p);

            p = _controller.GetPresenter("b");
            Assert.IsNotNull(p);
            Assert.AreEqual("b", p.Name);
        }

        [Test]
        public void GetRegisteredByContract()
        {
            IPresenter p;

            p = new PresenterA("a", false);
            _controller.RegisterPresenter(p);

            p = new PresenterB("b", false);
            _controller.RegisterPresenter(p);

            p = new PresenterC("c", false);
            _controller.RegisterPresenter(p);

            p = _controller.GetPresenter<PresenterB>();
            Assert.IsNotNull(p);
            Assert.AreEqual("b", p.Name);

        }

        private Client GetSomeClient()
        {
            //foreach (Client c in _clientAdapter.LoadByObjectQuery(new OQuery<Client>()))
            //    return c;
            return null;
        }

        private class Presenter : IPresenter
        {
            public Presenter(string name, bool requiresUserInput)
            {
                Name = name;
                RequiresUserInput = requiresUserInput;
            }

            public string Name
            {
                get;
                private set;
            }

            public bool RequiresUserInput
            {
                get;
                private set;
            }

            public void WriteMessage(object message)
            {
                Console.WriteLine(message);
            }
        }

        private class PresenterA : Presenter
        {
            public PresenterA(string name, bool requiresUserInput) : base(name, requiresUserInput) { }
        }
        private class PresenterB : Presenter
        {
            public PresenterB(string name, bool requiresUserInput) : base(name, requiresUserInput) { }
        }
        private class PresenterC : Presenter
        {
            public PresenterC(string name, bool requiresUserInput) : base(name, requiresUserInput) { }
        }
    }
}
