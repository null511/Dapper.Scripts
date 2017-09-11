using System.Data;
using System.Data.SqlClient;

namespace Dapper.Scripts.Connection
{
    public interface ISqlScriptConnection : IDbConnection
    {
        SqlConnection SqlConnection {get;}

        string GetScriptSql(string key, object param = null);
    }
}
