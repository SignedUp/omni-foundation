using System.Windows;
using System.Windows.Controls;
using Omniscient.Foundation.ApplicationModel.Presentation.Navigation;
using Omniscient.Foundation.ApplicationModel.Presentation;
using System.Collections.Generic;
using System.Windows.Media;

namespace Omniscient.Foundation.Contrib.Silverlight
{
    public partial class ViewNavigator : UserControl
    {
        private ForwardsClosingViewNavigator _navigator;
        private List<Button> _buttons;

        public ViewNavigator()
        {
            InitializeComponent();
            
            _buttons = new List<Button>();
            _navigator = new ForwardsClosingViewNavigator();
            _navigator.AddedView += new System.EventHandler<AddedViewEventArgs>(AddedView);
            _navigator.ClosedForwards += new System.EventHandler(ClosedForwards);
            _navigator.CurrentPositionChanged += new System.EventHandler<CurrentPositionChangedEventArgs>(CurrentPositionChanged);
            _navigator.CanGoBackChanged += new System.EventHandler(CanGoBackChanged);
            _navigator.CanGoForwardChanged += new System.EventHandler(CanGoForwardChanged);

            ButtonBack.IsEnabled = false;
            ButtonForward.IsEnabled = false;
        }

        public IViewNavigator Navigator
        {
            get { return _navigator; }
        }

        void CanGoForwardChanged(object sender, System.EventArgs e)
        {
            ButtonForward.IsEnabled = _navigator.CanGoForward();
        }

        void CanGoBackChanged(object sender, System.EventArgs e)
        {
            ButtonBack.IsEnabled = _navigator.CanGoBack();
        }

        void CurrentPositionChanged(object sender, CurrentPositionChangedEventArgs e)
        {
            if (e.LastPosition > -1)
            {
                Button last = _buttons[e.LastPosition];
                last.BorderBrush = null;
            }

            Button current = _buttons[_navigator.CurrentPosition];
            current.BorderBrush = new SolidColorBrush(Colors.Blue);
        }

        void ClosedForwards(object sender, System.EventArgs e)
        {
            for (int i = _navigator.CurrentPosition; i < _buttons.Count; i++)
                StackPanelContent.Children.Remove(_buttons[i]);    
        }

        void AddedView(object sender, AddedViewEventArgs e)
        {
            Button b = new Button();
            b.Content = new ViewContent(e.View);
            b.Click += new RoutedEventHandler(NavigationButton_Click);
            StackPanelContent.Children.Add(b);
            _buttons.Add(b);
        }

        void NavigationButton_Click(object sender, RoutedEventArgs e)
        {
            ViewContent content = (ViewContent)((Button)sender).Content;
            _navigator.NavigateTo(content.View);
        }

        public IViewController Controller
        {
            get { return _navigator.ViewController; }
            set { _navigator.ViewController = value; }
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            _navigator.GoBack();
        }

        private void ButtonForward_Click(object sender, RoutedEventArgs e)
        {
            _navigator.GoForward();
        }
    }
}
