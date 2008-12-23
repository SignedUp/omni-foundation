using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Mocks;

namespace Omniscient.Foundation.ServiceModel
{
    public class ContractImplementationMock: Mock, IContract
    {
        public string Echo(string msg)
        {
            return msg;
        }
    }
}
