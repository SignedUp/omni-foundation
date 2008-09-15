using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omniscient.Framework.Data.ObjectQuery
{
    /// <summary>
    /// Compares a path to another path (e.g. item.value <= customer.threshold)
    /// </summary>
    public class PathPredicate : Predicate
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="path">The path to the property being filtered./param>
        public PathPredicate(string path)
            : base(path)
        { }

        /// <summary>
        /// An equality or inequality marker.
        /// </summary>
        public RelationMarker Marker { get; set; }

        /// <summary>
        /// The path to the property that compares to the first path.
        /// </summary>
        public string SecondPath { get; set; }
    }
}
