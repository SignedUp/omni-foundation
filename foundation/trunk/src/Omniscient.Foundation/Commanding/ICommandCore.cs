using System;

namespace Omniscient.Foundation.Commanding
{
    /// <summary>
    /// Interface that represents a command.  The event CanExecuteChanged has been simplified so that this 
    /// interface is identical to the one contained in WPF (PresentationCore.dll).  ICommandCore has been 
    /// created because we don't believe that commands should be limited to environments with a user interface.
    /// </summary>
    /// <remarks>
    /// Your command can easily implement both interfaces to be able to leverage WPF's functionalities.  Since
    /// the members signatures are identical, you don't need to replicate any code; each member's implementation
    /// will handle the member from both interfaces automatically.
    /// </remarks>
    public interface ICommandCore
    {
        /// <summary>
        /// Raised when the command's availability changes.
        /// </summary>
        event EventHandler CanExecuteChanged;
        
        /// <summary>
        /// Retreives a value that indicates whether or not the command is in a state that allows execution.
        /// </summary>
        /// <param name="param">This is the param that would be passed to <see cref="Execute"/> method.</param>
        /// <returns>True if the command is in a state that allows execution; otherwise, false.</returns>
        bool CanExecute(object param);
        
        /// <summary>
        /// Executes the command
        /// </summary>
        /// <param name="param">Any value needed for execution.</param>
        void Execute(object param);
    }
}
