using System.Data;

namespace Dapper.Scripts.Connection
{
    public interface ISqlScriptConnection : IDbConnection
    {
        string GetScriptSql(string key, object param = null);
    }
}
