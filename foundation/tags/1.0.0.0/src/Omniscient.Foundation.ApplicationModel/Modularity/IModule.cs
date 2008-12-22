using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Omniscient.Foundation.ApplicationModel.Presentation;

namespace Omniscient.Foundation.ApplicationModel.Modularity
{
    public interface IModule
    {
        IPresentationController PresentationController { get; set; }
    }
}
