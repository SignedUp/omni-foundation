using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    }

    public class CurrentPositionChangedEventArgs : EventArgs
    {
        public CurrentPositionChangedEventArgs(int lastPositionValue)
        {
            LastPosition = lastPositionValue;
        }

        public int LastPosition { get; private set; }
    }

}
