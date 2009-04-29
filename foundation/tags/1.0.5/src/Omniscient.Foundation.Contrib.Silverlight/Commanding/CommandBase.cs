using Omniscient.Foundation.Commanding;
using System.Windows.Input;
using System;

namespace Omniscient.Foundation.Contrib.Silverlight.Commanding
{
    public class CommandBase: ICommandCore, ICommand
    {
        public event EventHandler CanExecuteChanged;

        public virtual bool CanExecute(object param)
        {
            return true;
        }

        public virtual void Execute(object param)
        {
            //nop
        }

        public virtual string Name
        {
            get { return this.GetType().Name; }
        }
    }
}
