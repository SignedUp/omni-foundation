using System;
using System.Collections.Generic;
using Omniscient.Foundation.Data;
using System.Diagnostics;

namespace Omniscient.Foundation.ApplicationModel.Presentation
{
    /// <summary>
    /// Default implementation for <see cref="IPresentationController"/>.  Generally, does not have to be derived for default behavior.
    /// </summary>
    public class PresentationController: IPresentationController
    {
        private Dictionary<Guid, object> _locks;
        private object _lock;
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
            _locks = new Dictionary<Guid, object>();
            _lock = new object();
            _controllers = new List<IViewController>();
            _openedViews = new List<IView>();
            SupportsUserInput = supportsUserInput;
            _presenters = new Dictionary<string, IPresenter>();
        }

        /// <summary>
        /// Gets the list of view controllers.  Default use is to feed that list at application startup.
        /// </summary>
        public List<IViewController> ViewControllers
        {
            get
            {
                return _controllers;
            }
        }

        /// <summary>
        /// Opens a view.  The controller is responsible for finding a view for that model, instanciating the view
        /// and displaying it correctly.
        /// </summary>
        /// <param name="model">The model to open.</param>
        public void OpenView(IModel model)
        {
            IView view;
            foreach (IViewController controller in ViewControllers)
            {
                view = controller.OpenView(model);
                if (view != null) _openedViews.Add(view);
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

        /// <summary>
        /// Begins editing an entity.  The entity will be cloned to preserve original values, in case that <see cref="CancelEdit{TEntity}"/> would be called.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="view">The view that wants to edit the entity.</param>
        /// <param name="entity">The entity to edit</param>
        /// <exception cref="InvalidOperationException">Thrown when the entity is already being edited elsewhere.</exception>
        public void BeginEdit<TEntity>(IView view, TEntity entity)
             where TEntity : IEntity
        {            
            IEntityController<TEntity> controller;
            controller = ApplicationManager.Current.ObjectContainer.Get<IEntityController<TEntity>>();
            if (controller == null) throw new InvalidOperationException(string.Format("No entity controller found for type {0}.", typeof(TEntity).FullName));

            lock (_lock)
            {
                if (_locks.ContainsKey(entity.Id))
                    throw new InvalidOperationException(string.Format("Entity {0} is already being edited.", entity));
                _locks.Add(entity.Id, null);
            }

            controller.BeginChanges(entity);
        }

        /// <summary>
        /// Cancels editing the entity.  The entity will be recopied against the clone, and the status set back to <see cref="EntityStatus.Clean"/>.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="view">The view that was editing the entity.</param>
        /// <param name="entity">The entity that was edited.</param>
        public void CancelEdit<TEntity>(IView view, TEntity entity)
             where TEntity : IEntity
        {
            IEntityController<TEntity> controller;
            controller = ApplicationManager.Current.ObjectContainer.Get<IEntityController<TEntity>>();
            if (controller == null) throw new InvalidOperationException(string.Format("No entity controller found for type {0}.", typeof(TEntity).FullName));

            lock (_lock)
            {
                if (!_locks.ContainsKey(entity.Id))
                    throw new InvalidOperationException(string.Format("Entity {0} is not being edited.", entity));
                _locks.Remove(entity.Id);
            }

            controller.CancelChanges(entity);
        }

        /// <summary>
        /// Ends editing an entity - that is, accept changes.  The clone will be destroyed, and the entity will be permanently changed,
        /// but not saved yet.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="view">The view that was editing the entity.</param>
        /// <param name="entity">The entity that was edited.</param>
        public void EndEdit<TEntity>(IView view, TEntity entity)
             where TEntity : IEntity
        {
            IEntityController<TEntity> controller;
            controller = ApplicationManager.Current.ObjectContainer.Get<IEntityController<TEntity>>();
            if (controller == null) throw new InvalidOperationException(string.Format("No entity controller found for type {0}.", typeof(TEntity).FullName));

            foreach (IView v in _openedViews)
            {
                if (entity.Status == EntityStatus.New)
                {
                    Debug.Assert(false, "Implement capability to add new entities and update views");
                }

                if (v != view && v.Model.HasEntity(entity.Id))
                { 
                    //ensure that new values are transfered to this model.
                    entity.CopyTo(v.Model.GetEntity(entity.Id), false);
                    v.UpdateView();
                }
            }
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

        public PresenterType GetPresenter<PresenterType>() where PresenterType : IPresenter
        {                  
            foreach (object presenter in _presenters.Values)
                if (typeof(PresenterType).IsAssignableFrom(presenter.GetType())) return (PresenterType)presenter;
            return default(PresenterType);


        }

        public bool SupportsUserInput
        {
            get;
            private set;
        }
    }
}
