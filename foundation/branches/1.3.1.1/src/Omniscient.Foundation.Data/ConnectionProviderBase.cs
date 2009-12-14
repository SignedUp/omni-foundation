using System.Configuration;
using System.Data;
using System;

namespace Omniscient.Foundation.Data
{
    public abstract class ConnectionProviderBase : IConnectionProvider
    {
        private string _connectionString;
        protected string ConnectionString { get { return _connectionString; } }

        public ConnectionProviderBase(string connectionStringName)
        {
            if (string.IsNullOrEmpty(connectionStringName)) throw new ArgumentNullException("connectionStringName");

            ConnectionStringSettings settings;
            settings = ConfigurationManager.ConnectionStrings[connectionStringName];

            if (settings == null)
            {
                string message; 
                message = string.Format("Can't find connection string named {0}.", connectionStringName);
                throw new ConfigurationErrorsException(message);
            }
            _connectionString = settings.ConnectionString;
        }

        public abstract IDbConnection CreateConnection();
    }
}
