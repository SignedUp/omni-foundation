using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omniscient.Foundation.Security
{
    /// <summary>
    /// Authenticates a user based on a username (the identifier) and a password.
    /// </summary>
    public interface IAuthenticationService
    {
        /// <summary>
        /// Promotes an existing <see cref="SecureIdentity"/> object by authenticating the 
        /// username and password.
        /// </summary>
        /// <param name="identity">An existing <see cref="SecureIdentity"/> object.</param>
        /// <param name="username">A username that identifies the <see cref="SecureIdentity"/>.  
        /// Can be empty (anonymous).</param>
        /// <param name="password">A password to validate.</param>
        void Promote(SecureIdentity identity, string username, string password);
        
        /// <summary>
        /// Authenticates the username and password and returns a new <see cref="SecureIdentity"/>.
        /// </summary>
        /// <param name="username">A username that identifies the <see cref="SecureIdentity"/>.  
        /// Can be empty (anonymous).</param>
        /// <param name="password">A password to validate.</param>
        /// <returns>An authenticated user, or anonymous if the username was empty.</returns>
        SecureIdentity Authenticate(string username, string password);
    }
}
