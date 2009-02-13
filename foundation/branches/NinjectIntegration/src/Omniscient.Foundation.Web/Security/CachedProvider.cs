using System;
using System.Collections.Generic;
using System.Web.Security;
using System.Collections;
using Omniscient.Foundation.Security;
using Omniscient.Foundation.Logging;
using Omniscient.Foundation.ApplicationModel;

namespace Omniscient.Foundation.Web.Security
{
    public class CachedRoleProvider<Type> : RoleProvider, IRoleProvider where Type : new()
    {
        private RoleProvider _provider;
        private Hashtable _cache;
        private ILogger _logger;

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
            ILoggingService service;
            service = ApplicationManager.Current.ServiceProvider.GetService<ILoggingService>();
            if (service == null) return;
            _logger = service.GetLogger(GetType());
        }

        protected void LogDebug(object message)
        {
            if (_logger == null) return;
            _logger.Debug(message);
        }

        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
        {
            LogDebug("Entering CachedProvider.Initialize");
            base.Initialize(name, config);
            _provider.Initialize("CachedRoleProvider", config);
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            LogDebug("Entering CachedProvider.AddUsersToRoles");
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
            LogDebug("Entering CachedProvider.CreateRole");
            _provider.CreateRole(roleName);
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            LogDebug("Entering CachedProvider.DeleteRole");
            bool result;
            result = _provider.DeleteRole(roleName, throwOnPopulatedRole);

            return result;
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            LogDebug("Entering CachedProvider.FindUsersInRole");
            string[] users;
            users = _provider.FindUsersInRole(roleName, usernameToMatch);

            return users;
        }

        public override string[] GetAllRoles()
        {
            LogDebug("Entering CachedProvider.GetAllRoles");
            string[] roles;
            roles = _provider.GetAllRoles();

            return roles;
        }

        public override string[] GetRolesForUser(string username)
        {
            LogDebug("Entering CachedProvider.GetRolesForUser");
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
            LogDebug("Entering CachedProvider.GetUsersInRole");
            string[] users;
            users = _provider.GetUsersInRole(roleName);

            return users;
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            LogDebug("Entering CachedProvider.IsUserInRole");
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
            return result;
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            LogDebug("Entering CachedProvider.RemoveUsersFromRoles");
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
            LogDebug("Entering CachedProvider.RoleExists");
            bool result;
            result = _provider.RoleExists(roleName);

            return result;
        }

        #region IRoleProvider Members

        string[] IRoleProvider.GetRolesForUser(string username)
        {
            LogDebug("Entering IRoleProvider.GetRolesForUser");
            return GetRolesForUser(username);
        }

        #endregion
    }
}
