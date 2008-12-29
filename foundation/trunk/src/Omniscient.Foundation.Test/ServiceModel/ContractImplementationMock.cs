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
