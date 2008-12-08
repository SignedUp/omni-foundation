using System;
using System.Collections.Generic;
using System.Web.Security;
using System.Collections;
using Omniscient.Foundation.Security;

namespace Omniscient.Foundation.Web.Security
{
    public class CachedRoleProvider<Type> : IRoleProvider, RoleProvider where Type : new()
    {
        private RoleProvider _provider;
        private Hashtable _cache;

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public CachedRoleProvider() : this(new Type() as RoleProvider) { }

        public CachedRoleProvider(RoleProvider provider)
        {
            _provider = provider;
            _cache = new Hashtable();
        }

        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
        {
            base.Initialize(name, config);
            _provider.Initialize("CachedRoleProvider", config);
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            foreach (string username in usernames)
            {
                if (_cache.ContainsKey(username))
                {
                    foreach (string roleName in roleNames)
                    {
                        List<string> roles = (List<string>)_cache[username];
                        if (!roles.Contains(roleName))
                        {
                            roles.Add(roleName);
                        }
                    }
                }
            }
            _provider.AddUsersToRoles(usernames, roleNames);
        }

        public override void CreateRole(string roleName)
        {
            _provider.CreateRole(roleName);
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            bool result;
            result = _provider.DeleteRole(roleName, throwOnPopulatedRole);

            return result;
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            string[] users;
            users = _provider.FindUsersInRole(roleName, usernameToMatch);

            return users;
        }

        public override string[] GetAllRoles()
        {
            string[] roles;
            roles = _provider.GetAllRoles();

            return roles;
        }

        public override string[] GetRolesForUser(string username)
        {
            string[] roles;

            if (_cache.ContainsKey(username))
            {
                roles = ((List<string>)_cache[username]).ToArray();
                return roles;
            }
            roles = _provider.GetRolesForUser(username);
            if (roles != null && roles.Length > 0)
            {
                _cache[username] = new List<string>(roles);
            }
            return roles;
        }

        public override string[] GetUsersInRole(string roleName)
        {
            string[] users;
            users = _provider.GetUsersInRole(roleName);

            return users;
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            bool result;
            List<string> roles;

            if (_cache.ContainsKey(username))
            {
                roles = (List<string>)_cache[username];
                result = roles.Contains(roleName);
                if (!result)
                {
                    result = _provider.IsUserInRole(username, roleName);
                    if (result)
                    {
                        roles.Add(roleName);
                    }
                }
                return result;
            }
            result = _provider.IsUserInRole(username, roleName);
            if (result)
            {
                roles = new List<string>(GetRolesForUser(username));
            }
            return result;
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            foreach (string username in usernames)
            {
                if (_cache.Contains(username))
                {
                    List<string> roles = (List<string>)_cache[username];
                    foreach (string role in roleNames)
                    {
                        roles.Remove(role);
                    }
                }
            }
            _provider.RemoveUsersFromRoles(usernames, roleNames);
        }

        public override bool RoleExists(string roleName)
        {
            bool result;
            result = _provider.RoleExists(roleName);

            return result;
        }

        #region IRoleProvider Members

        string[] IRoleProvider.GetRolesForUser(string username)
        {
            return GetRolesForUser(username);
        }

        #endregion
    }
}
