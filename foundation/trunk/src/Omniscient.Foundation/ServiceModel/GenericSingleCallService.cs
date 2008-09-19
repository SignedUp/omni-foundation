using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omniscient.Foundation.ServiceModel
{
    public class GenericSingleCallService<TContract, TImplementation>: IService<TContract>
        where TImplementation: TContract, new()
    {
        public TContract GetImplementation()
        {
            return new TImplementation();
        }

        public string Name
        {
            get { return typeof(TContract).Name; }
        }

        public Type ContractType
        {
            get { return typeof(TContract); }
        }
    }
}
