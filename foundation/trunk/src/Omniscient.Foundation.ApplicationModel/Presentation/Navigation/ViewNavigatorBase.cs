using System;
using System.Collections.Generic;

namespace Omniscient.Foundation.ApplicationModel.Presentation.Navigation
{
    public abstract class ViewNavigatorBase: IViewNavigator
    {
        public event EventHandler<CurrentPositionChangedEventArgs> CurrentPositionChanged;

        public ViewNavigatorBase(IViewController controller)
        {
            ViewController = controller;
            RegisteredViews = new List<IView>();

            //Initialize the event handlers so we don't have to do a null check
            CurrentPositionChanged = delegate { };
        }

        protected List<IView> RegisteredViews
        {
            get;
            private set;
        }

        protected int Cursor
        {
            get;
            set;
        }

        protected IViewController ViewController
        {
            get;
            private set;
        }

        public bool CanGoBack()
        {
            return Cursor > 0;
        }

        public bool CanGoForward()
        {
            return Cursor < RegisteredViews.Count - 1;
        }

        public void GoBack()
        {
            if (!this.CanGoBack()) return;
            ViewController.Focus(RegisteredViews[--Cursor]);
            OnCurrentPositionChanged(Cursor + 1);
        }

        public void GoForward()
        {
            if (!this.CanGoForward()) return;
            ViewController.Focus(RegisteredViews[++Cursor]);
            OnCurrentPositionChanged(Cursor - 1);
        }

        public abstract void NavigateTo(IView view);

        public IView CurrentView
        {
            get 
            {
                if (RegisteredViews.Count <= 0) return null;
                return RegisteredViews[Cursor];
            }
        }

        public int Count { get { return RegisteredViews.Count; } }
        
        public int CurrentPosition { get { return Cursor; } }

        protected void OnCurrentPositionChanged(int lastPositionValue)
        {
            CurrentPositionChanged(this, new CurrentPositionChangedEventArgs(lastPositionValue));
        }
    }
}
