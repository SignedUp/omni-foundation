using System;

namespace Omniscient.Foundation.Commanding
{
    ///<summary>
    ///</summary>
    public class DelegateCommandHandler: ICommandHandler
    {
        private Action<object> _action;

        ///<summary>
        ///</summary>
        ///<param name="executeMethod"></param>
        ///<exception cref="ArgumentNullException"></exception>
        public DelegateCommandHandler(Action<object> executeMethod)
        {
            if (executeMethod == null) throw new ArgumentNullException("executeMethod");
            _action = executeMethod;
        }

        #region ICommandHandler Members

        ///<summary>
        ///</summary>
        ///<param name="param"></param>
        public void Execute(object param)
        {
            _action(param);
        }

        #endregion
    }
}
