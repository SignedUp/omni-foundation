using System;
using System.Collections.Generic;

namespace Omniscient.Foundation.Commanding
{
    
    ///<summary>
    ///</summary>
    public class TransitiveCommand: ICommandCore
    {
        private List<ICommandHandler> _handlers;

        ///<summary>
        ///</summary>
        public TransitiveCommand()
        {
            _handlers = new List<ICommandHandler>();
        }

        ///<summary>
        ///</summary>
        ///<param name="handler"></param>
        public void RegisterHandler(ICommandHandler handler)
        {
            _handlers.Add(handler);
            if (_handlers.Count == 1 && CanExecuteChanged != null) CanExecuteChanged(this, EventArgs.Empty);
        }

        ///<summary>
        ///</summary>
        ///<param name="handler"></param>
        public void UnregisterHandler(ICommandHandler handler)
        {
            _handlers.Remove(handler);
            if (_handlers.Count == 0 && CanExecuteChanged != null) CanExecuteChanged(this, EventArgs.Empty);
        }

        #region ICommandCore Members

        public event EventHandler CanExecuteChanged;

        public virtual bool CanExecute(object param)
        {
            return _handlers.Count > 0;
        }

        public virtual void Execute(object param)
        {
            foreach (ICommandHandler handler in _handlers)
            {
                handler.Execute(param);
            }
        }

        #endregion
    }
}
