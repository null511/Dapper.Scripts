using Dapper.Scripts.Connection;
using System.Collections.Generic;
using System.Data;

namespace Dapper.Scripts
{
    public static class DbConnectionExtensions
    {
        public static int ExecuteScript(this ISqlScriptConnection connection, string key, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            var sql = connection.GetScriptSql(key, param);
            return connection.Execute(sql, param, transaction, commandTimeout, commandType);
        }

        public static IDataReader ExecuteReaderScript(this ISqlScriptConnection connection, string key, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            var sql = connection.GetScriptSql(key, param);
            return connection.ExecuteReader(sql, param, transaction, commandTimeout, commandType);
        }

        public static object ExecuteScalarScript(this ISqlScriptConnection connection, string key, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            var sql = connection.GetScriptSql(key, param);
            return connection.ExecuteScalar(sql, param, transaction, commandTimeout, commandType);
        }

        public static T ExecuteScalarScript<T>(this ISqlScriptConnection connection, string key, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            var sql = connection.GetScriptSql(key, param);
            return connection.ExecuteScalar<T>(sql, param, transaction, commandTimeout, commandType);
        }

        public static IEnumerable<dynamic> QueryScript(this ISqlScriptConnection connection, string key, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            var sql = connection.GetScriptSql(key, param);
            return connection.Query(sql, param, transaction, buffered, commandTimeout, commandType);
        }

        public static IEnumerable<T> QueryScript<T>(this ISqlScriptConnection connection, string key, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            var sql = connection.GetScriptSql(key, param);
            return connection.Query<T>(sql, param, transaction, buffered, commandTimeout, commandType);
        }

        public static dynamic QueryFirstScript(this ISqlScriptConnection connection, string key, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            var sql = connection.GetScriptSql(key, param);
            return connection.QueryFirst(sql, param, transaction, commandTimeout, commandType);
        }

        public static T QueryFirstScript<T>(this ISqlScriptConnection connection, string key, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            var sql = connection.GetScriptSql(key, param);
            return connection.QueryFirst<T>(sql, param, transaction, commandTimeout, commandType);
        }

        public static dynamic QueryFirstOrDefaultScript(this ISqlScriptConnection connection, string key, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            var sql = connection.GetScriptSql(key, param);
            return connection.QueryFirstOrDefault(sql, param, transaction, commandTimeout, commandType);
        }

        public static T QueryFirstOrDefaultScript<T>(this ISqlScriptConnection connection, string key, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            var sql = connection.GetScriptSql(key, param);
            return connection.QueryFirstOrDefault<T>(sql, param, transaction, commandTimeout, commandType);
        }

        public static SqlMapper.GridReader QueryMultipleScript(this ISqlScriptConnection connection, string key, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            var sql = connection.GetScriptSql(key, param);
            return connection.QueryMultiple(sql, param, transaction, commandTimeout, commandType);
        }

        public static dynamic QuerySingleScript(this ISqlScriptConnection connection, string key, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            var sql = connection.GetScriptSql(key, param);
            return connection.QuerySingle(sql, param, transaction, commandTimeout, commandType);
        }

        public static T QuerySingleScript<T>(this ISqlScriptConnection connection, string key, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            var sql = connection.GetScriptSql(key, param);
            return connection.QuerySingle<T>(sql, param, transaction, commandTimeout, commandType);
        }

        public static dynamic QuerySingleOrDefaultScript(this ISqlScriptConnection connection, string key, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            var sql = connection.GetScriptSql(key, param);
            return connection.QuerySingleOrDefault(sql, param, transaction, commandTimeout, commandType);
        }

        public static T QuerySingleOrDefaultScript<T>(this ISqlScriptConnection connection, string key, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            var sql = connection.GetScriptSql(key, param);
            return connection.QuerySingleOrDefault<T>(sql, param, transaction, commandTimeout, commandType);
        }
    }
}
