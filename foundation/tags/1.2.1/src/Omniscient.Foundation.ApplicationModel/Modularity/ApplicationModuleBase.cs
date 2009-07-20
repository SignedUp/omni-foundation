using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omniscient.Foundation.ApplicationModel.Modularity
{
    public class ApplicationModuleBase: IApplicationModule
    {
        public virtual string Name
        {
            get { return this.GetType().Name; }
        }

        public bool IsLoaded
        {
            get;
            private set;
        }

        public bool IsActivated
        {
            get;
            private set;
        }

        public virtual void Load()
        {
            IsLoaded = true;
        }

        public virtual void Activate(Omniscient.Foundation.ApplicationModel.Presentation.IPresentationController presentation)
        {
            IsActivated = true;
        }

        public virtual void Deactivate()
        {
            IsActivated = false;
        }

        public virtual void Unload()
        {
            IsLoaded = false;
        }
    }
}
