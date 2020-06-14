using Dapper.Scripts.Collection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace Dapper.Scripts
{
    /// <summary>
    /// Collection of extension methods for using Dapper with Scripts asynchronously.
    /// </summary>
    public static class DbConnectionAsyncExtensions
    {
        /// <summary>
        /// Execute a SQL script asynchronously.
        /// </summary>
        /// <param name="connection">The database connection.</param>
        /// <param name="scripts">The collection of scripts used to populate the commands SQL text.</param>
        /// <param name="command">The command to execute on this connection.</param>
        public static async Task<int> ExecuteScriptAsync(this DbConnection connection, ISqlScriptCollection scripts, ScriptCommandDefinition command)
        {
            return await connection.ExecuteAsync(command.ToSqlDefinition(scripts));
        }

        /// <summary>
        /// Execute a SQL script asynchronously.
        /// </summary>
        /// <param name="connection">The database connection.</param>
        /// <param name="scripts">The collection of scripts used to populate the commands SQL text.</param>
        /// <param name="key">The named identifier of the SQL script.</param>
        /// <param name="param">Optional collection of parameters used by SQL script and transformer.</param>
        /// <param name="transaction">The transaction to use for this query.</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        public static async Task<int> ExecuteScriptAsync(this DbConnection connection, ISqlScriptCollection scripts, string key, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            var sql = scripts.GetScriptSql(key, param);
            return await connection.ExecuteAsync(sql, param, transaction, commandTimeout, commandType);
        }

        /// <summary>
        /// Execute a parameterized SQL script asynchronously and return an <seealso cref="IDataReader"/>.
        /// </summary>
        /// <param name="connection">The database connection.</param>
        /// <param name="scripts">The collection of scripts used to populate the commands SQL text.</param>
        /// <param name="command">The command to execute on this connection.</param>
        public static async Task<IDataReader> ExecuteReaderScriptAsync(this DbConnection connection, ISqlScriptCollection scripts, ScriptCommandDefinition command)
        {
            return await connection.ExecuteReaderAsync(command.ToSqlDefinition(scripts));
        }

        /// <summary>
        /// Execute a parameterized SQL script asynchronously and return an <seealso cref="IDataReader"/>.
        /// </summary>
        /// <param name="connection">The database connection.</param>
        /// <param name="scripts">The collection of scripts used to populate the commands SQL text.</param>
        /// <param name="key">The named identifier of the SQL script.</param>
        /// <param name="param">Optional collection of parameters used by SQL script and transformer.</param>
        /// <param name="transaction">The transaction to use for this query.</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        public static async Task<IDataReader> ExecuteReaderScriptAsync(this DbConnection connection, ISqlScriptCollection scripts, string key, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            var sql = scripts.GetScriptSql(key, param);
            return await connection.ExecuteReaderAsync(sql, param, transaction, commandTimeout, commandType);
        }

        /// <summary>
        /// Execute a parameterized SQL script asynchronously that selects a single value.
        /// </summary>
        /// <param name="connection">The database connection.</param>
        /// <param name="scripts">The collection of scripts used to populate the commands SQL text.</param>
        /// <param name="command">The command to execute on this connection.</param>
        public static async Task<object> ExecuteScalarScriptAsync(this DbConnection connection, ISqlScriptCollection scripts, ScriptCommandDefinition command)
        {
            return await connection.ExecuteScalarAsync(command.ToSqlDefinition(scripts));
        }

        /// <summary>
        /// Execute a parameterized SQL script asynchronously that selects a single value.
        /// </summary>
        /// <param name="connection">The database connection.</param>
        /// <param name="scripts">The collection of scripts used to populate the commands SQL text.</param>
        /// <param name="key">The named identifier of the SQL script.</param>
        /// <param name="param">Optional collection of parameters used by SQL script and transformer.</param>
        /// <param name="transaction">The transaction to use for this query.</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        public static async Task<object> ExecuteScalarScriptAsync(this DbConnection connection, ISqlScriptCollection scripts, string key, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            var sql = scripts.GetScriptSql(key, param);
            return await connection.ExecuteScalarAsync(sql, param, transaction, commandTimeout, commandType);
        }

        /// <summary>
        /// Execute a parameterized SQL script asynchronously that selects a single value.
        /// </summary>
        /// <param name="connection">The database connection.</param>
        /// <param name="scripts">The collection of scripts used to populate the commands SQL text.</param>
        /// <param name="command">The command to execute on this connection.</param>
        public static async Task<T> ExecuteScalarScriptAsync<T>(this DbConnection connection, ISqlScriptCollection scripts, ScriptCommandDefinition command)
        {
            return await connection.ExecuteScalarAsync<T>(command.ToSqlDefinition(scripts));
        }

        /// <summary>
        /// Execute a parameterized SQL script asynchronously that selects a single value.
        /// </summary>
        /// <param name="connection">The database connection.</param>
        /// <param name="scripts">The collection of scripts used to populate the commands SQL text.</param>
        /// <param name="key">The named identifier of the SQL script.</param>
        /// <param name="param">Optional collection of parameters used by SQL script and transformer.</param>
        /// <param name="transaction">The transaction to use for this query.</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        public static async Task<T> ExecuteScalarScriptAsync<T>(this DbConnection connection, ISqlScriptCollection scripts, string key, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            var sql = scripts.GetScriptSql(key, param);
            return await connection.ExecuteScalarAsync<T>(sql, param, transaction, commandTimeout, commandType);
        }

        /// <summary>
        /// Execute a SQL script query asynchronously using Task.
        /// </summary>
        /// <param name="connection">The database connection.</param>
        /// <param name="scripts">The collection of scripts used to populate the commands SQL text.</param>
        /// <param name="command">The command to execute on this connection.</param>
        public static async Task<IEnumerable<dynamic>> QueryScriptAsync(this DbConnection connection, ISqlScriptCollection scripts, ScriptCommandDefinition command)
        {
            return await connection.QueryAsync(command.ToSqlDefinition(scripts));
        }

        /// <summary>
        /// Execute a SQL script query asynchronously using Task.
        /// </summary>
        /// <param name="connection">The database connection.</param>
        /// <param name="scripts">The collection of scripts used to populate the commands SQL text.</param>
        /// <param name="key">The named identifier of the SQL script.</param>
        /// <param name="param">Optional collection of parameters used by SQL script and transformer.</param>
        /// <param name="transaction">The transaction to use for this query.</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        public static async Task<IEnumerable<dynamic>> QueryScriptAsync(this DbConnection connection, ISqlScriptCollection scripts, string key, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            var sql = scripts.GetScriptSql(key, param);
            return await connection.QueryAsync(sql, param, transaction, commandTimeout, commandType);
        }

        /// <summary>
        /// Execute a SQL script query asynchronously using Task.
        /// </summary>
        /// <param name="connection">The database connection.</param>
        /// <param name="scripts">The collection of scripts used to populate the commands SQL text.</param>
        /// <param name="command">The command to execute on this connection.</param>
        public static async Task<IEnumerable<T>> QueryScriptAsync<T>(this DbConnection connection, ISqlScriptCollection scripts, ScriptCommandDefinition command)
        {
            return await connection.QueryAsync<T>(command.ToSqlDefinition(scripts));
        }

        /// <summary>
        /// Execute a SQL script query asynchronously using Task.
        /// </summary>
        /// <param name="connection">The database connection.</param>
        /// <param name="scripts">The collection of scripts used to populate the commands SQL text.</param>
        /// <param name="key">The named identifier of the SQL script.</param>
        /// <param name="param">Optional collection of parameters used by SQL script and transformer.</param>
        /// <param name="transaction">The transaction to use for this query.</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        public static async Task<IEnumerable<T>> QueryScriptAsync<T>(this DbConnection connection, ISqlScriptCollection scripts, string key, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            var sql = scripts.GetScriptSql(key, param);
            return await connection.QueryAsync<T>(sql, param, transaction, commandTimeout, commandType);
        }

        /// <summary>
        /// Execute a single-row SQL script query asynchronously using Task.
        /// </summary>
        /// <param name="connection">The database connection.</param>
        /// <param name="scripts">The collection of scripts used to populate the commands SQL text.</param>
        /// <param name="command">The command to execute on this connection.</param>
        public static async Task<dynamic> QueryFirstScriptAsync(this DbConnection connection, ISqlScriptCollection scripts, ScriptCommandDefinition command)
        {
            return await connection.QueryFirstAsync(command.ToSqlDefinition(scripts));
        }

        /// <summary>
        /// Execute a single-row SQL script query asynchronously using Task.
        /// </summary>
        /// <param name="connection">The database connection.</param>
        /// <param name="scripts">The collection of scripts used to populate the commands SQL text.</param>
        /// <param name="type">The type to return.</param>
        /// <param name="key">The named identifier of the SQL script.</param>
        /// <param name="param">Optional collection of parameters used by SQL script and transformer.</param>
        /// <param name="transaction">The transaction to use for this query.</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        public static async Task<dynamic> QueryFirstScriptAsync(this DbConnection connection, ISqlScriptCollection scripts, Type type, string key, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            var sql = scripts.GetScriptSql(key, param);
            return await connection.QueryFirstAsync(type, sql, param, transaction, commandTimeout, commandType);
        }

        /// <summary>
        /// Execute a single-row SQL script query asynchronously using Task.
        /// </summary>
        /// <param name="connection">The database connection.</param>
        /// <param name="scripts">The collection of scripts used to populate the commands SQL text.</param>
        /// <param name="command">The command to execute on this connection.</param>
        public static async Task<T> QueryFirstScriptAsync<T>(this DbConnection connection, ISqlScriptCollection scripts, ScriptCommandDefinition command)
        {
            return await connection.QueryFirstAsync<T>(command.ToSqlDefinition(scripts));
        }

        /// <summary>
        /// Execute a single-row SQL script query asynchronously using Task.
        /// </summary>
        /// <param name="connection">The database connection.</param>
        /// <param name="scripts">The collection of scripts used to populate the commands SQL text.</param>
        /// <param name="key">The named identifier of the SQL script.</param>
        /// <param name="param">Optional collection of parameters used by SQL script and transformer.</param>
        /// <param name="transaction">The transaction to use for this query.</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        public static async Task<T> QueryFirstScriptAsync<T>(this DbConnection connection, ISqlScriptCollection scripts, string key, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            var sql = scripts.GetScriptSql(key, param);
            return await connection.QueryFirstAsync<T>(sql, param, transaction, commandTimeout, commandType);
        }

        /// <summary>
        /// Execute a single-row SQL script query asynchronously using Task.
        /// </summary>
        /// <param name="connection">The database connection.</param>
        /// <param name="scripts">The collection of scripts used to populate the commands SQL text.</param>
        /// <param name="command">The command to execute on this connection.</param>
        public static async Task<dynamic> QueryFirstOrDefaultScriptAsync(this DbConnection connection, ISqlScriptCollection scripts, ScriptCommandDefinition command)
        {
            return await connection.QueryFirstOrDefaultAsync(command.ToSqlDefinition(scripts));
        }

        /// <summary>
        /// Execute a single-row SQL script query asynchronously using Task.
        /// </summary>
        /// <param name="connection">The database connection.</param>
        /// <param name="scripts">The collection of scripts used to populate the commands SQL text.</param>
        /// <param name="type">The type to return.</param>
        /// <param name="key">The named identifier of the SQL script.</param>
        /// <param name="param">Optional collection of parameters used by SQL script and transformer.</param>
        /// <param name="transaction">The transaction to use for this query.</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        public static async Task<dynamic> QueryFirstOrDefaultScriptAsync(this DbConnection connection, ISqlScriptCollection scripts, Type type, string key, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            var sql = scripts.GetScriptSql(key, param);
            return await connection.QueryFirstOrDefaultAsync(type, sql, param, transaction, commandTimeout, commandType);
        }

        /// <summary>
        /// Execute a single-row SQL script query asynchronously using Task.
        /// </summary>
        /// <param name="connection">The database connection.</param>
        /// <param name="scripts">The collection of scripts used to populate the commands SQL text.</param>
        /// <param name="command">The command to execute on this connection.</param>
        public static async Task<T> QueryFirstOrDefaultScriptAsync<T>(this DbConnection connection, ISqlScriptCollection scripts, ScriptCommandDefinition command)
        {
            return await connection.QueryFirstOrDefaultAsync<T>(command.ToSqlDefinition(scripts));
        }

        /// <summary>
        /// Execute a single-row SQL script query asynchronously using Task.
        /// </summary>
        /// <param name="connection">The database connection.</param>
        /// <param name="scripts">The collection of scripts used to populate the commands SQL text.</param>
        /// <param name="key">The named identifier of the SQL script.</param>
        /// <param name="param">Optional collection of parameters used by SQL script and transformer.</param>
        /// <param name="transaction">The transaction to use for this query.</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        public static async Task<T> QueryFirstOrDefaultScriptAsync<T>(this DbConnection connection, ISqlScriptCollection scripts, string key, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            var sql = scripts.GetScriptSql(key, param);
            return await connection.QueryFirstOrDefaultAsync<T>(sql, param, transaction, commandTimeout, commandType);
        }

        /// <summary>
        /// Execute a SQL script that returns multiple result sets, and access each in turn.
        /// </summary>
        /// <param name="connection">The database connection.</param>
        /// <param name="scripts">The collection of scripts used to populate the commands SQL text.</param>
        /// <param name="command">The command to execute on this connection.</param>
        public static async Task<SqlMapper.GridReader> QueryMultipleScriptAsync(this DbConnection connection, ISqlScriptCollection scripts, ScriptCommandDefinition command)
        {
            return await connection.QueryMultipleAsync(command.ToSqlDefinition(scripts));
        }

        /// <summary>
        /// Execute a SQL script that returns multiple result sets, and access each in turn.
        /// </summary>
        /// <param name="connection">The database connection.</param>
        /// <param name="scripts">The collection of scripts used to populate the commands SQL text.</param>
        /// <param name="key">The named identifier of the SQL script.</param>
        /// <param name="param">Optional collection of parameters used by SQL script and transformer.</param>
        /// <param name="transaction">The transaction to use for this query.</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        public static async Task<SqlMapper.GridReader> QueryMultipleScriptAsync(this DbConnection connection, ISqlScriptCollection scripts, string key, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            var sql = scripts.GetScriptSql(key, param);
            return await connection.QueryMultipleAsync(sql, param, transaction, commandTimeout, commandType);
        }

        /// <summary>
        /// Execute a single-row SQL script query asynchronously using Task.
        /// </summary>
        /// <param name="connection">The database connection.</param>
        /// <param name="scripts">The collection of scripts used to populate the commands SQL text.</param>
        /// <param name="command">The command to execute on this connection.</param>
        public static async Task<dynamic> QuerySingleScriptAsync(this DbConnection connection, ISqlScriptCollection scripts, ScriptCommandDefinition command)
        {
            return await connection.QuerySingleAsync(command.ToSqlDefinition(scripts));
        }

        /// <summary>
        /// Execute a single-row SQL script query asynchronously using Task.
        /// </summary>
        /// <param name="connection">The database connection.</param>
        /// <param name="scripts">The collection of scripts used to populate the commands SQL text.</param>
        /// <param name="type">The type to return.</param>
        /// <param name="key">The named identifier of the SQL script.</param>
        /// <param name="param">Optional collection of parameters used by SQL script and transformer.</param>
        /// <param name="transaction">The transaction to use for this query.</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        public static async Task<dynamic> QuerySingleScriptAsync(this DbConnection connection, ISqlScriptCollection scripts, Type type, string key, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            var sql = scripts.GetScriptSql(key, param);
            return await connection.QuerySingleAsync(type, sql, param, transaction, commandTimeout, commandType);
        }

        /// <summary>
        /// Execute a single-row SQL script query asynchronously using Task.
        /// </summary>
        /// <param name="connection">The database connection.</param>
        /// <param name="scripts">The collection of scripts used to populate the commands SQL text.</param>
        /// <param name="command">The command to execute on this connection.</param>
        public static async Task<T> QuerySingleScriptAsync<T>(this DbConnection connection, ISqlScriptCollection scripts, ScriptCommandDefinition command)
        {
            return await connection.QuerySingleAsync<T>(command.ToSqlDefinition(scripts));
        }

        /// <summary>
        /// Execute a single-row SQL script query asynchronously using Task.
        /// </summary>
        /// <param name="connection">The database connection.</param>
        /// <param name="scripts">The collection of scripts used to populate the commands SQL text.</param>
        /// <param name="key">The named identifier of the SQL script.</param>
        /// <param name="param">Optional collection of parameters used by SQL script and transformer.</param>
        /// <param name="transaction">The transaction to use for this query.</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        public static async Task<T> QuerySingleScriptAsync<T>(this DbConnection connection, ISqlScriptCollection scripts, string key, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            var sql = scripts.GetScriptSql(key, param);
            return await connection.QuerySingleAsync<T>(sql, param, transaction, commandTimeout, commandType);
        }

        /// <summary>
        /// Execute a single-row SQL script query asynchronously using Task.
        /// </summary>
        /// <param name="connection">The database connection.</param>
        /// <param name="scripts">The collection of scripts used to populate the commands SQL text.</param>
        /// <param name="command">The command to execute on this connection.</param>
        public static async Task<dynamic> QuerySingleOrDefaultScriptAsync(this DbConnection connection, ISqlScriptCollection scripts, ScriptCommandDefinition command)
        {
            return await connection.QuerySingleOrDefaultAsync(command.ToSqlDefinition(scripts));
        }

        /// <summary>
        /// Execute a single-row SQL script query asynchronously using Task.
        /// </summary>
        /// <param name="connection">The database connection.</param>
        /// <param name="scripts">The collection of scripts used to populate the commands SQL text.</param>
        /// <param name="type">The type to return.</param>
        /// <param name="key">The named identifier of the SQL script.</param>
        /// <param name="param">Optional collection of parameters used by SQL script and transformer.</param>
        /// <param name="transaction">The transaction to use for this query.</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        public static async Task<dynamic> QuerySingleOrDefaultScriptAsync(this DbConnection connection, ISqlScriptCollection scripts, Type type, string key, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            var sql = scripts.GetScriptSql(key, param);
            return await connection.QuerySingleOrDefaultAsync(type, sql, param, transaction, commandTimeout, commandType);
        }

        /// <summary>
        /// Execute a single-row SQL script query asynchronously using Task.
        /// </summary>
        /// <param name="connection">The database connection.</param>
        /// <param name="scripts">The collection of scripts used to populate the commands SQL text.</param>
        /// <param name="command">The command to execute on this connection.</param>
        public static async Task<T> QuerySingleOrDefaultScriptAsync<T>(this DbConnection connection, ISqlScriptCollection scripts, ScriptCommandDefinition command)
        {
            return await connection.QuerySingleOrDefaultAsync<T>(command.ToSqlDefinition(scripts));
        }

        /// <summary>
        /// Execute a single-row SQL script query asynchronously using Task.
        /// </summary>
        /// <param name="connection">The database connection.</param>
        /// <param name="scripts">The collection of scripts used to populate the commands SQL text.</param>
        /// <param name="key">The named identifier of the SQL script.</param>
        /// <param name="param">Optional collection of parameters used by SQL script and transformer.</param>
        /// <param name="transaction">The transaction to use for this query.</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        public static async Task<T> QuerySingleOrDefaultScriptAsync<T>(this DbConnection connection, ISqlScriptCollection scripts, string key, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            var sql = scripts.GetScriptSql(key, param);
            return await connection.QuerySingleOrDefaultAsync<T>(sql, param, transaction, commandTimeout, commandType);
        }
    }
}
