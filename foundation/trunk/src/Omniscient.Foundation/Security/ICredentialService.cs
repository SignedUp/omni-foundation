using System;
using Omniscient.Foundation.Security;
using Omniscient.Foundation.ApplicationModel;

namespace Omniscient.Foundation.Security
{
    /// <summary>
    /// Defines a service that will provide the application with user credentials.  The service places the current user
    /// in the <c>System.Threading.Thread.CurrentPrincipal</c> value.
    /// </summary>
    public interface ICredentialService : IStartable, IExtendable<ICredentialServiceExtender>
    {
        /// <summary>
        /// Ensures that the current user is not anonymous.  The service will as extenders to ask for user name and password.
        /// If no extenders are plugged in, the service is unable to log the user, and the InvalidOperationException is thrown.
        /// </summary>
        /// <exception cref="InvalidOperationException">The service must rely on extenders to ask for username and password;
        /// therefore, if no extenders are plugged into the extension port, this exception is thrown.</exception>
        void EnsureUserIsAuthenticated();

        /// <summary>
        /// Called on request from anywhere where the user credentials are required.  Returns the same object as 
        /// <c>System.Threading.Thread.CurrentPrincipal</c>.
        /// </summary>
        SecurePrincipal CurrentPrincipal { get; }
    }
}