using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Omniscient.Foundation.Data;
using Omniscient.Foundation.Data.ObjectQuery;

namespace Omniscient.Foundation.ApplicationModel.Presentation
{
    [TestFixture()]
    public class PresentationControllerTest
    {
        IPresentationController _controller;
        SidePanelViewControllerMock _sideController;
        MainViewControllerMock _mainController;
        EntityController<Client> _clientController;
        EntityController<Invoice> _invoiceController;

        [SetUp()]
        public void Init()
        {
            _controller = new PresentationController();
            _sideController = new SidePanelViewControllerMock();
            _mainController = new MainViewControllerMock();
            _controller.ViewControllers.Add(_mainController);
            _controller.ViewControllers.Add(_sideController);

            _controller.EntityControllerStore = new EntityControllerStore();
            _clientController = new EntityController<Client>();
            _clientController.Adapter = new ClientAdapter();
            _controller.EntityControllerStore.Register<Client>(_clientController);

            _invoiceController = new EntityController<Invoice>();
            _invoiceController.Adapter = new InvoiceAdapter();
            _controller.EntityControllerStore.Register<Invoice>(_invoiceController);
        }

        [Test()]
        public void TestCreateController()
        {
            Client client;
            client = GetSomeClient();
            ClientInvoicesModel model = new ClientInvoicesModel(client);

            _controller.OpenView(model);

            Assert.IsNotNull(_mainController.CurrentView);
            Assert.AreEqual(client, ((ModelSingleEntityBase<Client>)_mainController.CurrentView.Model).Entity);
        }

        [Test()]
        public void TestBeginEdit()
        {
            Client client;

            client = GetSomeClient();
            ClientInvoicesModel model = new ClientInvoicesModel(client);
            _controller.OpenView(model);

            Assert.AreEqual(EntityStatus.Clean, client.Status);
            _controller.BeginEdit(_mainController.CurrentView, client);
            Assert.AreEqual(EntityStatus.Dirty, client.Status);
        }

        [Test()]
        public void TestCancelEdit()
        {
            Client client;
            string originalName;

            client = GetSomeClient();
            originalName = client.Name;
            ClientInvoicesModel model = new ClientInvoicesModel(client);
            _controller.OpenView(model);

            _controller.BeginEdit(_mainController.CurrentView, client);
            client.Name = "new name";
            Assert.AreEqual("new name", client.Name);
            _controller.CancelEdit(_mainController.CurrentView, client);
            Assert.AreEqual(originalName, client.Name);
            Assert.AreEqual(EntityStatus.Clean, client.Status);
        }

        [Test()]
        public void TestEndEdit()
        {
            EntityList<Invoice> invoices;
            invoices = _invoiceController.Fetch(new OQuery<Invoice>());
            InvoicesModel invModel = new InvoicesModel(invoices);
            _controller.OpenView(invModel);
            Assert.IsNotNull(_sideController.CurrentView);
            Assert.AreSame(invModel, _sideController.CurrentView.Model);

            Client client;
            client = GetSomeClient();
            client.Invoices.AddRange(_invoiceController.Fetch(new OQuery<Invoice>()));
            ClientInvoicesModel model = new ClientInvoicesModel(client);
            _controller.OpenView(model);

            Invoice inv;
            inv = invoices[3];
            Assert.IsTrue(model.HasEntity(inv.Id));

            _controller.BeginEdit(_sideController.CurrentView, inv);
            double originalAmount = inv.Amount;
            inv.Amount = 99.0;

            _controller.EndEdit(_sideController.CurrentView, inv);
            Invoice inv2;
            inv2 = (Invoice)model.GetEntity(inv.Id);
            Assert.IsNotNull(inv2);
            Assert.AreEqual(inv, inv2);
            Assert.AreNotSame(inv2, inv);
            Assert.AreEqual(99.0, inv2.Amount);

        }

        private Client GetSomeClient()
        {
            foreach (Client c in _clientController.Fetch(new OQuery<Client>()))
                return c;
            return null;
        }

    }
}
