using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Omniscient.Foundation.Data;

namespace Omniscient.Foundation.ApplicationModel.Presentation
{
    public class PresentationController: IPresentationController
    {
        private Dictionary<Guid, object> _locks;
        private object _lock;
        private List<IViewController> _controllers;
        private List<IView> _openedViews;

        public PresentationController()
        {
            _locks = new Dictionary<Guid, object>();
            _lock = new object();
            _controllers = new List<IViewController>();
            _openedViews = new List<IView>();
        }

        public List<IViewController> ViewControllers
        {
            get
            {
                return _controllers;
            }
        }

        public void OpenView(IModel model)
        {
            IView view;
            foreach (IViewController controller in ViewControllers)
            {
                view = controller.OpenView(model);
                if (view != null) _openedViews.Add(view);
            }
        }

        public void ViewClosed(IView view)
        {
            throw new NotImplementedException();
        }

        public void BeginEdit<TEntity>(IView view, TEntity entity)
             where TEntity : IEntity, new()
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

        public void CancelEdit<TEntity>(IView view, TEntity entity)
             where TEntity : IEntity, new()
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

        public void EndEdit<TEntity>(IView view, TEntity entity)
             where TEntity : IEntity, new()
        {
            IEntityController<TEntity> controller;
            controller = ApplicationManager.Current.ObjectContainer.Get<IEntityController<TEntity>>();
            if (controller == null) throw new InvalidOperationException(string.Format("No entity controller found for type {0}.", typeof(TEntity).FullName));

            foreach (IView v in _openedViews)
            {
                if (v != view && v.Model.HasEntity(entity.Id))
                { 
                    //ensure that new values are transfered to this model.
                    entity.CopyValues(v.Model.GetEntity(entity.Id));
                    v.UpdateView();
                }
            }
        }
    }
}
