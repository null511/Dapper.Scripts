using System.Data;

#if NETCOREAPP3_1
using System.Data.SqlClient;
#else
using Microsoft.Data.SqlClient;
#endif

namespace Dapper.Scripts.Connection
{
    /// <summary>
    /// Represents an open connection to a data source
    /// that is bound to a collection of SQL scripts.
    /// </summary>
    public interface ISqlScriptConnection : IDbConnection
    {
        /// <summary>
        /// Gets the data source connection.
        /// </summary>
        SqlConnection SqlConnection {get;}

        /// <summary>
        /// Gets a SQL script from the bound collection.
        /// </summary>
        /// <param name="key">SQL script identifier.</param>
        /// <param name="param">Optional collection of values for transforming SQL script.</param>
        string GetScriptSql(string key, object param = null);
    }
}
