using System;
using System.Collections.Generic;

namespace Omniscient.Foundation.ApplicationModel.Presentation.Navigation
{
    public class ForwardsClosingViewNavigator: ViewNavigatorBase
    {
        public event EventHandler ClosedForwards;

        public ForwardsClosingViewNavigator(IViewController controller) : base(controller) 
        {
            ClosedForwards = delegate { };
        }
        public ForwardsClosingViewNavigator()
            : base()
        {
            ClosedForwards = delegate { };
        }

        public override void NavigateTo(IView view)
        {
            if (view == null) throw new ArgumentNullException("view");
            if (CurrentView == view) return;

            //if the view is already registered, then simply focus on it.
            for (int i = 0; i < RegisteredViews.Count; i++)
            {
                if (RegisteredViews[i] == view)
                {
                    int lastPos = Cursor;
                    Cursor = i;
                    ViewController.Focus(RegisteredViews[i]);
                    OnCurrentPositionChanged(lastPos);
                    return;
                }
            }

            //this is a new view; it's never been opened before.  If we are at the end of the row, then
            //simply add the view to the list.  Otherwise, we have to close all views in front of the current one.
            if (!this.CanGoForward())
            {
                RegisteredViews.Add(view);
                OnAddedView(view, this.CurrentPosition + 1);
                GoForward();
            }
            else
            {
                //Get the views that will be closed
                List<IView> forwards = new List<IView>(GetForwards());

                //ask the ViewController to close the views
                if (ViewController.CloseViewRange(forwards))
                {
                    //remove the closed views from the list
                    foreach (IView v in forwards) RegisteredViews.Remove(v);
                    OnClosedForwards();

                    //add the newly added item, and focus it
                    RegisteredViews.Add(view);
                    OnAddedView(view, this.CurrentPosition + 1);
                    GoForward();
                }
            }
        }

        private IEnumerable<IView> GetForwards()
        {
            for (int i = Cursor + 1; i < RegisteredViews.Count; i++)
                yield return RegisteredViews[i];
        }

        protected virtual void OnClosedForwards()
        {
            ClosedForwards(this, EventArgs.Empty);
        }

    }
}
