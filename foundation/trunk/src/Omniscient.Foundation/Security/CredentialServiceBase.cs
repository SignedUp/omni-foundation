using Omniscient.Foundation.ServiceModel;

namespace Omniscient.Foundation.Security
{
    public abstract class CredentialServiceBase : ServiceModel.ServiceBase<ICredentialService>, ICredentialService
    {
        public UserCredential UserCredential { get; protected set; }

        public override ICredentialService GetImplementation()
        {
            return this;
        }

        #region ICredentialService Members

        public abstract void EnsureUserIsLoggedIn();

        public virtual UserCredential GetUserCredential()
        {
            EnsureUserIsLoggedIn();
            return UserCredential;
        }

        #endregion

        #region IStartable Members

        public abstract void Start();
        public abstract void Stop();

        #endregion
    }
}