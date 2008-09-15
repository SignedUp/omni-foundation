using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omniscient.Framework.Data.ObjectQuery
{

    /// <summary>
    /// Represents a set of predicates grouped by a logical AND or OR operand.
    /// </summary>
    public class WhereGroup
    {
        private List<WhereGroup> _groups;
        private List<Predicate> _predicates;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="logic">AND or OR, depending on what kind of group is needed.</param>
        public WhereGroup(LogicalGroupComposition logic)
        {
            Logic = logic;
            _groups = new List<WhereGroup>();
            _predicates = new List<Predicate>();
        }

        /// <summary>
        /// Gets the logic of that group; either AND or OR.
        /// </summary>
        public LogicalGroupComposition Logic { get; private set; }

        /// <summary>
        /// Adds a Value predicate; generally compares a column to a scalar value.
        /// </summary>
        /// <typeparam name="T">The type of the value to compare with.</typeparam>
        /// <param name="path">The path of the object property to compare value with.</param>
        /// <param name="marker">An equality or inequality marker.</param>
        /// <param name="value">The value to compare with.</param>
        /// <returns>Returns current group.</returns>
        public WhereGroup AddValuePredicate<T>(string path, RelationMarker marker, T value)
        {
            ValuePredicate<T> predicate;
            predicate = new ValuePredicate<T>(path);
            predicate.Marker = marker;
            predicate.Value = value;
            _predicates.Add(predicate);
            return this;
        }

        /// <summary>
        /// Adds a path predicate; generally compares two columns in the database.
        /// </summary>
        /// <param name="path">The left-side path of the object property.</param>
        /// <param name="marker">An equality or inequality marker.</param>
        /// <param name="secondPath">The right-side path of the object property.</param>
        /// <returns>Returns current group.</returns>
        public WhereGroup AddPathPredicate(string path, RelationMarker marker, string secondPath)
        {
            PathPredicate predicate;
            predicate = new PathPredicate(path);
            predicate.Marker = marker;
            predicate.SecondPath = secondPath;
            _predicates.Add(predicate);
            return this;
        }

        /// <summary>
        /// Adds a set predicates; generally compares to a set of values of the same type (e.g. a set of integer values)
        /// </summary>
        /// <typeparam name="T">The type of the values in the set.</typeparam>
        /// <param name="path">Teh path of the object property to compare against value set.</param>
        /// <param name="marker">An equality or inequality marker.</param>
        /// <param name="values">A set of values of type <typeparamref name="T"/>.</param>
        /// <returns>Returns current group.</returns>
        public WhereGroup AddSetPredicate<T>(string path, SetMarker marker, T[] values)
        {
            SetPredicate<T> predicate;
            predicate = new SetPredicate<T>(path);
            predicate.Marker = marker;
            predicate.Values = values;
            _predicates.Add(predicate);
            return this;
        }

        /// <summary>
        /// Add a subgroup to this group.
        /// </summary>
        /// <param name="logic">The logic of the new group.</param>
        /// <returns>The new group.</returns>
        public WhereGroup AddGroup(LogicalGroupComposition logic)
        {
            WhereGroup g;
            g = new WhereGroup(logic);
            _groups.Add(g);
            return g;            
        }

        /// <summary>
        /// Returns the list of predicates for this group.
        /// </summary>
        public IEnumerable<Predicate> Predicates
        {
            get { return _predicates.ToArray(); }
        }

        /// <summary>
        /// Returns the list of subgroups.
        /// </summary>
        public IEnumerable<WhereGroup> Groups
        {
            get { return _groups.ToArray(); }
        }
    }
}
