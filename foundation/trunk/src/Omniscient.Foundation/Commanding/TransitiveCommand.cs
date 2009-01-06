using System;
using System.Collections.Generic;

namespace Omniscient.Foundation.Commanding
{
    
    ///<summary>
    ///</summary>
    public class TransitiveCommand: ICommandCore
    {
        private List<ICommandHandler> _handlers;
        private List<ICommandHandler> _registeredHandlers;
        private List<ICommandHandler> _unregisteredHandlers;
        private bool _isExecuting;

        ///<summary>
        ///</summary>
        public TransitiveCommand()
        {
            _handlers = new List<ICommandHandler>();
            _registeredHandlers = new List<ICommandHandler>();
            _unregisteredHandlers = new List<ICommandHandler>();
            _isExecuting = false;
        }

        ///<summary>
        ///</summary>
        ///<param name="handler"></param>
        public void RegisterHandler(ICommandHandler handler)
        {
            if(_isExecuting)
            {
                _registeredHandlers.Add(handler);
            }
            else
            {
                _handlers.Add(handler);
                if (_handlers.Count == 1 && CanExecuteChanged != null) CanExecuteChanged(this, EventArgs.Empty);
            }
        }

        ///<summary>
        ///</summary>
        ///<param name="handler"></param>
        public void UnregisterHandler(ICommandHandler handler)
        {
            if (_isExecuting)
            {
                _unregisteredHandlers.Add(handler);
            }
            else
            {
                _handlers.Remove(handler);
                if (_handlers.Count == 0 && CanExecuteChanged != null) CanExecuteChanged(this, EventArgs.Empty);
            }
        }

        #region ICommandCore Members

        public event EventHandler CanExecuteChanged;

        public virtual bool CanExecute(object param)
        {
            return _handlers.Count > 0;
        }

        public virtual void Execute(object param)
        {
            _isExecuting = true;
            foreach (ICommandHandler handler in _handlers)
            {
                handler.Execute(param);
            }
            _isExecuting = false;
            foreach(ICommandHandler handler in _registeredHandlers)
            {
                RegisterHandler(handler);
            }
            foreach(ICommandHandler handler in _unregisteredHandlers)
            {
                UnregisterHandler(handler);
            }
        }

        #endregion
    }
}
