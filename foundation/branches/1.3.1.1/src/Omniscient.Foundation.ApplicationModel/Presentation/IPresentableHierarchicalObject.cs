using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omniscient.Foundation.ApplicationModel.Presentation
{
    public interface IPresentableHierarchicalObject<TChildrenType>: IPresentableObject
    {
        IEnumerable<TChildrenType> Children { get; }
    }
}
