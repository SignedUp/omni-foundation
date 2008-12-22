﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace Omniscient.Foundation.Data.MySql
{
    internal class ConnectivityServiceImpl: IConnectivity
    {
        private Configuration.Connectivity _config;

        public ConnectivityServiceImpl(Configuration.Connectivity config) 
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