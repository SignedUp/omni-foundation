using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omniscient.Framework.Data.ObjectQuery
{
    /// <summary>
    /// A predicate that checks a property (column) compared to a set of values of the same type.
    /// </summary>
    /// <typeparam name="T">The type of the values in the set (e.g. integer)</typeparam>
    public class SetPredicate<T> : Predicate
    {
        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="path">The path to the property being compared.</param>
        public SetPredicate(string path)
            : base(path)
        { }

        /// <summary>
        /// A set marker.
        /// </summary>
        public SetMarker Marker { get; set; }

        /// <summary>
        /// The type of the values in the set (e.g. integer)
        /// </summary>
        public T[] Values { get; set; }
    }
}
