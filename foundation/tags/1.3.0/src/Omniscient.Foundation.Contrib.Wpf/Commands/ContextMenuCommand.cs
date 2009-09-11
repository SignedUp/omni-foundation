using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Omniscient.Foundation.Commanding;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Omniscient.Foundation.Contrib.Wpf.Commands
{
    public class ContextMenuCommand : ICommandCore, ICommand
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

        public string Name { get { return this.GetType().Name; } }

        public event EventHandler CanExecuteChanged;

        public void Execute(object param)
        {
            Application.Current.MainWindow.Activate();
            _menu.IsOpen = true;
        }
        #endregion
    }
}
