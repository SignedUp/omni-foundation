using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omniscient.Framework.Data
{
    /// <summary>
    /// Thrown when the status of an entity is not suited for the requested operation.
    /// </summary>
    public class InvalidOperationForStatusException: InvalidOperationException
    {
        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="invalidStatus">The faulty entity's status.</param>
        public InvalidOperationForStatusException(EntityStatus invalidStatus)
            :base(string.Format("Invalid status {0} for this operation.", invalidStatus))
        {
            this.Status = invalidStatus;
        }

        /// <summary>
        /// Gets the faulty entity's status
        /// </summary>
        public EntityStatus Status { get; private set; }
    }
}
