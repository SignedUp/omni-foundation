using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Omniscient.Foundation.ApplicationModel.Presentation;
using System.Windows;
using Avalon.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;
using Omniscient.Foundation.Commanding;
using System.Windows.Controls;
using System.Diagnostics;
using System.Windows.Input;

namespace WpfPresenters
{
    /// <summary>
    /// That presenter displays an icon in the notification area. Messages sent to the presenter are displayed in a balloon that floats
    /// above the notification area.  It can execute commands when the user clicks or double-clicks on the notification icon and/or the 
    /// balloon.
    /// </summary>
    /// <remarks>
    /// Look at project Development\Omniscient.Foundation.Contrib.Wpf.Sample for an example on how to use the presenter.
    /// </remarks>
    public class NotifyIconPresenter: IPresenter
    {
        private NotifyIcon _icon;
                                                                                               
        /// <summary>
        /// Creates the presenter with a primary icon to display in the notification area.
        /// </summary>
        /// <param name="icon">The icon to use at first display.</param>
        public NotifyIconPresenter(ImageSource icon)
        {
            _icon = new NotifyIcon();
            SetIcon(icon);

            //set to hidden because the icon is not part of the visual tree.
            //to hide (close), put visibility to "Collapsed".
            _icon.Visibility = Visibility.Hidden;

            _icon.MouseClick += new System.Windows.Input.MouseButtonEventHandler(_icon_MouseClick);
            _icon.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(_icon_MouseDoubleClick);
            _icon.BalloonTipClick += new RoutedEventHandler(_icon_BalloonTipClick);
        }

        void _icon_BalloonTipClick(object sender, RoutedEventArgs e)
        {
            if (BalloonClickCommand != null && BalloonClickCommand.CanExecute(sender)) BalloonClickCommand.Execute(sender);
        }

        void _icon_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left && LeftDoubleClickCommand != null && LeftDoubleClickCommand.CanExecute(sender))
            {
                LeftClickCommand.Execute(sender);
            }
            else if (e.ChangedButton == MouseButton.Right && RightDoubleClickCommand != null && RightDoubleClickCommand.CanExecute(sender))
            {
                RightClickCommand.Execute(sender);
            }
        }

        void _icon_MouseClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left && LeftClickCommand != null && LeftClickCommand.CanExecute(sender))
            {
                LeftClickCommand.Execute(sender);
            }
            else if (e.ChangedButton == MouseButton.Right && RightClickCommand != null && RightClickCommand.CanExecute(sender))
            {
                RightClickCommand.Execute(sender);
            }
        }

        /// <summary>
        /// Sets the icon to a new image.
        /// </summary>
        /// <param name="icon">The icon to display in the notification area</param>
        public void SetIcon(ImageSource icon)
        {
            _icon.Icon = icon;
        }

        /// <summary>
        /// Gets or sets the command to run when the user left-clicks the icon.  This will execute even when it's double-clicked.
        /// </summary>
        public ICommandCore LeftClickCommand
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the command to run when the user left-double-clicks the icon.  This will also execute the LeftClick command.
        /// </summary>
        public ICommandCore LeftDoubleClickCommand
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the command to run when the user right-clicks the icon.  This will execute even when it's double-clicked.
        /// </summary>
        public ICommandCore RightClickCommand
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the command to run when the user right-double-clicks the icon.  This will also execute the RightClick command.
        /// </summary>
        public ICommandCore RightDoubleClickCommand
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the command to run when the user clicks the message balloon.
        /// </summary>
        public ICommandCore BalloonClickCommand
        {
            get;
            set;
        }

        #region IPresenter Members

        /// <summary>
        /// Gets the name of the presenter.
        /// </summary>
        public string Name
        {
            get { return this.GetType().Name; }
        }

        /// <summary>
        /// Returns True.
        /// </summary>
        public bool RequiresUserInput
        {
            get { return true; }
        }

        /// <summary>
        /// Displays a message in a balloon that floats above the notification area.  The user can click that balloon, executing
        /// the <see cref="BalloonClickCommand"/> command.
        /// </summary>
        /// <param name="message">A message to display.  The presenter will call ToString() on that object.  If null, does nothing.</param>
        public void WriteMessage(object message)
        {
            if (message == null) return;
            //_icon.BalloonTipText = message.ToString();
            //_icon.BalloonTipTitle = message.ToString();
            _icon.ShowBalloonTip(300, message.ToString(), message.ToString(), NotifyBalloonIcon.Info);
        }

        #endregion
    }
}
