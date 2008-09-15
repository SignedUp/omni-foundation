using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omniscient.Foundation.Data.ObjectQuery
{
    /// <summary>
    /// This predicate compares a column to a scalar value (e.g. an integer)
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    public class ValuePredicate<T> : Predicate
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="path"></param>
        public ValuePredicate(string path)
            : base(path)
        { }

        /// <summary>
        /// An equality or inequality marker.
        /// </summary>
        public RelationMarker Marker { get; set; }

        /// <summary>
        /// The value to compare with.
        /// </summary>
        public T Value { get; set; }
    }
}
