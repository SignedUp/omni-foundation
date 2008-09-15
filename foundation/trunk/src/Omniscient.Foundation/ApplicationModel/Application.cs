using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Omniscient.Foundation.ServiceModel;

namespace Omniscient.Foundation.ApplicationModel
{
    public class Application
    {
        public IServiceContainer ServiceContainer
        {
            get;
            set;
        }

        public virtual void StartApplication()
        { }

        public virtual void CloseApplication()
        { }
    }
}
