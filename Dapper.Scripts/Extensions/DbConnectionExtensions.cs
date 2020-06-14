using Dapper.Scripts.Collection;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace Dapper.Scripts
{
    /// <summary>
    /// Collection of extension methods for using Dapper with Scripts.
    /// </summary>
    public static class DbConnectionExtensions
    {
        /// <summary>
        /// Execute a parameterized SQL script.
        /// </summary>
        /// <param name="connection">The database connection.</param>
        /// <param name="scripts">The collection of scripts used to populate the commands SQL text.</param>
        /// <param name="command">The command to execute on this connection.</param>
        public static int ExecuteScript(this DbConnection connection, ISqlScriptCollection scripts, ScriptCommandDefinition command)
        {
            return connection.Execute(command.ToSqlDefinition(scripts));
        }

        /// <summary>
        /// Execute a parameterized SQL script.
        /// </summary>
        /// <param name="connection">The database connection.</param>
        /// <param name="scripts">The collection of scripts used to populate the commands SQL text.</param>
        /// <param name="key">The named identifier of the SQL script.</param>
        /// <param name="param">Optional collection of parameters used by SQL script and transformer.</param>
        /// <param name="transaction">The transaction to use for this query.</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        public static int ExecuteScript(this DbConnection connection, ISqlScriptCollection scripts, string key, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            var sql = scripts.GetScriptSql(key, param);
            return connection.Execute(sql, param, transaction, commandTimeout, commandType);
        }

        /// <summary>
        /// Execute a parameterized SQL script and return an <seealso cref="IDataReader"/>.
        /// </summary>
        /// <param name="connection">The database connection.</param>
        /// <param name="scripts">The collection of scripts used to populate the commands SQL text.</param>
        /// <param name="command">The command to execute on this connection.</param>
        public static IDataReader ExecuteReaderScript(this DbConnection connection, ISqlScriptCollection scripts, ScriptCommandDefinition command)
        {
            return connection.ExecuteReader(command.ToSqlDefinition(scripts));
        }

        /// <summary>
        /// Execute a parameterized SQL script and return an <seealso cref="IDataReader"/>.
        /// </summary>
        /// <param name="connection">The database connection.</param>
        /// <param name="scripts">The collection of scripts used to populate the commands SQL text.</param>
        /// <param name="key">The named identifier of the SQL script.</param>
        /// <param name="param">Optional collection of parameters used by SQL script and transformer.</param>
        /// <param name="transaction">The transaction to use for this query.</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        public static IDataReader ExecuteReaderScript(this DbConnection connection, ISqlScriptCollection scripts, string key, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            var sql = scripts.GetScriptSql(key, param);
            return connection.ExecuteReader(sql, param, transaction, commandTimeout, commandType);
        }

        /// <summary>
        /// Execute a parameterized SQL script that returns a single value.
        /// </summary>
        /// <param name="connection">The database connection.</param>
        /// <param name="scripts">The collection of scripts used to populate the commands SQL text.</param>
        /// <param name="command">The command to execute on this connection.</param>
        public static object ExecuteScalarScript(this DbConnection connection, ISqlScriptCollection scripts, ScriptCommandDefinition command)
        {
            return connection.ExecuteScalar(command.ToSqlDefinition(scripts));
        }

        /// <summary>
        /// Execute a parameterized SQL script that returns a single value.
        /// </summary>
        /// <param name="connection">The database connection.</param>
        /// <param name="scripts">The collection of scripts used to populate the commands SQL text.</param>
        /// <param name="key">The named identifier of the SQL script.</param>
        /// <param name="param">Optional collection of parameters used by SQL script and transformer.</param>
        /// <param name="transaction">The transaction to use for this query.</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        public static object ExecuteScalarScript(this DbConnection connection, ISqlScriptCollection scripts, string key, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            var sql = scripts.GetScriptSql(key, param);
            return connection.ExecuteScalar(sql, param, transaction, commandTimeout, commandType);
        }

        /// <summary>
        /// Execute a parameterized SQL script that returns a single value.
        /// </summary>
        /// <param name="connection">The database connection.</param>
        /// <param name="scripts">The collection of scripts used to populate the commands SQL text.</param>
        /// <param name="command">The command to execute on this connection.</param>
        public static T ExecuteScalarScript<T>(this DbConnection connection, ISqlScriptCollection scripts, ScriptCommandDefinition command)
        {
            return connection.ExecuteScalar<T>(command.ToSqlDefinition(scripts));
        }

        /// <summary>
        /// Execute a parameterized SQL script that returns a single value.
        /// </summary>
        /// <param name="connection">The database connection.</param>
        /// <param name="scripts">The collection of scripts used to populate the commands SQL text.</param>
        /// <param name="key">The named identifier of the SQL script.</param>
        /// <param name="param">Optional collection of parameters used by SQL script and transformer.</param>
        /// <param name="transaction">The transaction to use for this query.</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        public static T ExecuteScalarScript<T>(this DbConnection connection, ISqlScriptCollection scripts, string key, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            var sql = scripts.GetScriptSql(key, param);
            return connection.ExecuteScalar<T>(sql, param, transaction, commandTimeout, commandType);
        }

        /// <summary>
        /// Return a sequence of dynamic objects with properties matching the columns.
        /// </summary>
        /// <param name="connection">The database connection.</param>
        /// <param name="scripts">The collection of scripts used to populate the commands SQL text.</param>
        /// <param name="key">The named identifier of the SQL script.</param>
        /// <param name="param">Optional collection of parameters used by SQL script and transformer.</param>
        /// <param name="transaction">The transaction to use for this query.</param>
        /// <param name="buffered">Whether to buffer the results in memory.</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        public static IEnumerable<dynamic> QueryScript(this DbConnection connection, ISqlScriptCollection scripts, string key, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            var sql = scripts.GetScriptSql(key, param);
            return connection.Query(sql, param, transaction, buffered, commandTimeout, commandType);
        }

        /// <summary>
        /// Executes a query, returning the data typed as per <typeparamref name="T"/>.
        /// </summary>
        /// <param name="connection">The database connection.</param>
        /// <param name="scripts">The collection of scripts used to populate the commands SQL text.</param>
        /// <param name="command">The command to execute on this connection.</param>
        public static IEnumerable<T> QueryScript<T>(this DbConnection connection, ISqlScriptCollection scripts, ScriptCommandDefinition command)
        {
            return connection.Query<T>(command.ToSqlDefinition(scripts));
        }

        /// <summary>
        /// Executes a query, returning the data typed as per <typeparamref name="T"/>.
        /// </summary>
        /// <param name="connection">The database connection.</param>
        /// <param name="scripts">The collection of scripts used to populate the commands SQL text.</param>
        /// <param name="key">The named identifier of the SQL script.</param>
        /// <param name="param">Optional collection of parameters used by SQL script and transformer.</param>
        /// <param name="transaction">The transaction to use for this query.</param>
        /// <param name="buffered">Whether to buffer the results in memory.</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        public static IEnumerable<T> QueryScript<T>(this DbConnection connection, ISqlScriptCollection scripts, string key, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            var sql = scripts.GetScriptSql(key, param);
            return connection.Query<T>(sql, param, transaction, buffered, commandTimeout, commandType);
        }

        /// <summary>
        /// Return a dynamic object with properties matching the columns.
        /// </summary>
        /// <param name="connection">The database connection.</param>
        /// <param name="scripts">The collection of scripts used to populate the commands SQL text.</param>
        /// <param name="key">The named identifier of the SQL script.</param>
        /// <param name="param">Optional collection of parameters used by SQL script and transformer.</param>
        /// <param name="transaction">The transaction to use for this query.</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        public static dynamic QueryFirstScript(this DbConnection connection, ISqlScriptCollection scripts, string key, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            var sql = scripts.GetScriptSql(key, param);
            return connection.QueryFirst(sql, param, transaction, commandTimeout, commandType);
        }

        /// <summary>
        /// Executes a single-row query, returning the data typed as per <typeparamref name="T"/>.
        /// </summary>
        /// <param name="connection">The database connection.</param>
        /// <param name="scripts">The collection of scripts used to populate the commands SQL text.</param>
        /// <param name="command">The command to execute on this connection.</param>
        public static T QueryFirstScript<T>(this DbConnection connection, ISqlScriptCollection scripts, ScriptCommandDefinition command)
        {
            return connection.QueryFirst<T>(command.ToSqlDefinition(scripts));
        }

        /// <summary>
        /// Executes a single-row query, returning the data typed as per <typeparamref name="T"/>.
        /// </summary>
        /// <param name="connection">The database connection.</param>
        /// <param name="scripts">The collection of scripts used to populate the commands SQL text.</param>
        /// <param name="key">The named identifier of the SQL script.</param>
        /// <param name="param">Optional collection of parameters used by SQL script and transformer.</param>
        /// <param name="transaction">The transaction to use for this query.</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        public static T QueryFirstScript<T>(this DbConnection connection, ISqlScriptCollection scripts, string key, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            var sql = scripts.GetScriptSql(key, param);
            return connection.QueryFirst<T>(sql, param, transaction, commandTimeout, commandType);
        }

        /// <summary>
        /// Return a dynamic object with properties matching the columns.
        /// </summary>
        /// <param name="connection">The database connection.</param>
        /// <param name="scripts">The collection of scripts used to populate the commands SQL text.</param>
        /// <param name="key">The named identifier of the SQL script.</param>
        /// <param name="param">Optional collection of parameters used by SQL script and transformer.</param>
        /// <param name="transaction">The transaction to use for this query.</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        public static dynamic QueryFirstOrDefaultScript(this DbConnection connection, ISqlScriptCollection scripts, string key, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            var sql = scripts.GetScriptSql(key, param);
            return connection.QueryFirstOrDefault(sql, param, transaction, commandTimeout, commandType);
        }

        /// <summary>
        /// Executes a single-row query, returning the data typed as per <typeparamref name="T"/>.
        /// </summary>
        /// <param name="connection">The database connection.</param>
        /// <param name="scripts">The collection of scripts used to populate the commands SQL text.</param>
        /// <param name="command">The command to execute on this connection.</param>
        public static T QueryFirstOrDefaultScript<T>(this DbConnection connection, ISqlScriptCollection scripts, ScriptCommandDefinition command)
        {
            return connection.QueryFirstOrDefault<T>(command.ToSqlDefinition(scripts));
        }

        /// <summary>
        /// Executes a single-row query, returning the data typed as per <typeparamref name="T"/>.
        /// </summary>
        /// <param name="connection">The database connection.</param>
        /// <param name="scripts">The collection of scripts used to populate the commands SQL text.</param>
        /// <param name="key">The named identifier of the SQL script.</param>
        /// <param name="param">Optional collection of parameters used by SQL script and transformer.</param>
        /// <param name="transaction">The transaction to use for this query.</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        public static T QueryFirstOrDefaultScript<T>(this DbConnection connection, ISqlScriptCollection scripts, string key, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            var sql = scripts.GetScriptSql(key, param);
            return connection.QueryFirstOrDefault<T>(sql, param, transaction, commandTimeout, commandType);
        }

        /// <summary>
        /// Execute a command that returns multiple result sets, and access each in turn.
        /// </summary>
        /// <param name="connection">The database connection.</param>
        /// <param name="scripts">The collection of scripts used to populate the commands SQL text.</param>
        /// <param name="command">The command to execute on this connection.</param>
        public static SqlMapper.GridReader QueryMultipleScript(this DbConnection connection, ISqlScriptCollection scripts, ScriptCommandDefinition command)
        {
            return connection.QueryMultiple(command.ToSqlDefinition(scripts));
        }

        /// <summary>
        /// Execute a command that returns multiple result sets, and access each in turn.
        /// </summary>
        /// <param name="connection">The database connection.</param>
        /// <param name="scripts">The collection of scripts used to populate the commands SQL text.</param>
        /// <param name="key">The named identifier of the SQL script.</param>
        /// <param name="param">Optional collection of parameters used by SQL script and transformer.</param>
        /// <param name="transaction">The transaction to use for this query.</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        public static SqlMapper.GridReader QueryMultipleScript(this DbConnection connection, ISqlScriptCollection scripts, string key, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            var sql = scripts.GetScriptSql(key, param);
            return connection.QueryMultiple(sql, param, transaction, commandTimeout, commandType);
        }

        /// <summary>
        /// Return a dynamic object with properties matching the columns.
        /// </summary>
        /// <param name="connection">The database connection.</param>
        /// <param name="scripts">The collection of scripts used to populate the commands SQL text.</param>
        /// <param name="key">The named identifier of the SQL script.</param>
        /// <param name="param">Optional collection of parameters used by SQL script and transformer.</param>
        /// <param name="transaction">The transaction to use for this query.</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        public static dynamic QuerySingleScript(this DbConnection connection, ISqlScriptCollection scripts, string key, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            var sql = scripts.GetScriptSql(key, param);
            return connection.QuerySingle(sql, param, transaction, commandTimeout, commandType);
        }

        /// <summary>
        /// Executes a single-row query, returning the data typed as per <typeparamref name="T"/>.
        /// </summary>
        /// <param name="connection">The database connection.</param>
        /// <param name="scripts">The collection of scripts used to populate the commands SQL text.</param>
        /// <param name="command">The command to execute on this connection.</param>
        public static T QuerySingleScript<T>(this DbConnection connection, ISqlScriptCollection scripts, ScriptCommandDefinition command)
        {
            return connection.QuerySingle<T>(command.ToSqlDefinition(scripts));
        }

        /// <summary>
        /// Executes a single-row query, returning the data typed as per <typeparamref name="T"/>.
        /// </summary>
        /// <param name="connection">The database connection.</param>
        /// <param name="scripts">The collection of scripts used to populate the commands SQL text.</param>
        /// <param name="key">The named identifier of the SQL script.</param>
        /// <param name="param">Optional collection of parameters used by SQL script and transformer.</param>
        /// <param name="transaction">The transaction to use for this query.</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        public static T QuerySingleScript<T>(this DbConnection connection, ISqlScriptCollection scripts, string key, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            var sql = scripts.GetScriptSql(key, param);
            return connection.QuerySingle<T>(sql, param, transaction, commandTimeout, commandType);
        }

        /// <summary>
        /// Return a dynamic object with properties matching the columns.
        /// </summary>
        /// <param name="connection">The database connection.</param>
        /// <param name="scripts">The collection of scripts used to populate the commands SQL text.</param>
        /// <param name="key">The named identifier of the SQL script.</param>
        /// <param name="param">Optional collection of parameters used by SQL script and transformer.</param>
        /// <param name="transaction">The transaction to use for this query.</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        public static dynamic QuerySingleOrDefaultScript(this DbConnection connection, ISqlScriptCollection scripts, string key, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            var sql = scripts.GetScriptSql(key, param);
            return connection.QuerySingleOrDefault(sql, param, transaction, commandTimeout, commandType);
        }

        /// <summary>
        /// Executes a single-row query, returning the data typed as per <typeparamref name="T"/>.
        /// </summary>
        /// <param name="connection">The database connection.</param>
        /// <param name="scripts">The collection of scripts used to populate the commands SQL text.</param>
        /// <param name="command">The command to execute on this connection.</param>
        public static T QuerySingleOrDefaultScript<T>(this DbConnection connection, ISqlScriptCollection scripts, ScriptCommandDefinition command)
        {
            return connection.QuerySingleOrDefault<T>(command.ToSqlDefinition(scripts));
        }

        /// <summary>
        /// Executes a single-row query, returning the data typed as per <typeparamref name="T"/>.
        /// </summary>
        /// <param name="connection">The database connection.</param>
        /// <param name="scripts">The collection of scripts used to populate the commands SQL text.</param>
        /// <param name="key">The named identifier of the SQL script.</param>
        /// <param name="param">Optional collection of parameters used by SQL script and transformer.</param>
        /// <param name="transaction">The transaction to use for this query.</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        public static T QuerySingleOrDefaultScript<T>(this DbConnection connection, ISqlScriptCollection scripts, string key, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            var sql = scripts.GetScriptSql(key, param);
            return connection.QuerySingleOrDefault<T>(sql, param, transaction, commandTimeout, commandType);
        }
    }
}
