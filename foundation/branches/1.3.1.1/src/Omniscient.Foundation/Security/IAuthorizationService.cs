using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omniscient.Foundation.Security
{
    /// <summary>
    /// Authorizes a <see cref="SecurePrincipal"/> object by promoting it with roles.
    /// </summary>
    public interface IAuthorizationService
    {
        /// <summary>
        /// Promotes an existing <see cref="SecurePrincipal"/> object with roles.
        /// </summary>
        /// <param name="principal">Principal object to promote with roles.</param>
        void Promote(SecurePrincipal principal);
        
        /// <summary>
        /// Authorizes a Principal with roles and returns it as a <see cref="SecurePrincipal"/>.
        /// </summary>
        /// <param name="identity">The Identity used by the new Principal.</param>
        /// <returns>A Principal with roles.</returns>
        SecurePrincipal Authorize(SecureIdentity identity);
    }
}
