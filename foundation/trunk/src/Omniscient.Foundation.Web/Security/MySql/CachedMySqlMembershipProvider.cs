using System;
using System.Collections;
using System.Configuration;
using System.Web.Security;
using MySql.Data.MySqlClient;

namespace Omniscient.Foundation.Web.Security.MySql
{
    public class CachedMySqlMembershipProvider<Type> : CachedMembershipProvider<Type> where Type : new()
    {
        public CachedMySqlMembershipProvider() : this(new Type() as MembershipProvider) { }

        public CachedMySqlMembershipProvider(MembershipProvider provider)
        {
            _provider = provider;
            _cache = new Hashtable();
        }

        protected override UserPassword RetrieveUserPassword(string username)
        {
            UserPassword userPass = new UserPassword();
            string connectionString =
                ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string sql =
                    @"SELECT Password, PasswordKey " +
                     "FROM User u " +
                     "JOIN Membership m ON m.userId=u.id " +
                     "WHERE u.Name=@name";
                MySqlCommand command = new MySqlCommand(sql, connection);
                command.Parameters.AddWithValue("@name", username);
                MySqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    userPass.Password = (string)reader["Password"];
                    userPass.PasswordKey = Convert.FromBase64String((string)reader["PasswordKey"]);
                }
            }
            return userPass;
        }
    }
}
