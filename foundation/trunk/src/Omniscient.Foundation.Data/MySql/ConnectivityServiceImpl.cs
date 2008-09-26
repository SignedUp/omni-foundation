using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace Omniscient.Foundation.Data.MySql
{
    internal class ConnectivityServiceImpl: IConnectivity
    {
        private string _connectionStr;
        private Configuration.Connectivity _config;

        public ConnectivityServiceImpl(Configuration.Connectivity config) 
        {
            config = _config;
        }

        #region IConnectivity Members

        public MySqlConnection CreateConnection()
        {
            return new MySqlConnection(_config.ConnectionString);
        }

        #endregion
    }
}
