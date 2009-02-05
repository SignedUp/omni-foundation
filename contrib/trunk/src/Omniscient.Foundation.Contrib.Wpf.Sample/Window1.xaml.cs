using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using WpfPresenters;
using Omniscient.Foundation.Commanding;

namespace Omniscient.Foundation.Contrib.Wpf.Sample
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private NotifyIconPresenter _presenter;

        public Window1()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _presenter.WriteMessage(TextBoxMessage.Text);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Stream ico;
            ico = this.GetType().Assembly.GetManifestResourceStream("Omniscient.Foundation.Contrib.Wpf.Sample.IconStart.ico");
            ImageSource icon = BitmapFrame.Create(ico);
            _presenter = new NotifyIconPresenter(icon);

            ContextMenu menu;
            MenuItem item;
            ContextMenuCommand cmd;

            menu = new ContextMenu();
            item = new MenuItem();
            item.Header = "left click";
            menu.Items.Add(item);
            menu.Placement = System.Windows.Controls.Primitives.PlacementMode.MousePoint;
            cmd = new ContextMenuCommand(menu);
            _presenter.LeftClickCommand = cmd;

            menu = new ContextMenu();
            item = new MenuItem();
            item.Header = "right click";
            menu.Items.Add(item);
            menu.Placement = System.Windows.Controls.Primitives.PlacementMode.MousePoint;
            cmd = new ContextMenuCommand(menu);
            _presenter.RightClickCommand = cmd;
        }
    }

    public class ContextMenuCommand : ICommandCore
    {
        private ContextMenu _menu;

        public ContextMenuCommand(ContextMenu menu)
        {
            _menu = menu;
        }

        #region ICommandCore Members

        public bool CanExecute(object param)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object param)
        {
            Application.Current.MainWindow.Activate();
            if (_menu.IsOpen) _menu.IsOpen = false;
            else _menu.IsOpen = true;
        }

        #endregion
    }
}
