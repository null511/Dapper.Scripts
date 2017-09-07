﻿using Dapper.Scripts.Connection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Dapper.Scripts
{
    public static class DbConnectionAsyncExtensions
    {
        public static async Task<int> ExecuteScriptAsync(this ISqlScriptConnection connection, string key, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            var sql = connection.GetScriptSql(key, param);
            return await connection.ExecuteAsync(sql, param, transaction, commandTimeout, commandType);
        }

        public static async Task<IDataReader> ExecuteReaderScriptAsync(this ISqlScriptConnection connection, string key, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            var sql = connection.GetScriptSql(key, param);
            return await connection.ExecuteReaderAsync(sql, param, transaction, commandTimeout, commandType);
        }

        public static async Task<object> ExecuteScalarScriptAsync(this ISqlScriptConnection connection, string key, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            var sql = connection.GetScriptSql(key, param);
            return await connection.ExecuteScalarAsync(sql, param, transaction, commandTimeout, commandType);
        }

        public static async Task<T> ExecuteScalarScriptAsync<T>(this ISqlScriptConnection connection, string key, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            var sql = connection.GetScriptSql(key, param);
            return await connection.ExecuteScalarAsync<T>(sql, param, transaction, commandTimeout, commandType);
        }

        public static async Task<IEnumerable<dynamic>> QueryScriptAsync(this ISqlScriptConnection connection, string key, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            var sql = connection.GetScriptSql(key, param);
            return await connection.QueryAsync(sql, param, transaction, commandTimeout, commandType);
        }

        public static async Task<IEnumerable<T>> QueryScriptAsync<T>(this ISqlScriptConnection connection, string key, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            var sql = connection.GetScriptSql(key, param);
            return await connection.QueryAsync<T>(sql, param, transaction, commandTimeout, commandType);
        }

        public static async Task<dynamic> QueryFirstScriptAsync(this ISqlScriptConnection connection, Type type, string key, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            var sql = connection.GetScriptSql(key, param);
            return await connection.QueryFirstAsync(type, sql, param, transaction, commandTimeout, commandType);
        }

        public static async Task<T> QueryFirstScriptAsync<T>(this ISqlScriptConnection connection, string key, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            var sql = connection.GetScriptSql(key, param);
            return await connection.QueryFirstAsync<T>(sql, param, transaction, commandTimeout, commandType);
        }

        public static async Task<dynamic> QueryFirstOrDefaultScriptAsync(this ISqlScriptConnection connection, Type type, string key, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            var sql = connection.GetScriptSql(key, param);
            return await connection.QueryFirstOrDefaultAsync(type, sql, param, transaction, commandTimeout, commandType);
        }

        public static async Task<T> QueryFirstOrDefaultScriptAsync<T>(this ISqlScriptConnection connection, string key, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            var sql = connection.GetScriptSql(key, param);
            return await connection.QueryFirstOrDefaultAsync<T>(sql, param, transaction, commandTimeout, commandType);
        }

        public static async Task<SqlMapper.GridReader> QueryMultipleScriptAsync(this ISqlScriptConnection connection, string key, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            var sql = connection.GetScriptSql(key, param);
            return await connection.QueryMultipleAsync(sql, param, transaction, commandTimeout, commandType);
        }

        public static async Task<dynamic> QuerySingleScriptAsync(this ISqlScriptConnection connection, Type type, string key, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            var sql = connection.GetScriptSql(key, param);
            return await connection.QuerySingleAsync(type, sql, param, transaction, commandTimeout, commandType);
        }

        public static async Task<T> QuerySingleScriptAsync<T>(this ISqlScriptConnection connection, string key, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            var sql = connection.GetScriptSql(key, param);
            return await connection.QuerySingleAsync<T>(sql, param, transaction, commandTimeout, commandType);
        }

        public static async Task<dynamic> QuerySingleOrDefaultScriptAsync(this ISqlScriptConnection connection, Type type, string key, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            var sql = connection.GetScriptSql(key, param);
            return await connection.QuerySingleOrDefaultAsync(type, sql, param, transaction, commandTimeout, commandType);
        }

        public static async Task<T> QuerySingleOrDefaultScriptAsync<T>(this ISqlScriptConnection connection, string key, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            var sql = connection.GetScriptSql(key, param);
            return await connection.QuerySingleOrDefaultAsync<T>(sql, param, transaction, commandTimeout, commandType);
        }
    }
}
