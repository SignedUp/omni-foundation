using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omniscient.Foundation.Commanding
{
    public class DelegateCommandHandler: ICommandHandler
    {
        private Action<object> _action;

        public DelegateCommandHandler(Action<object> executeMethod)
        {
            if (executeMethod == null) throw new ArgumentNullException("executeMethod");
            _action = executeMethod;
        }

        #region ICommandHandler Members

        public void Execute(object param)
        {
            _action(param);
        }

        #endregion
    }
}
