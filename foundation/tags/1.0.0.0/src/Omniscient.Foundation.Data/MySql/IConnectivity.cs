using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace Omniscient.Foundation.Data.MySql
{
    public interface IConnectivity
    {
        MySqlConnection CreateConnection();
    }
}
