using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omniscient.Foundation.ServiceModel
{
    public interface IContract
    {
        string Echo(string msg);
    }
}
