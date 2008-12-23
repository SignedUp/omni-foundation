using MySql.Data.MySqlClient;
using Omniscient.Foundation.Contrib.Data.MySql.Configuration;

namespace Omniscient.Foundation.Contrib.Data.MySql
{
    internal class ConnectivityServiceImpl: IConnectivity
    {
        private Connectivity _config;

        public ConnectivityServiceImpl(Connectivity config) 
        {
            _config = config;
        }

        #region IConnectivity Members

        public MySqlConnection CreateConnection()
        {
            return new MySqlConnection(_config.ConnectionString);
        }

        #endregion
    }
}