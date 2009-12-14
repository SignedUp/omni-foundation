using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Runtime.Serialization;

namespace Omniscient.Foundation.Security
{
    /// <summary>
    /// Implementation of <see href="IPrincipal"</see> that allows the principal to be "promoted", that is, add roles to it.
    /// </summary>
    [DataContract]
    public class SecurePrincipal : IPrincipal
    {
        private SecureIdentity _identity;
        [DataMember]
        private List<string> _roles;
        
        /// <summary>
        /// Creates an instance of SecurePrincipal with an anonymous (unauthenticated) <see href="SecureIdentity"</see>, and an empty collection
        /// of roles.
        /// </summary>
        public SecurePrincipal() : this(new SecureIdentity(), new string[] { }) { }

        /// <summary>
        /// Creates an instance of SecurePrincipal with an existing <see href="SecureIdentity"</see>, and an initial collection of roles.
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="roles"></param>
        public SecurePrincipal(SecureIdentity identity, string[] roles)
        {
            _identity = identity;
            _roles = new List<string>();
            foreach (string s in roles)
                if (!string.IsNullOrEmpty(s)) _roles.Add(s);

            
        }

        /// <summary>
        /// Adds specified roles to the collection of roles.
        /// </summary>
        /// <param name="roles">Roles to add to the roles collection.</param>
        public void Promote(string[] roles)
        {
            foreach (string s in roles)
                if (!string.IsNullOrEmpty(s)) _roles.Add(s);            
        }

        /// <summary>
        /// Gets a string array of all roles this principal is granted.
        /// </summary>
        public string[] AllRoles
        {
            get { return _roles.ToArray(); }
        }

        #region IPrincipal Members

        /// <summary>
        /// Gets the <see href="IIdentity"</see> of that principal.  Concrete type is <see href="SecureIdentity"</see>.
        /// </summary>
        IIdentity IPrincipal.Identity
        {
            get { return Identity; }
        }

        /// <summary>
        /// Gets the <see href="SecureIdentity"</see> of that principal.
        /// </summary>
        [DataMember]
        public SecureIdentity Identity
        {
            get { return _identity; }
            private set { _identity = value; }
        }

        /// <summary>
        /// Returns true if that principal is granted with given role.  Otherwise, false.
        /// </summary>
        /// <param name="role">The role to test.</param>
        /// <returns>True if that principal is granted with role <paramref name="role"/>.  Otherwise, false.</returns>
        public bool IsInRole(string role)
        {
            if (string.IsNullOrEmpty(role)) return false;
            foreach (string givenRole in _roles)
            {
                if (string.Compare(givenRole, role, StringComparison.OrdinalIgnoreCase) == 0) return true;
            }
            return false;
        }

        #endregion
    }
}
