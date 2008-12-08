using System;
using System.Collections;
using System.Configuration;
using System.Configuration.Provider;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;

namespace Omniscient.Foundation.Web.Security
{
    public class CachedMembershipProvider<Type> : MembershipProvider where Type : new()
    {
        protected MembershipProvider _provider;
        protected Hashtable _cache;

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

        #endregion

        #region Methods

        public CachedMembershipProvider() : this(new Type() as MembershipProvider) { }

        public CachedMembershipProvider(MembershipProvider provider)
        {
            _provider = provider;
            _cache = new Hashtable();
        }

        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
        {
            base.Initialize(name, config);
            _provider.Initialize("CachedMembershipProvider", config);
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
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
            throw new System.NotImplementedException();
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            return _provider.CreateUser(username, password, email, passwordQuestion, passwordAnswer, isApproved, providerUserKey, out status);
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
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
            throw new System.NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new System.NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new System.NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new System.NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new ProviderException("Cannot retrieve Hashed passwords.");
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            return _provider.GetUser(username, userIsOnline);
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new System.NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new System.NotImplementedException();
        }

        public override string ResetPassword(string username, string answer)
        {
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
            throw new System.NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new System.NotImplementedException();
        }

        public override bool ValidateUser(string username, string password)
        {
            bool result;

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
                return result;
            }
            result = _provider.ValidateUser(username, password);
            if (result)
            {
                _cache[username] = RetrieveUserPassword(username);
            }
            return result;
        }

        protected virtual UserPassword RetrieveUserPassword(string username)
        {
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
