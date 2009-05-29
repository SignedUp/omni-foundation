using System.Windows;
using System.Windows.Controls;
using Omniscient.Foundation.ApplicationModel.Presentation.Navigation;
using Omniscient.Foundation.ApplicationModel.Presentation;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System;

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
            current.Focus();
        }

        void ClosedForwards(object sender, System.EventArgs e)
        {
            for (int i = _navigator.CurrentPosition + 1; i < _buttons.Count; i++)
            {
                StackPanelContent.Children.Remove(_buttons[i]);
            }
            _buttons.RemoveRange(_navigator.CurrentPosition + 1, _buttons.Count - (_navigator.CurrentPosition + 1));
        }

        void AddedView(object sender, AddedViewEventArgs e)
        {
            NavigationButton button = new NavigationButton();
            StackPanel panel = new StackPanel();
            
            TextBlock title = new TextBlock();
            title.TextAlignment = TextAlignment.Center;
            title.Text = e.View.Title;
            panel.Children.Add(title);

            Image icon = new Image();
            icon.Source = new BitmapImage(new Uri("/Omniscient.Foundation.Contrib.Silverlight;Component/Resources/application_side_boxes.png", UriKind.Relative));
            icon.Stretch = Stretch.Fill;
            ContentControl iconPlaceholder = new ContentControl();
            iconPlaceholder.Height = 24.0;
            iconPlaceholder.Width = 24.0;
            iconPlaceholder.Content = icon;
            iconPlaceholder.Margin = new Thickness(3.0);
            panel.Children.Add(iconPlaceholder);

            button.Margin = new Thickness(0, 0, 3, 0);
            button.Content = panel;
            button.View = e.View;
            
            button.Click += new RoutedEventHandler(NavigationButton_Click);
            StackPanelContent.Children.Add(button);
            _buttons.Add(button);
        }

        void NavigationButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationButton button = (NavigationButton)sender;
            _navigator.NavigateTo(button.View);
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
