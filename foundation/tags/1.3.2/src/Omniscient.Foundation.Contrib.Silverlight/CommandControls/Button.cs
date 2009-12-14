using System.Windows;
using System.Windows.Input;
namespace Omniscient.Foundation.Contrib.Silverlight.CommandControls
{
    public class Button: System.Windows.Controls.Button
    {
        public static readonly DependencyProperty CommandProperty;
        public static readonly DependencyProperty CommandParameterProperty;

        static Button()
        {
            CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(Button),
                new PropertyMetadata(null, new PropertyChangedCallback(OnCommandChanged)));

            CommandParameterProperty = DependencyProperty.Register("CommandParam", typeof(object), typeof(Button),
                new PropertyMetadata(null, new PropertyChangedCallback(OnCommandParamChanged)));
        }

        public Button()
            : base()
        {
            this.Click += new RoutedEventHandler(CommandButton_Click);
        }

        void CommandButton_Click(object sender, RoutedEventArgs e)
        {
            if (Command != null) Command.Execute(this.CommandParam);
        }

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public object CommandParam
        {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        private static void OnCommandChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            Button button = (Button)sender;
            button.UpdateCanExecute();
        }

        private static void OnCommandParamChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            Button button = (Button)sender;
            button.UpdateCanExecute();
        }

        private void UpdateCanExecute()
        {
            bool canExecute = true;

            if (Command == null) canExecute = false;
            if (canExecute) canExecute = Command.CanExecute(CommandParam);
            this.IsEnabled = canExecute;
        }
    }
}
