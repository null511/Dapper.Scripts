using Dapper.Scripts.Collection;
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Dapper.Scripts.Connection
{
    public class SqlConnectionCreatedEventArgs : EventArgs
    {
        public IDbConnection Connection {get; set;}
    }

    public class SqlScriptConnectionFactory
    {
        public event EventHandler<SqlConnectionCreatedEventArgs> ConnectionCreated;

        private readonly ISqlScriptCollection scriptCollection;

        public string ConnectionString {get; set;}


        public SqlScriptConnectionFactory(ISqlScriptCollection scripts)
        {
            scriptCollection = scripts;
        }

        /// <summary>
        /// Creates a new database connection.
        /// </summary>
        public SqlScriptConnection Connect()
        {
            var dbConnection = OnCreateConnection();
            OnConnectionCreated(dbConnection);

            return new SqlScriptConnection(scriptCollection, dbConnection);
        }

        /// <summary>
        /// Creates and opens a new database connection.
        /// </summary>
        public SqlScriptConnection Open()
        {
            var dbConnection = OnCreateConnection();
            OnConnectionCreated(dbConnection);

            var connection = new SqlScriptConnection(scriptCollection, dbConnection);

            try {
                connection.Open();
                return connection;
            }
            catch {
                connection.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Creates and opens a new database connection asynchronously.
        /// </summary>
        public async Task<SqlScriptConnection> OpenAsync()
        {
            var dbConnection = OnCreateConnection();
            OnConnectionCreated(dbConnection);

            var connection = new SqlScriptConnection(scriptCollection, dbConnection);

            try {
                await connection.OpenAsync();
                return connection;
            }
            catch {
                connection.Dispose();
                throw;
            }
        }

        public void Run(Action<ISqlScriptConnection> dbAction)
        {
            if (dbAction == null) throw new ArgumentNullException(nameof(dbAction));

            using (var session = Connect()) {
                dbAction.Invoke(session);
            }
        }

        public T Run<T>(Func<ISqlScriptConnection, T> dbAction)
        {
            if (dbAction == null) throw new ArgumentNullException(nameof(dbAction));

            using (var session = Connect()) {
                return dbAction.Invoke(session);
            }
        }

        public async Task RunAsync(Func<ISqlScriptConnection, Task> dbAction)
        {
            if (dbAction == null) throw new ArgumentNullException(nameof(dbAction));

            using (var session = Connect()) {
                await dbAction.Invoke(session);
            }
        }

        public async Task<T> RunAsync<T>(Func<ISqlScriptConnection, Task<T>> dbAction)
        {
            if (dbAction == null) throw new ArgumentNullException(nameof(dbAction));

            using (var session = Connect()) {
                return await dbAction.Invoke(session);
            }
        }

        protected virtual DbConnection OnCreateConnection()
        {
            return new SqlConnection(ConnectionString);
        }

        private void OnConnectionCreated(DbConnection connection)
        {
            try {
                ConnectionCreated?.Invoke(this, new SqlConnectionCreatedEventArgs {
                    Connection = connection,
                });
            }
            catch {}
        }
    }
}
