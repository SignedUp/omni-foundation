using System.Data;
using MySql.Data.MySqlClient;
using Omniscient.Foundation.Contrib.Data.MySql.Configuration;
using Omniscient.Foundation.Data;

namespace Omniscient.Foundation.Contrib.Data.MySql
{
    internal class ConnectivityServiceImpl: IConnectionProvider
    {
        private Connectivity _config;

        public ConnectivityServiceImpl(Connectivity config) 
        {
            _config = config;
        }

        #region IConnectivity Members

        public IDbConnection CreateConnection()
        {
            return new MySqlConnection(_config.ConnectionString);
        }

        #endregion
    }
}