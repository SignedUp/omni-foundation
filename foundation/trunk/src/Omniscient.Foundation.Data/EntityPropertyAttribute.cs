using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omniscient.Foundation.Data
{
    /// <summary>
    /// Marks a property as being serialized with the entity.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class EntityPropertyAttribute: Attribute
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="type">Either a value type, a reference type, or a reference list.</param>
        public EntityPropertyAttribute(EntityPropertyType type)
            : this(type, string.Empty)
        { }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="type">Either a value type, a reference type, or a reference list.</param>
        /// <param name="column">The column mapped to this property.  Can be null.</param>
        public EntityPropertyAttribute(EntityPropertyType type, string column)
        {
            Type = type;
            if (column == null) ColumnName = string.Empty;
            ColumnName = column;
        }

        /// <summary>
        /// Gets the property type of this property.
        /// </summary>
        public EntityPropertyType Type
        {
            get;
            set;
        }

        public string ColumnName
        {
            get;
            set;
        }
    }

}
