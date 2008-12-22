using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omniscient.Foundation.ApplicationModel.Modularity
{
    public interface IExtender<TContract>
    {
        TContract GetImplementation();
    }
}
