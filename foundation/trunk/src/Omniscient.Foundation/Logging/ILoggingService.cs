using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omniscient.Foundation.Logging
{
    public interface ILoggingService
    {
        ILogger GetLogger(string name);
        ILogger GetLogger(Type logSource);
    }
}
