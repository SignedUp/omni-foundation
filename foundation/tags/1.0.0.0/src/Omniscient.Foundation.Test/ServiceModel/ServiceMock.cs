using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omniscient.Foundation.ServiceModel
{
    public class ServiceMock: GenericSingletonService<IContract, ContractImplementationMock>
    {
    }
}
