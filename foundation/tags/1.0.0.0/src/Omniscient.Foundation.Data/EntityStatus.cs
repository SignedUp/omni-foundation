using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omniscient.Foundation.Data
{
    /// <summary>
    /// The database-aware status of an entity.
    /// </summary>
    public enum EntityStatus
    {
        /// <summary>
        /// The entity is yet to be loaded from the database.
        /// </summary>
        NotLoadedYet,
        
        /// <summary>
        /// The entity is new and doesn't exist yet in the database.  The next save will result in the entity being created.
        /// </summary>
        New,
        
        /// <summary>
        /// The entity is loaded from the database, and hasn't been modified since.
        /// </summary>
        Clean,
        
        /// <summary>
        /// The entity is loaded from the database, and has been modified since.
        /// </summary>
        Dirty,
        
        /// <summary>
        /// The entity will be deleted from the database at the next save.
        /// </summary>
        ToBeDeleted,
        
        /// <summary>
        /// The entity doesn't exist in the database, and won't be created.  Basically, an entity in this state is dead.
        /// </summary>
        NonExistent,
        
        /// <summary>
        /// The entity exists solely to maintain the values of another entity from which it has been cloned.
        /// </summary>
        Clone
    }
}
