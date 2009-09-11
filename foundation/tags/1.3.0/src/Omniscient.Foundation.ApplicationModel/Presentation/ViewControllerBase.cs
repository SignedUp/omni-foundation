using System;
using System.Collections.Generic;

namespace Omniscient.Foundation.ApplicationModel.Presentation
{
    /// <summary>
    /// Base class for view controllers that implements some generic functionalities.
    /// </summary>
    public abstract class ViewControllerBase: IViewController
    {
        private Dictionary<Type, ViewModelBinding> _bindings;
        private IView _current;

        /// <summary>
        /// Raised when the <see cref="CurrentView"/> is changed to another view.
        /// </summary>
        public event EventHandler CurrentViewChanged = delegate { };
        
        /// <summary>
        /// Creates a view controller
        /// </summary>
        public ViewControllerBase()
        {
            OpenedViews = new List<IView>();
            _bindings = new Dictionary<Type, ViewModelBinding>();
        }

        /// <summary>
        /// Opens a view for the given model.
        /// </summary>
        /// <param name="model">Model to open in a view.</param>
        /// <returns>Newly opened view.</returns>
        public abstract IView OpenView(IModel model);

        /// <summary>
        /// Adds a view to the list of opened views, and sets <see cref="CurrentView"/>.
        /// </summary>
        /// <param name="view"></param>
        protected void OnNewViewOpened(IView view)
        {
            OpenedViews.Add(view);
            CurrentView = view;
        }

        /// <summary>
        /// Opens a view for the given list of models.
        /// </summary>
        /// <param name="models">Models to open in a view.</param>
        /// <returns>Newly opened view.</returns>
        public abstract IView OpenView<TModel>(IList<TModel> models) 
            where TModel : IModel;

        /// <summary>
        /// Returns the view that has the focus, or by any other mean is the "current view".
        /// </summary>
        public virtual IView CurrentView
        {
            get { return _current; }
            set
            {
                if (_current == value) return;
                if (!OpenedViews.Contains(value))
                    throw new InvalidOperationException(string.Format("View {0} is not opened yet.", value));
                _current = value;
                CurrentViewChanged(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Gets the list of currently opened views.
        /// </summary>
        protected List<IView> OpenedViews 
        { 
            get; 
            private set; 
        }

        /// <summary>
        /// Puts a certain view into focus, given that this view is already opened by the controller.
        /// </summary>
        /// <param name="view">The view that receives focus.</param>
        public virtual void Focus(IView view)
        {
            if (!OpenedViews.Contains(view)) 
                throw new InvalidOperationException(string.Format("View {0} is not opened yet.", view));

            CurrentView = view;
        }

        /// <summary>
        /// Closes a certain view, given that this view is already opened by the controller.
        /// </summary>
        /// <param name="view">The view to close.</param>
        /// <returns>True if the view has been successfully closed.  Otherwise, false.</returns>
        public bool CloseView(IView view)
        {
            if (!OpenedViews.Contains(view)) 
                throw new InvalidOperationException(string.Format("View {0} is not opened yet.", view));
            
            int index = OpenedViews.IndexOf(view);
            OpenedViews.Remove(view);
            if (index == OpenedViews.Count) index--;
            if (index >= 0) CurrentView = OpenedViews[index];
            return true;
        }

        /// <summary>
        /// Closes a group of views, until a certain view is not already opened by the controller.
        /// </summary>
        /// <param name="views">The views to close.</param>
        /// <returns>True if all views have been closed.  Otherwise, false.</returns>
        public bool CloseViewRange(IEnumerable<IView> views)
        {
            foreach (IView view in views)
                CloseView(view);
            return true;
        }

        /// <summary>
        /// Binds a model to a view and optionally a view model.  This is a DSL; e.g.:
        /// Bind&lt;MyModel&gt;().To&lt;MyView&gt;().Through&lt;MyViewModel&gt;();
        /// </summary>
        /// <typeparam name="TModel">The type of model to bind.</typeparam>
        /// <returns></returns>
        public ViewModelBinding Bind<TModel>()
            where TModel: IModel
        {
            if (!_bindings.ContainsKey(typeof(TModel)))
            {
                ViewModelBinding binding = new ViewModelBinding(typeof(TModel));
                _bindings.Add(typeof(TModel), binding);
            }
            return _bindings[typeof(TModel)];
        }

        /// <summary>
        /// Returns a binding for model of type <typeparamref name="TModel"/> name="TModel"/>, if any.  If not found, returns null.
        /// </summary>
        /// <typeparam name="TModel">The type of model to search a binding for.</typeparam>
        /// <returns>A binding of found, or null if not found.</returns>
        public ViewModelBinding GetBinding<TModel>()
            where TModel : IModel
        {
            return GetBinding(typeof(TModel));
        }

        /// <summary>
        /// Returns a binding for model of type <paramref name="modelType"/>, if any.  If not found, returns null.
        /// </summary>
        /// <param name="modelType">The type of model to search a binding for.</param>
        /// <returns>A binding of found, or null if not found.</returns>
        public ViewModelBinding GetBinding(Type modelType)
        {
            if (_bindings.ContainsKey(modelType)) return _bindings[modelType];
            return null;
        }

        /// <summary>
        /// Holds a binding between a model and an view.
        /// </summary>
        public class ViewModelBinding
        {
            /// <summary>
            /// Creates a binding for a type of model.
            /// </summary>
            /// <param name="modelType">The type of model to bind.</param>
            public ViewModelBinding(Type modelType)
            {
                ModelType = modelType;
            }

            /// <summary>
            /// The type of model that is bound.
            /// </summary>
            public Type ModelType { get; private set; }

            /// <summary>
            /// The binding to the view.
            /// </summary>
            public ViewViewModelBinding Binding { get; private set; }

            /// <summary>
            /// Binds a model to a view
            /// </summary>
            /// <typeparam name="TView">The type of view to bind to.</typeparam>
            /// <returns>A binding.</returns>
            public ViewViewModelBinding To<TView>()
            {
                if (Binding == null) Binding = new ViewViewModelBinding(typeof(TView));
                return Binding;
            }
        }

        /// <summary>
        /// Represents a binding between a model, a view and optionally a view model.
        /// </summary>
        public class ViewViewModelBinding
        {
            /// <summary>
            /// Creates the binding
            /// </summary>
            /// <param name="viewType">The type of the view that is bound.</param>
            public ViewViewModelBinding(Type viewType)
            {
                ViewType = viewType;
            }

            /// <summary>
            /// Gets the type of the view that is bound.
            /// </summary>
            public Type ViewType { get; private set; }

            /// <summary>
            /// Gets the optional view model that is bound.
            /// </summary>
            public Type ViewModelType { get; private set; }

            /// <summary>
            /// Binds a view model to the model-view binding.
            /// </summary>
            /// <typeparam name="TViewModel">The type of view model to bind.</typeparam>
            /// <returns>Returns this.</returns>
            public ViewViewModelBinding Through<TViewModel>()
            {
                ViewModelType = typeof(TViewModel);
                return this;
            }
        }
    }
}
