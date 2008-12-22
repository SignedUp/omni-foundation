using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omniscient.Foundation.Logging
{
    public interface ILogger
    {
        void Debug(object message);
        void Info(object message);
        void Error(object message);
        void Fatal(object message);
    }
}
