using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omniscient.Foundation.Commanding
{
    /// <summary>
    /// Command that delegates the execution of Execute and CanExecute
    /// </summary>
    public class DelegateCommand: CommandBase
    {
        private Action<object> _execute;
        private Func<object, bool> _canExecute;

        /// <summary>
        /// Constructs a command that delegates the execution of Execute and CanExecute
        /// </summary>
        /// <param name="executeAction">Action to call when executing Execute</param>
        /// <param name="canExecuteFunc">Function to call when executing CanExecute</param>
        public DelegateCommand(Action<object> executeAction, Func<object, bool> canExecuteFunc)
        {
            if (executeAction == null) throw new ArgumentNullException("executeAction");
            if (canExecuteFunc == null) throw new ArgumentNullException("canExecuteFunc");

            _execute = executeAction;
            _canExecute = canExecuteFunc;
        }

        /// <summary>
        /// Returns true if the command can be executed.  Otherwise, false.
        /// </summary>
        /// <param name="param">The param that will be passed to Execute.</param>
        /// <returns>True if the command can be executed.  Otherwise, false.</returns>
        public override bool CanExecute(object param)
        {
            return _canExecute(param);
        }

        /// <summary>
        /// Executes the command.  Returns without executing the command if a call to CanExecute returns false.
        /// </summary>
        /// <param name="param">The param passed to the executing command.</param>
        public override void Execute(object param)
        {
            if (!_canExecute(param)) return;
            _execute(param);
        }
    }
}
