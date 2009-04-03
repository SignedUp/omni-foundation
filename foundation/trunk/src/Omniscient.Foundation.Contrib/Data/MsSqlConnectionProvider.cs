using System.Data;
using System.Data.SqlClient;
using Omniscient.Foundation.Data;

namespace Omniscient.Foundation.Contrib.Data
{
    public class MsSqlConnectionProvider : ConnectionProviderBase
    {
        public MsSqlConnectionProvider(string connectionStringName) : base(connectionStringName) {}

        public override IDbConnection CreateConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}
