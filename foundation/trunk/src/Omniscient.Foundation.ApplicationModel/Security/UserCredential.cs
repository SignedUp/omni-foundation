using System.Security;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace Omniscient.Foundation.ApplicationModel.Security
{
    /// <summary>
    /// Defines the user credential that identifies a user in the application.
    /// </summary>
    public class UserCredential : IIdentity
    {
        private const string ANONYMOUS_USER = "anonymous";

        /// <summary>
        /// Represents password of a user.
        /// </summary>
        public SecureString Password { get; set; }

        /// <summary>
        /// Represents name of a user.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Shows if the user did not provide cridential information.
        /// </summary>
        public bool IsAnonymous { get { return Name == ANONYMOUS_USER; } }

        /// <summary>
        /// Shows if the user has been successfully authenticated.
        /// </summary>
        public bool IsAuthenticated { get; private set; }

        /// <summary>
        /// Constructor for UserCredential.
        /// </summary>
        /// <remarks>By default, the credentials are set to anonymous user.</remarks>
        public UserCredential() : this(ANONYMOUS_USER) {}

        /// <summary>
        /// Constructor for UserCredential.
        /// </summary>
        /// <param name="name">User's login name.</param>
        public UserCredential(string name) : this(name, new SecureString()) { }

        /// <summary>
        /// Constructor for UserCredential.
        /// </summary>
        /// <param name="name">User's login name.</param>
        /// <param name="password">User's password.</param>
        public UserCredential(string name, SecureString password)
        {
            Name = name;
            Password = password;
        }

        #region IIdentity Members

        /// <summary>
        /// </summary>
        public string AuthenticationType
        {
            get { return string.Empty; }
        }

        #endregion
    }
}