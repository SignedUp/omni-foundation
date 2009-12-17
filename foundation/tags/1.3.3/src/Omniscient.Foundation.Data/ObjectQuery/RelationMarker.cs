namespace Omniscient.Foundation.Data.ObjectQuery
{
    /// <summary>
    /// An equality or inequality marker.
    /// </summary>
    public enum RelationMarker
    {
        /// <summary>
        /// Scalar equality.
        /// </summary>
        Equal, 
        
        /// <summary>
        /// Strict scalar inequality.
        /// </summary>
        NotEqual, 
        
        /// <summary>
        /// Greater Than inequality.
        /// </summary>
        Greater, 
        
        /// <summary>
        /// Greater Than or Equal inequality.
        /// </summary>
        GreaterOrEqual, 
        
        /// <summary>
        /// Smaller Than inequality.
        /// </summary>
        Smaller, 
        
        /// <summary>
        /// Smaller Than or Equal inequality.
        /// </summary>
        SmallerOrEqual, 
        
        /// <summary>
        /// Reference equality.
        /// </summary>
        Is, 
        
        /// <summary>
        /// Reference inequality.
        /// </summary>
        IsNot
    }
}
