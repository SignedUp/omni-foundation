using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omniscient.Foundation.ApplicationModel
{
    public class StaticApplicationContext: IApplicationContext
    {
        private static ApplicationManager _current;

        static StaticApplicationContext()
        {
            _current = new ApplicationManager();
        }

        public ApplicationManager Current
        {
            get { return _current; }
        }
    }
}
