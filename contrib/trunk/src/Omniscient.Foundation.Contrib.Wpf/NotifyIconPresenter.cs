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
using System.Collections.ObjectModel;
using Omniscient.Foundation.Contrib.Wpf.Commands;

namespace Omniscient.Foundation.Contrib.Wpf
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
        private ObservableHierarchicalCommandObject _leftClickMenu, _rightClickMenu;
                                                                                       
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
        /// Creates a contextual menu and associates it to the left- or right-click command.  Uses the <see cref="StandardCommandContextMenu"/>.
        /// To use your own menu, with custom data template and style, call the overload that takes a menu.
        /// </summary>
        /// <param name="mouseClickButton">Either <see cref="MouseButton.Left"/> or <see cref="MouseButton.Right"/>.  The contextual menu will
        /// be associated with that button's click event.</param>
        /// <exception cref="ArgumentException">Raised if <paramref name="mouseClickButton"/> is neither <see cref="MouseButton.Left"/> nor <see cref="MouseButton.Right"/>.</exception>
        public ObservableHierarchicalCommandObject CreateHierarchicalMenu(MouseButton mouseClickButton)
        {
            ContextMenu menu;
            menu = new StandardCommandContextMenu();
            return CreateHierarchicalMenu(mouseClickButton, menu);
        }

        /// <summary>
        /// Creates a contextual menu and associates it to the left- or right-click command.  That method will bind a new
        /// <see cref="ObservableHierarchicalCommandObject"/> to the menu; essentially, you call this overload only when you want
        /// to provide you own template and style for the menu.  Otherwise, call the overload that takes a single argument, or pass
        /// a <see cref="StandardCommandContextMenu"/> which has all the templates it needs to build the menu correctly.
        /// </summary>
        /// <param name="mouseClickButton">Either <see cref="MouseButton.Left"/> or <see cref="MouseButton.Right"/>.  The contextual menu will
        /// be associated with that button's click event.</param>
        /// <param name="menu">A context menu.  By default, you should use <see cref="StandardCommandContextMenu"/>.</param>
        /// <exception cref="ArgumentException">Raised if <paramref name="mouseClickButton"/> is neither <see cref="MouseButton.Left"/> nor <see cref="MouseButton.Right"/>.</exception>
        /// <exception cref="ArgumentNullException">Raised if <paramref name="menu"/> is null.</exception>
        public ObservableHierarchicalCommandObject CreateHierarchicalMenu(MouseButton mouseClickButton, ContextMenu menu)
        {
            if (mouseClickButton != MouseButton.Left && mouseClickButton != MouseButton.Right)
                throw new ArgumentException(string.Format("Mouse button {0} not supported.", mouseClickButton.ToString()));

            if (menu == null) throw new ArgumentNullException("menu");

            ObservableHierarchicalCommandObject menuItems;
            ContextMenuCommand cmd;

            menu.Placement = System.Windows.Controls.Primitives.PlacementMode.MousePoint;
            cmd = new ContextMenuCommand(menu);
            menuItems = new ObservableHierarchicalCommandObject();
            menu.ItemsSource = menuItems.Children;

            if (mouseClickButton == MouseButton.Left)
            {
                _leftClickMenu = menuItems;
                this.LeftClickCommand = cmd;
            }
            else
            {
                _rightClickMenu = menuItems;
                this.RightClickCommand = cmd;
            }
            return menuItems;
        }

        public ObservableHierarchicalCommandObject LeftClickMenu
        {
            get { return _leftClickMenu; }
        }

        public ObservableHierarchicalCommandObject RightClickMenu
        {
            get { return _rightClickMenu; }
        }

        public HierarchicalDataTemplate MenuTemplate
        {
            get;
            set;
        }

        public Style MenuStyle
        {
            get;
            set;
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
