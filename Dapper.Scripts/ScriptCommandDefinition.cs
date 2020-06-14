using Dapper.Scripts.Collection;
using Dapper.Scripts.Connection;
using System.Data;
using System.Threading;

namespace Dapper.Scripts
{
    /// <summary>
    /// Describes a command based on a SQL script.
    /// </summary>
    public class ScriptCommandDefinition
    {
        /// <summary>
        /// The key identifying the SQL script.
        /// </summary>
        public string Key {get; set;}

        /// <summary>
        /// Gets or sets the parameters used with the command.
        /// </summary>
        public object Parameters {get; set;}
        
        /// <summary>
        /// Gets or sets the active transaction for the command.
        /// </summary>
        public IDbTransaction Transaction {get; set;}

        /// <summary>
        /// Gets or sets the timeout for the command, in seconds.
        /// </summary>
        public int? CommandTimeout {get; set;}

        /// <summary>
        /// Gets or sets the type of command that the SQL script represents.
        /// </summary>
        public CommandType? CommandType {get; set;}

        /// <summary>
        /// Gets or sets any additional state flags for the command.
        /// </summary>
        public CommandFlags Flags {get; set;}

        /// <summary>
        /// Gets or sets the cancellation token used for asynchronous commands.
        /// </summary>
        public CancellationToken CancellationToken {get; set;}


        /// <summary>
        /// Creates a new instance of <see cref="CommandDefinition"/>
        /// using the current settings.
        /// </summary>
        /// <param name="connection">The script connection used to execute the command.</param>
        public CommandDefinition ToSqlDefinition(ISqlScriptConnection connection)
        {
            var sql = connection.GetScriptSql(Key, Parameters);
            return new CommandDefinition(sql, Parameters, Transaction, CommandTimeout, CommandType, Flags, CancellationToken);
        }

        /// <summary>
        /// Creates a new instance of <see cref="CommandDefinition"/>
        /// using the current settings.
        /// </summary>
        /// <param name="scripts">The collection of scripts used to populate the commands SQL text.</param>
        public CommandDefinition ToSqlDefinition(ISqlScriptCollection scripts)
        {
            var sql = scripts.GetScriptSql(Key, Parameters);
            return new CommandDefinition(sql, Parameters, Transaction, CommandTimeout, CommandType, Flags, CancellationToken);
        }
    }
}
