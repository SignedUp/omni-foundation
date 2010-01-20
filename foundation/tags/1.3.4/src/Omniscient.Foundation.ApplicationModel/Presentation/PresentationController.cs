using System;
using System.Collections.Generic;

namespace Omniscient.Foundation.ApplicationModel.Presentation
{
    /// <summary>
    /// Default implementation for <see cref="IPresentationController"/>.  Generally, does not have to be derived for default behavior.
    /// </summary>
    public class PresentationController : IPresentationController
    {
        private List<IViewController> _controllers;
        private List<IView> _openedViews;
        private Dictionary<string, IPresenter> _presenters;

        ///<summary>
        ///</summary>
        public PresentationController() : this(true) { }

        /// <summary>
        /// Ctor.
        /// </summary>
        public PresentationController(bool supportsUserInput)
        {
            _controllers = new List<IViewController>();
            _openedViews = new List<IView>();
            SupportsUserInput = supportsUserInput;
            _presenters = new Dictionary<string, IPresenter>();
        }

        /// <summary>
        /// Opens a view.  The controller is responsible for finding a view for that model, instanciating the view
        /// and displaying it correctly.
        /// </summary>
        /// <param name="model">The model to open.</param>
        public void OpenView(IModel model)
        {
            IView view;
            foreach (IViewController controller in _controllers)
            {
                view = controller.OpenView(model);
                if (view != null) _openedViews.Add(view);
            }
        }

        /// <summary>
        /// Opens a view.  The controller is responsible for finding a view for those models, instanciating the view
        /// and displaying it correctly.
        /// </summary>
        /// <param name="models">The models to open.</param>
        public void OpenView<TModel>(IList<TModel> models)
            where TModel : IModel
        {
            IView view;
            foreach (IViewController controller in _controllers)
            {
                view = controller.OpenView(models);
                if (view != null) _openedViews.Add(view);
            }
        }

        public void CloseAllViews()
        {
            foreach (IViewController vc in _controllers)
            {
                vc.CloseView(vc.CurrentView);
            }
        }

        ///<summary>
        /// Closes all views for a specific model type.
        ///</summary>
        ///<typeparam name="TModel">Model type filter.</typeparam>
        public void CloseAllViews<TModel>() where TModel : IModel
        {
            foreach (IViewController controller in _controllers)
            {
                controller.CloseAllViews<TModel>();
            }
        }

        /// <summary>
        /// Informs the controller that a view has been closed.
        /// </summary>
        /// <param name="view">The view that's been closed.</param>
        public void ViewClosed(IView view)
        {
            throw new NotImplementedException();
        }

        public void RegisterPresenter(IPresenter presenter)
        {
            if (presenter == null) throw new ArgumentNullException("presenter");
            if (_presenters.ContainsKey(presenter.Name))
                throw new InvalidOperationException(string.Format("A presenter with name {0} is already registered.  Choose another name for your presenter.", presenter.Name));
            if (presenter.RequiresUserInput && !SupportsUserInput)
                throw new InvalidOperationException("The presentation controller does not support user inputs.");

            _presenters.Add(presenter.Name, presenter);
        }

        public IPresenter GetPresenter(string name)
        {
            if (!_presenters.ContainsKey(name)) return null;
            return _presenters[name];
        }

        public TPresenter GetPresenter<TPresenter>() where TPresenter : IPresenter
        {
            foreach (IPresenter presenter in _presenters.Values)
                if (typeof(TPresenter).IsAssignableFrom(presenter.GetType())) return (TPresenter)presenter;
            return default(TPresenter);


        }

        public bool SupportsUserInput
        {
            get;
            private set;
        }

        public void RegisterViewController(IViewController controller)
        {
            if (controller == null) throw new ArgumentNullException("controller");
            if (_controllers.Contains(controller))
                throw new InvalidOperationException(string.Format("A view controller with name {0} is already registered.  Choose another view controller.", controller));
            _controllers.Add(controller);

        }

        public TViewController GetViewController<TViewController>() where TViewController : IViewController
        {
            foreach (IViewController controller in _controllers)
                if (typeof(TViewController).IsAssignableFrom(controller.GetType())) return (TViewController)controller;
            return default(TViewController);
        }
    }
}
