using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omniscient.Foundation.Commanding
{
    public interface ICommandHandler
    {
        void Execute(object param);
    }
}
