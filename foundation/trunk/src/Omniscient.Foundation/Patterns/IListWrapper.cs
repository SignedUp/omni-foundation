using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omniscient.Foundation.Patterns
{
    /// <summary>
    /// Represents a special <see cref="IObjectWrapper<T>"/> that is specialized on wrapping lists.
    /// It is recommended that implementers return the class itself as the result of a call 
    /// to <see cref="Adapt"/>.
    /// </summary>
    public interface IListWrapper<T>: IObjectWrapper<List<T>>, IList<T>
    {
    }
}
