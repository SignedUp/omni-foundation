using System.Data;
using MySql.Data.MySqlClient;
using Omniscient.Foundation.Data;

namespace Omniscient.Foundation.Contrib.Data
{
    public class MySqlConnectionProvider : ConnectionProviderBase
    {
        public MySqlConnectionProvider(string connectionStringName) : base(connectionStringName) { }

        public override IDbConnection CreateConnection()
        {
            return new MySqlConnection(ConnectionString);
        }
    }
}
