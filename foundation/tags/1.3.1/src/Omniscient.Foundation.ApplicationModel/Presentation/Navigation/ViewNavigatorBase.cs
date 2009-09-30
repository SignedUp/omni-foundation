using System;
using System.Collections.Generic;
using Omniscient.Foundation.Commanding;

namespace Omniscient.Foundation.ApplicationModel.Presentation.Navigation
{
    public abstract class ViewNavigatorBase: IViewNavigator
    {
        public event EventHandler<CurrentPositionChangedEventArgs> CurrentPositionChanged;
        public event EventHandler<AddedViewEventArgs> AddedView;
        public event EventHandler CanGoBackChanged;
        public event EventHandler CanGoForwardChanged;

        public ViewNavigatorBase()
        {
            Cursor = -1;
            RegisteredViews = new List<IView>();

            //Initialize the event handlers so we don't have to do a null check
            CurrentPositionChanged = delegate { };
            AddedView = delegate { };
            CanGoBackChanged = delegate { };
            CanGoForwardChanged = delegate { };
        }

        public ViewNavigatorBase(IViewController controller): this()
        {
            ViewController = controller;
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

        public IViewController ViewController
        {
            get;
            set;
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

        protected virtual void OnCurrentPositionChanged(int lastPositionValue)
        {
            CurrentPositionChanged(this, new CurrentPositionChangedEventArgs(lastPositionValue));

            int limit = 0;
            if (lastPositionValue > limit && CurrentPosition == limit || lastPositionValue == limit && CurrentPosition > limit) CanGoBackChanged(this, EventArgs.Empty);
            limit = RegisteredViews.Count - 1;
            if (lastPositionValue < limit && CurrentPosition == limit || lastPositionValue == limit && CurrentPosition < limit) CanGoForwardChanged(this, EventArgs.Empty);
        }

        protected virtual void OnAddedView(IView view, int position)
        {
            AddedView(this, new AddedViewEventArgs(view, position));
        }
    }
}
