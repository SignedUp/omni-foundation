using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omniscient.Foundation.Commanding
{
    /// <summary>
    /// Base class for composite commands.  When executed, a composite command is executing all its children commands.
    /// </summary>
    /// <typeparam name="TComposite">The type of the children commands.</typeparam>
    public class CompositeCommand<TComposite>: ICommandCore
        where TComposite: ICommandCore
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public CompositeCommand()
        {
            Commands = new List<TComposite>();
        }

        /// <summary>
        /// Gets the list of children (composed) commands.  Upon execution, those children commands will be executed sequentially.
        /// </summary>
        public List<TComposite> Commands
        {
            get;
            private set;
        }

        #region ICommandCore Members

        /// <summary>
        /// Raised when <see cref="CanExecuteChanged"/> will change its return value.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Returns True if the command is in a state that allows execution.  Otherwise, False.
        /// </summary>
        /// <param name="param">The object that will be passed to <see cref="Execute"/>.</param>
        /// <returns>True if command allows execution.  Otherwise, False.</returns>
        public virtual bool CanExecute(object param)
        {
            return true;
        }

        /// <summary>
        /// Executes all children commands sequentially.  Overrides may add behavior to the execution.
        /// </summary>
        /// <param name="param">Object passed to the command for the execution.</param>
        public virtual void Execute(object param) 
        {
            if (!CanExecute(param)) return;
            foreach (TComposite child in Commands)
            {
                child.Execute(param);
            }
        }

        #endregion
    }
}
