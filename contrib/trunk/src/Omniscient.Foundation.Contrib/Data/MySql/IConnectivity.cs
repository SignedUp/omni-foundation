using MySql.Data.MySqlClient;

namespace Omniscient.Foundation.Contrib.Data.MySql
{
    public interface IConnectivity
    {
        MySqlConnection CreateConnection();
    }
}