using Omniscient.Foundation.ServiceModel;
using Omniscient.Foundation.ApplicationModel.Modularity;

namespace Omniscient.Foundation.ApplicationModel.Security
{
    /// <summary>
    /// Defines a base implementation class for ICredentialService
    /// </summary>
    public class CredentialServiceBase : ServiceModel.ServiceBase<ICredentialService>, ICredentialService
    {
        /// <summary>
        /// Holds current user's credential.
        /// </summary>
        public UserCredential UserCredential { get; protected set; }

        /// <summary>
        /// Retrieves implementation for ICredentialService.
        /// </summary>
        /// <returns></returns>
        public override ICredentialService GetImplementation()
        {
            return this;
        }

        #region ICredentialService Members

        /// <summary>
        /// Ensures the user has entered login information.
        /// </summary>
        /// <remarks>
        /// If the user did not enter any information, UserCredential will be assigned the "anonymous" status.
        /// </remarks>
        public virtual void EnsureUserIsLoggedIn()
        {
            if (UserCredential == null || UserCredential.IsAnonymous)
            {
                IExtensionPortManager manager = ApplicationManager.Current.ExtensionPortManager;
                foreach (IExtender<ICredentialServiceExtenderContract> extender in manager.GetExtensionPort<ICredentialServiceExtenderContract>().Extenders)
                {
                    UserCredential = extender.GetImplementation().GetUserAuthentication();
                    return;
                }
                UserCredential = GetAnonymousUserCredential();
            }
        }

        /// <summary>
        /// Returns an "anonymous" UserCredential.
        /// </summary>
        /// <returns>UserCredential with "anonymous" status.</returns>
        protected virtual UserCredential GetAnonymousUserCredential()
        { 
            return new UserCredential();
        }

        /// <summary>
        /// Returns the current user's credential.
        /// </summary>
        /// <returns></returns>
        public virtual UserCredential GetUserCredential()
        {
            EnsureUserIsLoggedIn();
            return UserCredential;
        }

        #endregion

        #region IStartable Members

        /// <summary>
        /// Starts the current service.
        /// </summary>
        public virtual void Start()
        {
            IExtensionPortManager manager = ApplicationManager.Current.ExtensionPortManager;
            if (manager.GetExtensionPort<ICredentialServiceExtenderContract>() == null)
            {
                manager.RegisterExtensionPort<ICredentialServiceExtenderContract>(new ExtensionPortBase<ICredentialServiceExtenderContract>());
            }
        }

        /// <summary>
        /// Stops the current service.
        /// </summary>
        public virtual void Stop() { }

        #endregion
    }
}