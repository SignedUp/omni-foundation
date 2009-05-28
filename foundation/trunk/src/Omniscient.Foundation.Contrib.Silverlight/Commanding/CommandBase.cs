using Omniscient.Foundation.Commanding;
using System.Windows.Input;
using System;

namespace Omniscient.Foundation.Contrib.Silverlight.Commanding
{
    /// <summary>
    /// This is a <see cref="Omniscient.Foundation.Commanding.CommandBase"/> that implements
    /// the <see cref="ICommand"/> Silverlight interface, in order to be compatible with Silverlight.
    /// </summary>
    public abstract class CommandBase : Omniscient.Foundation.Commanding.CommandBase, ICommand
    {
    }
}
