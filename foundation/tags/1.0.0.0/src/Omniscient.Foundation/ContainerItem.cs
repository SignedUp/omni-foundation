using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omniscient.Foundation
{
    //todo: this looks like ServiceModel with the use of ServiceContainer; which one is better?  Why two?
    public class ContainerItem<T>
    {
        private T _instance;

        public ContainerItem()
            : this(default(T))
        {
        }

        public ContainerItem(T instance)
        {
            _instance = instance;
        }

        public virtual T GetInstance()
        {
            return _instance;
        }
    }
}
