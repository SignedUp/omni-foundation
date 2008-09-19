using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omniscient.Foundation.ServiceModel
{
    public class GenericSingletonService<TContract, TImplementation>: IService<TContract>
        where TImplementation: TContract, new()
    {
        private TImplementation _singleton;

        public GenericSingletonService()
        {
            _singleton = new TImplementation();
        }

        public TContract GetImplementation()
        {
            return _singleton;
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
