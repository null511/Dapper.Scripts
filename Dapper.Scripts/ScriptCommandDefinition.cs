using Dapper.Scripts.Connection;
using System.Data;
using System.Threading;

namespace Dapper.Scripts
{
    public class ScriptCommandDefinition
    {
        public string Key {get; set;}
        public dynamic Parameters {get; set;}
        public IDbTransaction Transaction {get; set;}
        public int? CommandTimeout {get; set;}
        public CommandType? CommandType {get; set;}
        public CommandFlags Flags {get; set;}
        public CancellationToken CancellationToken {get; set;}


        public CommandDefinition ToSqlDefinition(ISqlScriptConnection connection)
        {
            var sql = connection.GetScriptSql(Key, Parameters);
            return new CommandDefinition(sql, Parameters, Transaction, CommandTimeout, CommandType, Flags, CancellationToken);
        }
    }
}
