using System.Data;

namespace Omniscient.Foundation.Data
{
    ///<summary>
    /// Provides a database connection for DB access.
    ///</summary>
    public interface IConnectionProvider
    {
        ///<summary>
        /// Creates a database connection.
        ///</summary>
        ///<returns>A database connection</returns>
        IDbConnection CreateConnection();
    }
}
