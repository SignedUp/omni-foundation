namespace Omniscient.Foundation.Data
{
    /// <summary>
    /// The status of an entity relative to its data store.
    /// </summary>
    public enum EntityStatus
    {
        /// <summary>
        /// The entity is new and doesn't exist yet in the data store.  The next save will result in the entity being created.
        /// </summary>
        New,
        
        /// <summary>
        /// The entity is loaded from the data store, and hasn't been modified since.
        /// </summary>
        Clean,
        
        /// <summary>
        /// The entity is loaded from the data store, and has been modified since.
        /// </summary>
        Dirty,
        
        /// <summary>
        /// The entity will be deleted from the data store at the next commit.
        /// </summary>
        ToBeDeleted,
        
        /// <summary>
        /// The entity doesn't exist in the database, and won't be created.  Basically, an entity in this state is dead.
        /// Generally, an entity will be in this state after it's been marked as ToBeDeleted, and has been committed 
        /// (effectively deleted from the datastore).
        /// </summary>
        NonExistent,
        
        /// <summary>
        /// The entity exists solely to maintain the values of another entity from which it has been cloned.
        /// </summary>
        Clone
    }
}
