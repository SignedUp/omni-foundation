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
        /// <param name="propertyType">Either a value type, a reference type, or a reference list.</param>
        public EntityPropertyAttribute(EntityPropertyType propertyType)
        {
            Type = propertyType;
        }

        /// <summary>
        /// Gets the property type of this property.
        /// </summary>
        public EntityPropertyType Type
        {
            get;
            private set;
        }
    }

}
