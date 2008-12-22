﻿using System;
using System.Collections;
using System.Configuration;
using System.Web.Security;
using MySql.Data.MySqlClient;
using Omniscient.Foundation.ApplicationModel;
using Omniscient.Foundation.Data.MySql;

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
            IConnectivity connectivity = ApplicationManager.Current.ServiceProvider.GetService<IConnectivity>();

            if (connectivity == null) throw new Exception("Connectivity service not found.");

            UserPassword userPass = new UserPassword();

            using (MySqlConnection connection = connectivity.CreateConnection())
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
