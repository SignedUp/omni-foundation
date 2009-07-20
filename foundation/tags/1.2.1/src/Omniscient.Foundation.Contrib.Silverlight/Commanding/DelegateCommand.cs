using System.Windows.Input;
using System;

namespace Omniscient.Foundation.Contrib.Silverlight.Commanding
{
    /// <summary>
    /// This is a <see cref="Omniscient.Foundation.Commanding.DelegateCommand"/> that implements
    /// the <see cref="ICommand"/> Silverlight interface, in order to be compatible with Silverlight.
    /// </summary>
    public class DelegateCommand: Omniscient.Foundation.Commanding.DelegateCommand, ICommand
    {
        /// <summary>
        /// Constructs a command that delegates the execution of Execute and CanExecute
        /// </summary>
        /// <param name="executeAction">Action to call when executing Execute</param>
        /// <param name="canExecuteFunc">Function to call when executing CanExecute</param>
        public DelegateCommand(Action<object> executeAction, Func<object, bool> canExecuteFunc)
            : base(executeAction, canExecuteFunc) { }
    }
}
