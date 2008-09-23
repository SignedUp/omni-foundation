using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omniscient.Foundation
{
    /// <summary>
    /// Defines an object container where objects of certain types can be registered and later gathered.
    /// </summary>
    public interface IObjectContainer
    {
        void Register<TObject>(TObject instance);
        TObject Get<TObject>();
    }
}
