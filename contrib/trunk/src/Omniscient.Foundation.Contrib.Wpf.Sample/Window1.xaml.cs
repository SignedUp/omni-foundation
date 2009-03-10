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
using Omniscient.Foundation.Commanding;
using Omniscient.Foundation.Contrib.Wpf.Commands;
using Omniscient.Foundation.ApplicationModel.Presentation;

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
            //_presenter.LeftClickMenu.Children[1].Children[0].Children.Add(new ObservableHierarchicalCommandObject() { Text = "myItem 2.1.1" });
            //_presenter.LeftClickMenu.Children[1].Children[0].Children.Add(new ObservableHierarchicalCommandObject() { Text = "myItem 2.1.2" });
            //_presenter.LeftClickMenu.Children[1].Text = "hahahahahahah";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Stream ico;
            ObservableHierarchicalCommandObject cmdItem;
    
            ico = this.GetType().Assembly.GetManifestResourceStream("Omniscient.Foundation.Contrib.Wpf.Sample.IconStart.ico");
            ImageSource icon = BitmapFrame.Create(ico);
            _presenter = new NotifyIconPresenter(icon);

            cmdItem = _presenter.CreateHierarchicalMenu(MouseButton.Left);
            cmdItem.Children.Add(new ObservableHierarchicalCommandObject() { Text = "do something" });

            //cmdItem = _presenter.CreateHierarchicalMenu(MouseButton.Left);
            //cmdItem.Children.Add(new ObservableHierarchicalCommandObject() { Text = "myItem 1" });
            //cmdItem.Children.Add(new ObservableHierarchicalCommandObject() { Text = "myItem 2", Command = new MessageboxCommand("myItem 2 message") });
            //cmdItem.Children[0].Children.Add(new ObservableHierarchicalCommandObject() { Text = "myItem 1.1", Command = new MessageboxCommand("myItem 1.1 message") });
            //cmdItem.Children[0].Children.Add(new ObservableHierarchicalCommandObject() { Text = "myItem 1.2", Command = new MessageboxCommand("myItem 1.2 message") });
            //cmdItem.Children[0].Children.Add(new ObservableHierarchicalCommandObject() { Text = "myItem 1.3" });
            //cmdItem.Children[1].Children.Add(new ObservableHierarchicalCommandObject() { Text = "myItem 2.1" });
            //cmdItem.Children[1].Children.Add(new ObservableHierarchicalCommandObject() { Text = "myItem 2.2" });
        }
    }

    public class MessageboxCommand : ICommandCore, ICommand
    {
        private string _msg;

        public MessageboxCommand(string msg)
        {
            _msg = msg;
        }

        #region ICommandCore Members

        public bool CanExecute(object param)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object param)
        {
            MessageBox.Show(_msg);
        }

        public string Name
        {
            get { return this.GetType().Name; }
        }

        #endregion
    }

}
