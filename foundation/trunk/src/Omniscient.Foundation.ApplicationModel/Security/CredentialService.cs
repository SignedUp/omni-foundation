using System;
using System.Linq;
using System.Collections.Generic;
using Omniscient.Foundation.ServiceModel;
using Omniscient.Foundation.ApplicationModel.Modularity;
using Omniscient.Foundation.Security;

namespace Omniscient.Foundation.ApplicationModel.Security
{
    /// <summary>
    /// Defines a base implementation class for ICredentialService.  When this service starts, it registers an anonymous user
    /// as the AppDomain's principal.
    /// </summary>
    public class CredentialService : ServiceModel.ServiceBase<ICredentialService>, ICredentialService
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
        /// Ensures the user has entered login information.
        /// </summary>
        /// <remarks>
        /// If the user did not enter any information, UserCredential will be assigned the "anonymous" status.
        /// </remarks>
        public virtual void EnsureUserIsAuthenticated()
        {
            if (_current != null && _current.Identity.IsAuthenticated) return;

            IExtensionPortManager manager = ApplicationManager.Current.ExtensionPortManager;
            IExtensionPort<ICredentialServiceExtenderContract> port;
            port = manager.GetExtensionPort<ICredentialServiceExtenderContract>();
            if (port.Extenders.Count() < 1) throw new InvalidOperationException
                ("No extender found for extension port ICredentialServiceExtenderContract.  The service is unable to ensure that user is authenticated by itself.");

            foreach (IExtender<ICredentialServiceExtenderContract> extender in port.Extenders)
            {
                extender.GetImplementation().NegociateAuthentication(CurrentPrincipal);
                if (CurrentPrincipal.Identity.IsAuthenticated) return;
            }            
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
            IExtensionPortManager manager = ApplicationManager.Current.ExtensionPortManager;
            if (manager.GetExtensionPort<ICredentialServiceExtenderContract>() == null)
            {
                manager.RegisterExtensionPort<ICredentialServiceExtenderContract>(new ExtensionPortBase<ICredentialServiceExtenderContract>());
            }

            //Create an anonymous user on service startup.
            _current = new SecurePrincipal();
            System.AppDomain.CurrentDomain.SetThreadPrincipal(_current);
        }

        /// <summary>
        /// Stops the current service.
        /// </summary>
        public virtual void Stop() { }

        #endregion
    }
}