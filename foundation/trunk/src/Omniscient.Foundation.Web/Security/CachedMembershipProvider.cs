using System;
using System.Collections;
using System.Configuration.Provider;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;
using Omniscient.Foundation.Logging;
using Omniscient.Foundation.ApplicationModel;

namespace Omniscient.Foundation.Web.Security
{
    public class CachedMembershipProvider<Type> : MembershipProvider where Type : new()
    {
        protected MembershipProvider _provider;
        protected Hashtable _cache;
        private ILogger _logger;

        #region Properties

        public override string ApplicationName
        {
            get
            {
                return _provider.ApplicationName;
            }
            set
            {
                _provider.ApplicationName = value;
            }
        }

        public override bool EnablePasswordReset
        {
            get { return _provider.EnablePasswordReset; }
        }

        public override bool EnablePasswordRetrieval
        {
            get { return false; }
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { return _provider.MaxInvalidPasswordAttempts; }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { return _provider.MinRequiredNonAlphanumericCharacters; }
        }

        public override int MinRequiredPasswordLength
        {
            get { return _provider.MinRequiredPasswordLength; }
        }

        public override int PasswordAttemptWindow
        {
            get { return _provider.PasswordAttemptWindow; }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { return _provider.PasswordFormat; }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { return _provider.PasswordStrengthRegularExpression; }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { return _provider.RequiresQuestionAndAnswer; }
        }

        public override bool RequiresUniqueEmail
        {
            get { return _provider.RequiresUniqueEmail; }
        }

        protected ILogger Logger
        {
            get { return _logger; }
        }
        #endregion

        #region Methods

        public CachedMembershipProvider() : this(new Type() as MembershipProvider) { }

        public CachedMembershipProvider(MembershipProvider provider)
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
            LogDebug("Entering CachedMembershipProvider.Initialize");
            base.Initialize(name, config);
            _provider.Initialize("CachedMembershipProvider", config);
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            LogDebug("Entering ChangePassword");
            bool result = _provider.ChangePassword(username, oldPassword, newPassword);
            if (result)
            {
                if (_cache.ContainsKey(username))
                {
                    UserPassword userPass = (UserPassword)_cache[username];
                    string hashedNewPass = HashPassword(Encoding.Unicode.GetBytes(newPassword), userPass.PasswordKey);
                    userPass.Password = hashedNewPass;
                }
            }
            return result;
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            LogDebug("Entering ChangePasswordQuestionAndAnswer");
            throw new System.NotImplementedException();
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            LogDebug("Entering CreateUser");
            return _provider.CreateUser(username, password, email, passwordQuestion, passwordAnswer, isApproved, providerUserKey, out status);
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            LogDebug("Entering DeleteUser");
            bool result = _provider.DeleteUser(username, deleteAllRelatedData);

            if (result)
            {
                if (_cache.ContainsKey(username))
                {
                    _cache.Remove(username);
                }
            }
            return result;
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            LogDebug("Entering FindUsersByEmail");
            throw new System.NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            LogDebug("Entering FindUsersByName");
            throw new System.NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            LogDebug("Entering GetAllUsers");
            throw new System.NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            LogDebug("Entering GetNumberOfUsersOnline");
            throw new System.NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            LogDebug("Entering GetPassword");
            throw new ProviderException("Cannot retrieve Hashed passwords.");
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            LogDebug("Entering GetUser");
            return _provider.GetUser(username, userIsOnline);
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            LogDebug("Entering GetUser");
            throw new System.NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            LogDebug("Entering GetUserNameByEmail");
            throw new System.NotImplementedException();
        }

        public override string ResetPassword(string username, string answer)
        {
            LogDebug("Entering ResetPassword");
            string newPassword = _provider.ResetPassword(username, answer);

            if (_cache.ContainsKey(username))
            {
                UserPassword userPass = (UserPassword)_cache[username];
                userPass.Password = HashPassword(Encoding.Unicode.GetBytes(newPassword), userPass.PasswordKey);
            }
            return newPassword;
        }

        public override bool UnlockUser(string userName)
        {
            LogDebug("Entering UlockUser");
            throw new System.NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            LogDebug("Entering UpdateUser");
            throw new System.NotImplementedException();
        }

        public override bool ValidateUser(string username, string password)
        {
            bool result;

            LogDebug("Entering ValidateUser");
            if (_cache.ContainsKey(username))
            {
                UserPassword userPass = (UserPassword)_cache[username];
                string hashedPass = HashPassword(Encoding.Unicode.GetBytes(password), userPass.PasswordKey);

                result = hashedPass == userPass.Password;
                if (!result)
                {
                    result = _provider.ValidateUser(username, password);
                    if (result)
                    {
                        _cache[username] = RetrieveUserPassword(username);
                    }
                }
                LogDebug("Returning from cache.  Value: " + result);
                return result;
            }
            result = _provider.ValidateUser(username, password);
            if (result)
            {
                _cache[username] = RetrieveUserPassword(username);
            }
            LogDebug("Returning non-cached.  Value: " + result);
            return result;
        }

        protected virtual UserPassword RetrieveUserPassword(string username)
        {
            LogDebug("Entering RetrieveUserPassword");
            throw new NotImplementedException("Implement password retrieval.");
        }

        private string HashPassword(byte[] password, byte[] passwordKey)
        {
            byte[] result = new byte[password.Length + passwordKey.Length];
            Array.Copy(passwordKey, result, passwordKey.Length);
            Array.Copy(password, 0, result, passwordKey.Length, password.Length);

            HashAlgorithm algorithm = HashAlgorithm.Create(Membership.HashAlgorithmType);

            return Convert.ToBase64String(algorithm.ComputeHash(result));
        }

        #endregion
    }

    public struct UserPassword
    {
        public string Password { get; set; }
        public byte[] PasswordKey { get; set; }
    }
}
