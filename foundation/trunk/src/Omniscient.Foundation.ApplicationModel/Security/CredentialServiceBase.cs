using Omniscient.Foundation.ServiceModel;
using Omniscient.Foundation.ApplicationModel.Modularity;

namespace Omniscient.Foundation.ApplicationModel.Security
{
    public abstract class CredentialServiceBase : ServiceModel.ServiceBase<ICredentialService>, ICredentialService
    {
        public UserCredential UserCredential { get; protected set; }

        public override ICredentialService GetImplementation()
        {
            return this;
        }

        #region ICredentialService Members

        public virtual void EnsureUserIsLoggedIn()
        {
            if (UserCredential == null || !UserCredential.IsAuthenticated)
            {
                foreach (IExtender<ICredentialServiceExtenderContract> extender in ApplicationManager.Current.ExtensionPortManager.GetExtensionPort<ICredentialServiceExtenderContract>().Extenders)
                {
                    UserCredential = extender.GetImplementation().GetUserAuthentication();
                    return;
                }
                UserCredential = new UserCredential();
            }
        }

        public virtual UserCredential GetUserCredential()
        {
            EnsureUserIsLoggedIn();
            return UserCredential;
        }

        #endregion

        #region IStartable Members

        public virtual void Start()
        {
            ApplicationManager.Current.ExtensionPortManager.RegisterExtensionPort<ICredentialServiceExtenderContract>(new ExtensionPortBase<ICredentialServiceExtenderContract>());
        }

        public virtual void Stop() { }

        #endregion
    }
}