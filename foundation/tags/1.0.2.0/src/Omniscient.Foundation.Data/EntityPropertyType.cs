namespace Omniscient.Foundation.Data
{
    /// <summary>
    /// An entity's property type.
    /// </summary>
    public enum EntityPropertyType
    {
        /// <summary>
        /// Value property types are a scalar being serialized to a column in the database.
        /// </summary>
        Value,

        /// <summary>
        /// Specifies that the property is a pointer to another entity.
        /// </summary>
        Reference,

        /// <summary>
        /// Specifies that the property is a pointer to a list of other entities.
        /// </summary>
        ReferenceList
    }
}