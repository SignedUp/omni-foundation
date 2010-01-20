namespace Omniscient.Foundation.Commanding
{
    using System;

    public abstract class CommandBase : ICommandCore
    {
        public event EventHandler CanExecuteChanged;

        public CommandBase()
        {
            CanExecuteChanged = delegate { };
        }

        public virtual string Name
        {
            get { return GetType().Name; }
        }

        public abstract bool CanExecute(object param);

        public abstract void Execute(object param);

        public void NotifyCanExecuteChanged()
        {
            CanExecuteChanged(this, EventArgs.Empty);
        }
   }
}
