using Omniscient.Foundation.ServiceModel;

namespace Omniscient.Foundation.Security
{
    /// <summary>
    /// Defines a service that will provide the application with user credentials.  
    /// </summary>
    /// <remarks>
    /// This service must provide an "anonymous" credential if a user has not been successfully
    /// authenticated.
    /// </remarks>
    public interface ICredentialService : IStartable
    {
        /// <summary>
        /// Called by an authentication module, if one exists.
        /// </summary>
        void EnsureUserIsLoggedIn();

        /// <summary>
        /// Called on request from anywhere where the user credentials are required.
        /// </summary>
        UserCredential GetUserCredential();
    }
}