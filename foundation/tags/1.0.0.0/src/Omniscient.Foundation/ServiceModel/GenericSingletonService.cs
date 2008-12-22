using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omniscient.Foundation.ServiceModel
{
    public class GenericSingletonService<TContract, TImplementation>: ServiceBase<TContract>
        where TImplementation: TContract, new()
    {
        private TImplementation _singleton;

        public GenericSingletonService()
        {
            _singleton = new TImplementation();
        }

        public override TContract GetImplementation()
        {
            return _singleton;
        }
    }
}
