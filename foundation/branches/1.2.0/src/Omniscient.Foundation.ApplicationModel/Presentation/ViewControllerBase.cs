using System;
using System.Collections.Generic;

namespace Omniscient.Foundation.ApplicationModel.Presentation
{
    public abstract class ViewControllerBase: IViewController
    {
        private Dictionary<Type, ViewModelBinding> _bindings;

        public event EventHandler CurrentViewChanged = delegate { };
        
        public ViewControllerBase()
        {
            OpenedViews = new List<IView>();
            _bindings = new Dictionary<Type, ViewModelBinding>();
        }

        public abstract IView OpenView(IModel model);

        public abstract IView OpenView<TModel>(IList<TModel> models) 
            where TModel : IModel;

        private IView _current;
        public virtual IView CurrentView
        {
            get { return _current; }
            set
            {
                if (_current == value) return;
                _current = value;
                CurrentViewChanged(this, EventArgs.Empty);
            }
        }

        protected List<IView> OpenedViews 
        { 
            get; 
            private set; 
        }

        public virtual void Focus(IView view)
        {
            if (!OpenedViews.Contains(view)) 
                throw new InvalidOperationException(string.Format("View {0} is not opened yet.", view));

            CurrentView = view;
        }

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

        public bool CloseViewRange(IEnumerable<IView> views)
        {
            foreach (IView view in views)
                CloseView(view);
            return true;
        }

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

        public class ViewModelBinding
        {
            public ViewModelBinding(Type modelType)
            {
                ModelType = modelType;
            }

            public Type ModelType { get; private set; }

            public ViewViewModelBinding Binding { get; private set; }

            public ViewViewModelBinding To<TView>()
            {
                if (Binding == null) Binding = new ViewViewModelBinding(typeof(TView));
                return Binding;
            }
        }

        public class ViewViewModelBinding
        {
            public ViewViewModelBinding(Type viewType)
            {
                ViewType = viewType;
            }

            public Type ViewType { get; private set; }
            public Type ViewModelType { get; private set; }

            public ViewViewModelBinding Through<TViewModel>()
            {
                ViewModelType = typeof(TViewModel);
                return this;
            }
        }
    }
}
