using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Omniscient.Foundation.Commanding;

namespace Omniscient.Foundation.ApplicationModel.Presentation.Navigation
{
    public interface IViewNavigator
    {
        bool CanGoBack();
        bool CanGoForward();
        void GoBack();
        void GoForward();

        void NavigateTo(IView v);
        IView CurrentView { get; }
        int Count { get; }
        int CurrentPosition { get; }

        event EventHandler<CurrentPositionChangedEventArgs> CurrentPositionChanged;
        event EventHandler<AddedViewEventArgs> AddedView;
        event EventHandler CanGoBackChanged;
        event EventHandler CanGoForwardChanged;
    }

    public class CurrentPositionChangedEventArgs : EventArgs
    {
        public CurrentPositionChangedEventArgs(int lastPositionValue)
        {
            LastPosition = lastPositionValue;
        }

        public int LastPosition { get; private set; }
    }

    public class AddedViewEventArgs : EventArgs
    {
        public AddedViewEventArgs(IView view, int position)
        {
            Position = position;
            View = view;
        }

        public int Position { get; private set; }

        public IView View { get; private set; }
    }
}
