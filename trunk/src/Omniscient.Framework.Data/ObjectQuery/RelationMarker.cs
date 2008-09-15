using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omniscient.Framework.Data.ObjectQuery
{
    /// <summary>
    /// An equality or inequality marker.
    /// </summary>
    public enum RelationMarker
    {
        Equal, NotEqual, Greater, GreaterOrEqual, Smaller, SmallerOrEqual, Is, IsNot
    }
}
