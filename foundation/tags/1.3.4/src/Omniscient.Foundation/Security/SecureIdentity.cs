using System;
using System.Security.Principal;
using System.Runtime.Serialization;
using System.Security;

namespace Omniscient.Foundation.Security
{
    /// <summary>
    /// Implementation of <see href="IIdentity"</see> that allows the identity to be "promoted", that is, go from anonymous (non-authenticated)
    /// to authenticated with a name, password and an authentication type.
    /// </summary>
    [DataContract]
    public class SecureIdentity: IIdentity
    {
        private string _name;
        private string _authType;
#if SILVERLIGHT
        private string _password;
#else
        private SecureString _password;
#endif

        /// <summary>
        /// Creates an anonymous (unauthenticated) user.
        /// </summary>
        public SecureIdentity() : this(string.Empty, null, string.Empty) { }
        
        /// <summary>
        /// Creates an authenticated user with given name and password.
        /// </summary>
        /// <param name="name">The name of the authenticated user.</param>
        /// <param name="password">The password used to authenticate the user.</param>
        /// <remarks>The password is allowed to be null.</remarks>
#if SILVERLIGHT
        public SecureIdentity(string name, string password) : this(name, password, string.Empty) { }
#else
        public SecureIdentity(string name, SecureString password) : this(name, password, string.Empty) { }
#endif
            
        
        /// <summary>
        /// Creates an authenticated user with given name, password and authentication machanism.
        /// </summary>
        /// <param name="name">The name of the authenticated user.</param>
        /// <param name="password">The password used to authenticate the user.</param>
        /// <param name="authenticationType">The authentication mechanism used to authenticate the user.</param>
        /// <remarks>The password is allowed to be null.</remarks>
#if SILVERLIGHT
        public SecureIdentity(string name, string password, string authenticationType)            
#else
        public SecureIdentity(string name, SecureString password, string authenticationType)
#endif

        {
            if (name == null) name = string.Empty;
            if (authenticationType == null) authenticationType = string.Empty;
            _name = name;
            _authType = authenticationType;
            _password = password;
        }

        /// <summary>
        /// Promotes the identity with a username and password.  The identity is then considered authenticated.
        /// </summary>
        /// <remarks>The password is allowed to be null.</remarks>
        /// <param name="name">The username used to authenticate.</param>
        /// <param name="password">The password used to authenticate.</param>
#if SILVERLIGHT
        public void Promote(string name, string password)
#else
        public void Promote(string name, SecureString password)
#endif
        {
            Promote(name, password, string.Empty);
        }

        /// <summary>
        /// Promotes the identity with a username.  The identity is then considered authenticated.
        /// </summary>
        /// <param name="name">The username used to authenticate.</param>
        /// <param name="password">The password used to authenticate.</param>
        /// <param name="authenticationType">The authentication type used to authenticate.</param>
#if SILVERLIGHT
        public void Promote(string name, string password, string authenticationType)
#else
        public void Promote(string name, SecureString password, string authenticationType)
#endif
        {
            if (name == null) name = string.Empty;
            if (authenticationType == null) authenticationType = string.Empty;
            _name = name;
            _authType = authenticationType;
            _password = password;
        }

        /// <summary>
        /// Gets the password used to promote (authenticate) this Identity, or null if no password was supplied or if the 
        /// identity is anonymous (unauthenticated).
        /// </summary>
#if SILVERLIGHT
        public string Password
#else
        public SecureString Password
#endif
        {
            get { return _password; }
        }

        #region IIdentity Members

        /// <summary>
        /// Gets the authentication mechanism used to authenticate the user.
        /// </summary>
        [DataMember]
        public string AuthenticationType
        {
            get { return _authType; }
            private set { _authType = value; }
        }

        /// <summary>
        /// Gets a boolean that indicates whether the user has been authenticated.
        /// </summary>
        public bool IsAuthenticated
        {
            get { return _name != string.Empty; }
        }

        /// <summary>
        /// Gets the name of the user represented by this Identity.
        /// </summary>
        [DataMember]
        public string Name
        {
            get { return _name; }
            private set { _name = value; }
        }

        #endregion
    }
}
