using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omniscient.Foundation.ServiceModel
{
    public class ServiceMock: IService<IContract>
    {
        private ContractImplementationMock _imp;

        public ServiceMock()
        {
            _imp = new ContractImplementationMock();
        }

        public IContract GetImplementation()
        {
            return _imp;
        }

        public string Name
        {
            get { return "ServiceMock"; }
        }

        public Type ContractType
        {
            get { return typeof(IContract); }
        }
    }
}
