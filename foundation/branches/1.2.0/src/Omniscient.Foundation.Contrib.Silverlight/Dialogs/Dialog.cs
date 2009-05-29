using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Omniscient.Foundation.Contrib.Silverlight.Dialogs
{
    [TemplatePart(Name = "RootElement", Type = typeof(Grid))]
    [TemplatePart(Name = "ShadowElement", Type = typeof(Rectangle))]
    [TemplatePart(Name = "DialogElement", Type = typeof(DialogBase))]
    public class Dialog : Control
    {
        public static readonly DependencyProperty ShadowBackgroundProperty =
            DependencyProperty.Register("ShadowBackground", typeof(Brush), typeof(Dialog), new PropertyMetadata(new SolidColorBrush(Colors.Black)));

        private Grid rootElement;
        private Rectangle shadowElement;
        private DialogBase dialogElement;

        private bool isOpen;
        private bool isModal;
        private object content;
        private double dialogWidth;
        private double dialogHeight;
        private Brush dialogBackground;
        private Brush headerBackground;
        private string headerText;
        //private ButtonLayout? dialogButtonLayout;
        private string firstButtonText;
        private string firstButtonCommand;
        private Visibility firstButtonVisibility;
        private string secondButtonText;
        private string secondButtonCommand;
        private Visibility secondButtonVisibility;
        private string thirdButtonText;
        private string thirdButtonCommand;
        private Visibility thirdButtonVisibility;
        
        private double positionX;
        private double positionY;

        public Dialog()
        {
            DefaultStyleKey = typeof(Dialog);

            IsModal = false;
            DialogWidth = 200;
            DialogHeight = 100;
            DialogBackground = new SolidColorBrush(Colors.White);
            HeaderBackground = new SolidColorBrush(Colors.Gray);
            HeaderText = string.Empty;
            Content = null;
            //DialogButtonLayout = null;
            FirstButtonText = string.Empty;
            FirstButtonCommand = null;
            FirstButtonVisibility = Visibility.Visible;
            SecondButtonText = string.Empty;
            SecondButtonCommand = null;
            ThirdButtonText = string.Empty;
            ThirdButtonCommand = null;
        }

        public bool IsOpen
        {
            get { return isOpen; }
            set
            {
                isOpen = value;

                if (rootElement != null)
                {
                    rootElement.Visibility = isOpen ? Visibility.Visible : Visibility.Collapsed;
                }
            }
        }

        public Brush ShadowBackground
        {
            get { return (Brush) GetValue(ShadowBackgroundProperty); }
            set { SetValue(ShadowBackgroundProperty, value); }
        }

        public bool IsModal
        {
            get { return isModal; }
            set
            {
                isModal = value;

                if (shadowElement != null)
                {
                    shadowElement.Visibility = isModal ? Visibility.Visible : Visibility.Collapsed;
                }
            }
        }

        public double DialogWidth
        {
            get { return dialogWidth; }
            set
            {
                dialogWidth = value;

                if (dialogElement != null)
                {
                    dialogElement.Width = value;
                }
            }
        }

        public double DialogHeight
        {
            get { return dialogHeight; }
            set
            {
                dialogHeight = value;

                if (dialogElement != null)
                {
                    dialogElement.Height = value;
                }
            }
        }

        public Brush DialogBackground
        {
            get { return dialogBackground; }
            set
            {
                dialogBackground = value;

                if (dialogElement != null)
                {
                    dialogElement.DialogBackground = value;
                }
            }
        }

        public Brush HeaderBackground
        {
            get { return headerBackground; }
            set
            {
                headerBackground = value;

                if (dialogElement != null)
                {
                    dialogElement.HeaderBackground = value;
                }
            }
        }

        public string HeaderText
        {
            get { return headerText; }
            set
            {
                headerText = value;

                if (dialogElement != null)
                {
                    dialogElement.HeaderText = value;
                }
            }
        }

        public object Content
        {
            get { return content; }
            set
            {
                content = value;

                if (dialogElement != null)
                {
                    dialogElement.Content = value;
                }
            }
        }

        //public ButtonLayout? DialogButtonLayout
        //{
        //    get { return dialogButtonLayout; }
        //    set
        //    {
        //        dialogButtonLayout = value;

        //        if (dialogElement != null)
        //        {
        //            dialogElement.DialogButtonLayout = value;
        //        }
        //    }
        //}

        public string FirstButtonText
        {
            get { return firstButtonText; }
            set
            {
                firstButtonText = value;

                if (dialogElement != null)
                {
                    dialogElement.FirstButtonText = value;
                }
            }
        }

        public string FirstButtonCommand
        {
            get { return firstButtonCommand; }
            set
            {
                firstButtonCommand = value;

                if (dialogElement != null)
                {
                    dialogElement.FirstButtonCommand = value;
                }
            }
        }

        public Visibility FirstButtonVisibility
        {
            get { return firstButtonVisibility; }
            set
            {
                firstButtonVisibility = value;

                if (dialogElement != null)
                {
                    dialogElement.FirstButtonVisibility = value;
                }
            }
        }

        public string SecondButtonText
        {
            get { return secondButtonText; }
            set
            {
                secondButtonText = value;

                if (dialogElement != null)
                {
                    dialogElement.SecondButtonText = value;
                }
            }
        }

        public string SecondButtonCommand
        {
            get { return secondButtonCommand; }
            set
            {
                secondButtonCommand = value;

                if (dialogElement != null)
                {
                    dialogElement.SecondButtonCommand = value;
                }
            }
        }

        public Visibility SecondButtonVisibility
        {
            get { return secondButtonVisibility; }
            set
            {
                secondButtonVisibility = value;

                if (dialogElement != null)
                {
                    dialogElement.SecondButtonVisibility = value;
                }
            }
        }

        public string ThirdButtonText
        {
            get { return thirdButtonText; }
            set
            {
                thirdButtonText = value;

                if (dialogElement != null)
                {
                    dialogElement.ThirdButtonText = value;
                }
            }
        }

        public string ThirdButtonCommand
        {
            get { return thirdButtonCommand; }
            set
            {
                thirdButtonCommand = value;

                if (dialogElement != null)
                {
                    dialogElement.ThirdButtonCommand = value;
                }
            }
        }

        public Visibility ThirdButtonVisibility
        {
            get { return thirdButtonVisibility; }
            set
            {
                thirdButtonVisibility = value;

                if (dialogElement != null)
                {
                    dialogElement.ThirdButtonVisibility = value;
                }
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            rootElement = (Grid) GetTemplateChild("RootElement");
            rootElement.Visibility = IsOpen ? Visibility.Visible : Visibility.Collapsed;
            rootElement.SizeChanged += rootElement_SizeChanged;

            shadowElement = (Rectangle) GetTemplateChild("ShadowElement");
            shadowElement.Visibility = IsModal ? Visibility.Visible : Visibility.Collapsed;

            dialogElement = (DialogBase) GetTemplateChild("DialogElement");
            dialogElement.Width = DialogWidth;
            dialogElement.Height = DialogHeight;
            dialogElement.DialogBackground = DialogBackground;
            dialogElement.HeaderBackground = HeaderBackground;
            dialogElement.HeaderText = HeaderText;
            dialogElement.Content = Content;
            dialogElement.FirstButtonText = FirstButtonText;
            dialogElement.FirstButtonCommand = FirstButtonCommand;
            dialogElement.FirstButtonVisibility = FirstButtonVisibility;
            dialogElement.SecondButtonText = SecondButtonText;
            dialogElement.SecondButtonCommand = SecondButtonCommand;
            dialogElement.SecondButtonVisibility = SecondButtonVisibility;
            dialogElement.ThirdButtonText = ThirdButtonText;
            dialogElement.ThirdButtonCommand = ThirdButtonCommand;
            dialogElement.ThirdButtonVisibility = ThirdButtonVisibility;
            //dialogElement.DialogButtonLayout = DialogButtonLayout;
            dialogElement.DialogClosing += dialogElement_DialogClosing;

            PositionDialog();
        }

        public void ShowDialog()
        {
            IsOpen = true;
            IsModal = false;
        }

        public void ShowDialogAsModal()
        {
            IsOpen = true;
            IsModal = true;
        }

        public void CloseDialog()
        {
            IsOpen = false;
        }

        private void rootElement_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            PositionDialog();
        }

        private void dialogElement_DialogClosing(object sender, EventArgs e)
        {
            CloseDialog();
        }

        private void PositionDialog()
        {
            positionX = (Application.Current.Host.Content.ActualWidth / 2) - (dialogElement.Width / 2);
            positionY = (Application.Current.Host.Content.ActualHeight / 2) - (dialogElement.Height / 2);

            Canvas.SetLeft(dialogElement, positionX);
            Canvas.SetTop(dialogElement, positionY);
        }
    }
}