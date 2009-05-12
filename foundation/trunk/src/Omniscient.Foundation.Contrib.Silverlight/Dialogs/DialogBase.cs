using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Omniscient.Foundation.ApplicationModel;
using Omniscient.Foundation.Commanding;

namespace Omniscient.Foundation.Contrib.Silverlight.Dialogs
{
    [TemplatePart(Name = "RootElement", Type = typeof(Border))]
    [TemplatePart(Name = "CloseButtonElement", Type = typeof(Button))]
    [TemplatePart(Name = "FirstButtonElement", Type = typeof(DialogButton))]
    [TemplatePart(Name = "SecondButtonElement", Type = typeof(DialogButton))]
    [TemplatePart(Name = "ThirdButtonElement", Type = typeof(DialogButton))]
    public class DialogBase : Control 
    {
        public static readonly DependencyProperty DialogBackgroundProperty =
            DependencyProperty.Register("DialogBackground", typeof(Brush), typeof(DialogBase), new PropertyMetadata(new SolidColorBrush(Colors.White)));

        public static readonly DependencyProperty HeaderBackgroundProperty =
            DependencyProperty.Register("HeaderBackground", typeof(Brush), typeof(DialogBase), new PropertyMetadata(new SolidColorBrush(Colors.Gray)));

        public static readonly DependencyProperty HeaderTextProperty =
            DependencyProperty.Register("HeaderText", typeof(string), typeof(DialogBase), new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(object), typeof(DialogBase), new PropertyMetadata(null));

        public static readonly DependencyProperty FirstButtonTextProperty =
            DependencyProperty.Register("FirstButtonText", typeof(string), typeof(DialogBase), new PropertyMetadata("Ok"));

        public static readonly DependencyProperty FirstButtonCommandProperty =
            DependencyProperty.Register("FirstButtonCommand", typeof(string), typeof(DialogBase), new PropertyMetadata(null));

        public static readonly DependencyProperty FirstButtonVisibilityProperty =
            DependencyProperty.Register("FirstButtonVisibility", typeof(Visibility), typeof(DialogBase), new PropertyMetadata(Visibility.Visible));

        public static readonly DependencyProperty SecondButtonTextProperty =
            DependencyProperty.Register("SecondButtonText", typeof(string), typeof(DialogBase), new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty SecondButtonCommandProperty =
            DependencyProperty.Register("SecondButtonCommand", typeof(string), typeof(DialogBase), new PropertyMetadata(null));

        public static readonly DependencyProperty SecondButtonVisibilityProperty =
            DependencyProperty.Register("SecondButtonVisibility", typeof(Visibility), typeof(DialogBase), new PropertyMetadata(Visibility.Collapsed));

        public static readonly DependencyProperty ThirdButtonTextProperty =
            DependencyProperty.Register("ThirdButtonText", typeof(string), typeof(DialogBase), new PropertyMetadata("Cancel"));

        public static readonly DependencyProperty ThirdButtonCommandProperty =
            DependencyProperty.Register("ThirdButtonCommand", typeof(string), typeof(DialogBase), new PropertyMetadata(null));

        public static readonly DependencyProperty ThirdButtonVisibilityProperty =
            DependencyProperty.Register("ThirdButtonVisibility", typeof(Visibility), typeof(DialogBase), new PropertyMetadata(Visibility.Visible));

        private Button closeButtonElement;
        //private ButtonLayout? dialogButtonLayout;
        private DialogButton firstButtonElement;
        private DialogButton secondButtonElement;
        private DialogButton thirdButtonElement;

        public DialogBase()
        {
            DefaultStyleKey = typeof(DialogBase);

            DialogClosing = delegate { };
        }

        public event EventHandler DialogClosing;

        public Brush DialogBackground
        {
            get { return (Brush) GetValue(DialogBackgroundProperty); }
            set { SetValue(DialogBackgroundProperty, value); }
        }

        public Brush HeaderBackground
        {
            get { return (Brush) GetValue(HeaderBackgroundProperty); }
            set { SetValue(HeaderBackgroundProperty, value); }
        }

        public object Content
        {
            get { return GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        public string HeaderText
        {
            get { return (string) GetValue(HeaderTextProperty); }
            set { SetValue(HeaderTextProperty, value); }
        }

        //public ButtonLayout? DialogButtonLayout
        //{
        //    get { return dialogButtonLayout; }
        //    set
        //    {
        //        dialogButtonLayout = value;

        //        if (value == null) return;

        //        if (firstButtonElement == null || secondButtonElement == null || thirdButtonElement == null) return;

        //        switch (dialogButtonLayout)
        //        {
        //            case ButtonLayout.None:
        //                FirstButtonVisibility = Visibility.Collapsed;
        //                SecondButtonVisibility = Visibility.Collapsed;
        //                ThirdButtonVisibility = Visibility.Collapsed;
        //                break;
        //            case ButtonLayout.Ok:
        //                FirstButtonText = "Ok";
        //                FirstButtonVisibility = Visibility.Visible;
        //                SecondButtonVisibility = Visibility.Collapsed;
        //                ThirdButtonVisibility = Visibility.Collapsed;
        //                break;
        //            case ButtonLayout.YesNo:
        //                FirstButtonText = "Yes";
        //                FirstButtonVisibility = Visibility.Visible;
        //                SecondButtonText = "No";
        //                SecondButtonVisibility = Visibility.Visible;
        //                ThirdButtonVisibility = Visibility.Collapsed;
        //                break;
        //            case ButtonLayout.YesNoCancel:
        //                FirstButtonText = "Yes";
        //                FirstButtonVisibility = Visibility.Visible;
        //                SecondButtonText = "No";
        //                SecondButtonVisibility = Visibility.Visible;
        //                ThirdButtonText = "Cancel";
        //                ThirdButtonVisibility = Visibility.Visible;
        //                break;
        //            default:
        //                FirstButtonText = "Ok";
        //                FirstButtonVisibility = Visibility.Visible;
        //                SecondButtonVisibility = Visibility.Collapsed;
        //                ThirdButtonText = "Cancel";
        //                ThirdButtonVisibility = Visibility.Visible;
        //                break;
        //        }
        //    }
        //}

        public string FirstButtonText
        {
            get { return (string) GetValue(FirstButtonTextProperty); }
            set
            {
                SetValue(FirstButtonTextProperty, value);

                if (firstButtonElement != null)
                {
                    firstButtonElement.Text = value;
                }
            }
        }

        public string FirstButtonCommand
        {
            get { return (string)GetValue(FirstButtonCommandProperty); }
            set
            {
                SetValue(FirstButtonCommandProperty, value);

                if (firstButtonElement != null)
                {
                    firstButtonElement.Command = value;
                }
            }
        }

        public Visibility FirstButtonVisibility
        {
            get { return (Visibility) GetValue(FirstButtonVisibilityProperty); }
            set { SetValue(FirstButtonVisibilityProperty, value); }
        }

        public string SecondButtonText
        {
            get { return (string)GetValue(SecondButtonTextProperty); }
            set
            {
                SetValue(SecondButtonTextProperty, value);

                if (secondButtonElement != null)
                {
                    secondButtonElement.Text = value;
                }
            }
        }

        public string SecondButtonCommand
        {
            get { return (string) GetValue(SecondButtonCommandProperty); }
            set
            {
                SetValue(SecondButtonCommandProperty, value);

                if (secondButtonElement != null)
                {
                    secondButtonElement.Command = value;
                }
            }
        }

        public Visibility SecondButtonVisibility
        {
            get { return (Visibility)GetValue(SecondButtonVisibilityProperty); }
            set { SetValue(SecondButtonVisibilityProperty, value); }
        }

        public string ThirdButtonText
        {
            get { return (string)GetValue(ThirdButtonTextProperty); }
            set
            {
                SetValue(ThirdButtonTextProperty, value);

                if (thirdButtonElement != null)
                {
                    thirdButtonElement.Text = value;
                }
            }
        }

        public string ThirdButtonCommand
        {
            get { return (string) GetValue(ThirdButtonCommandProperty); }
            set
            {
                SetValue(ThirdButtonCommandProperty, value);

                if (thirdButtonElement != null)
                {
                    thirdButtonElement.Command = value;
                }
            }
        }

        public Visibility ThirdButtonVisibility
        {
            get { return (Visibility)GetValue(ThirdButtonVisibilityProperty); }
            set { SetValue(ThirdButtonVisibilityProperty, value); }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            closeButtonElement = (Button) GetTemplateChild("CloseButtonElement");
            closeButtonElement.Click += closeButtonElement_Click;

            firstButtonElement = (DialogButton) GetTemplateChild("FirstButtonElement");
            firstButtonElement.Text = FirstButtonText;
            firstButtonElement.Command = FirstButtonCommand;
            firstButtonElement.Click += firstButtonElement_Click;

            secondButtonElement = (DialogButton) GetTemplateChild("SecondButtonElement");
            secondButtonElement.Text = SecondButtonText;
            secondButtonElement.Command = SecondButtonCommand;
            secondButtonElement.Click += secondButtonElement_Click;

            thirdButtonElement = (DialogButton) GetTemplateChild("ThirdButtonElement");
            thirdButtonElement.Text = ThirdButtonText;
            thirdButtonElement.Command = ThirdButtonCommand;
            thirdButtonElement.Click += thirdButtonElement_Click;

            //DialogButtonLayout = DialogButtonLayout;
        }

        private void thirdButtonElement_Click(object sender, RoutedEventArgs e)
        {
            ExecuteButtonCommand(sender, e, thirdButtonElement);
        }

        private void secondButtonElement_Click(object sender, RoutedEventArgs e)
        {
            ExecuteButtonCommand(sender, e, secondButtonElement);
        }

        private void firstButtonElement_Click(object sender, RoutedEventArgs e)
        {
            ExecuteButtonCommand(sender, e, firstButtonElement);
        }

        private void closeButtonElement_Click(object sender, RoutedEventArgs e)
        {
            DialogClosing(this, EventArgs.Empty);
        }

        private void ExecuteButtonCommand(object sender, EventArgs e, DialogButton button)
        {
            if (button != null)
            {
                if (!string.IsNullOrEmpty(button.Command))
                {
                    ICommandCore command = ApplicationManager.Current.CommandStore[button.Command];

                    if (command != null)
                    {
                        if (command.CanExecute(null))
                        {
                            command.Execute(null);
                        }
                    }
                }
            }

            DialogClosing(sender, e);
        }
    }
}