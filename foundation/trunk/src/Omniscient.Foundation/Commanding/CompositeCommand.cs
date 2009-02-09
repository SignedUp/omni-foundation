using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omniscient.Foundation.Commanding
{
    /// <summary>
    /// Defines a composite command.  When executed, a composite command is executing all its children commands.  
    /// See <see cref="CompositeCommand"/> for default implementation.
    /// </summary>
    public interface ICompositeCommand : ICommandCore
    {
        ICollection<ICommandCore> Commands { get; }
    }

    /// <summary>
    /// Base class for composite commands.  When executed, a composite command is executing all its children commands.
    /// </summary>
    public class CompositeCommand: ICompositeCommand
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public CompositeCommand()
        {
            Commands = new List<ICommandCore>();
        }

        /// <summary>
        /// Gets the list of children (composed) commands.  Upon execution, those children commands will be executed sequentially.
        /// </summary>
        public virtual ICollection<ICommandCore> Commands
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
            foreach (ICommandCore child in Commands)
            {
                child.Execute(param);
            }
        }

        /// <summary>
        /// Gets the name of the command.
        /// </summary>
        public virtual string Name { get { return this.GetType().Name; } }
        #endregion
    }
}
