using System;
using System.Linq;
using Omniscient.Foundation.Security;
using System.Diagnostics;
using System.Collections.Generic;
using Omniscient.Foundation.ApplicationModel;

namespace Omniscient.Foundation.Security
{
    /// <summary>
    /// Defines a base implementation class for ICredentialService.  When this service starts, it registers an anonymous user
    /// as the AppDomain's principal.
    /// </summary>
    public class CredentialService : ServiceModel.ExtendableServiceBase<ICredentialService, ICredentialServiceExtender>, ICredentialService
    {
        private SecurePrincipal _current;

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
        /// Ensures the user has entered login information, using extenders.  Only the first extender will be used.
        /// </summary>
        /// <remarks>
        /// If the user did not enter any information, UserCredential will be assigned the "anonymous" status.
        /// </remarks>
        /// <exception cref="System.Security.SecurityException">Thrown if the extender is unable to authenticate the user.</exception>
        public virtual void EnsureUserIsAuthenticated()
        {
            if (_current.Identity.IsAuthenticated) return;
            Debug.Assert(CurrentPrincipal != null);

            foreach (IExtender<ICredentialServiceExtender> extender in this.Extenders)
            {
                extender.GetImplementation().NegociateAuthentication(CurrentPrincipal);
                if (!CurrentPrincipal.Identity.IsAuthenticated)
                    throw new System.Security.SecurityException("Unable to authenticate current principal.");
                return;
            }
            throw new InvalidOperationException("No extenders have been registered for the CredentialService service.  Cannot authenticate user.");
        }

        /// <summary>
        /// Returns the current user principal.  That user will not be logged in until a call is made to <c>EnsureUserIsLoggedIn</c>.
        /// </summary>
        /// <returns></returns>
        public virtual SecurePrincipal CurrentPrincipal
        {
            get { return _current; }
        }

        #endregion

        #region IStartable Members

        /// <summary>
        /// Starts the current service.
        /// </summary>
        public virtual void Start()
        {
            //Create an anonymous user on service startup.
            _current = new SecurePrincipal();
#if !SILVERLIGHT
            AppDomain.CurrentDomain.SetThreadPrincipal(_current);
#endif
        }

        /// <summary>
        /// Stops the current service.
        /// </summary>
        public virtual void Stop() { }

        #endregion
    }
}