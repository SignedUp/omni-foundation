using System;
using System.Windows;
using System.Windows.Controls;
using Omniscient.Foundation.ApplicationModel;
using Omniscient.Foundation.Commanding;

namespace Omniscient.Foundation.Contrib.Silverlight.Dialogs
{
    [TemplatePart(Name = "ButtonElement", Type = typeof(Button))]
    public class DialogButton : Control
    {
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(DialogButton), new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(string), typeof(DialogButton), new PropertyMetadata(null));

        private Button buttonElement;
        private ICommandCore command;

        public DialogButton()
        {
            DefaultStyleKey = typeof(DialogButton);

            Click = delegate { };
        }

        public event RoutedEventHandler Click;

        public string Text
        {
            get { return (string) GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public string Command
        {
            get { return (string) GetValue(CommandProperty); }
            set
            {
                SetValue(CommandProperty, value);

                if (!string.IsNullOrEmpty(value))
                {
                    command = ApplicationManager.Current.CommandStore[value];

                    if (command != null)
                    {
                        IsEnabled = command.CanExecute(null);
                        command.CanExecuteChanged += command_CanExecuteChanged;
                    }
                }
                else
                {
                    command = null;
                }
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            buttonElement = (Button) GetTemplateChild("ButtonElement");
            buttonElement.Click += buttonElement_Click;
        }

        private void buttonElement_Click(object sender, RoutedEventArgs e)
        {
            Click(this, e);
        }

        private void command_CanExecuteChanged(object sender, EventArgs e)
        {
            if (command != null)
            {
                IsEnabled = command.CanExecute(null);
            }
        }
    }
}