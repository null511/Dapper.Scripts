using Dapper.Scripts.Collection;
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Dapper.Scripts.Connection
{
    /// <summary>
    /// Creates database connections bound to a collection of SQL scripts.
    /// </summary>
    public class SqlScriptConnectionFactory
    {
        private readonly ISqlScriptCollection scriptCollection;

        /// <summary>
        /// Occurs when a database connection is created, before being returned to the caller.
        /// </summary>
        public event EventHandler<SqlConnectionCreatedEventArgs> ConnectionCreated;

        /// <summary>
        /// Connection String used for creating database connections.
        /// </summary>
        public string ConnectionString {get; set;}


        /// <summary>
        /// Creates a new instance of the <see cref="SqlScriptConnectionFactory"/>.
        /// </summary>
        /// <param name="scripts">The collection of SQL scripts to use for connection bindings.</param>
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

        /// <summary>
        /// Creates a database connection for the provided
        /// <paramref name="dbAction"/>, and closes it after completion.
        /// </summary>
        /// <param name="dbAction">The action to perform using the created connection.</param>
        public void Run(Action<ISqlScriptConnection> dbAction)
        {
            if (dbAction == null) throw new ArgumentNullException(nameof(dbAction));

            using (var session = Connect()) {
                dbAction.Invoke(session);
            }
        }

        /// <summary>
        /// Creates a database connection for the provided
        /// <paramref name="dbAction"/>, and closes it after completion.
        /// </summary>
        /// <typeparam name="T">Type of result returned by <paramref name="dbAction"/>.</typeparam>
        /// <param name="dbAction">The action to perform using the created connection.</param>
        public T Run<T>(Func<ISqlScriptConnection, T> dbAction)
        {
            if (dbAction == null) throw new ArgumentNullException(nameof(dbAction));

            using (var session = Connect()) {
                return dbAction.Invoke(session);
            }
        }

        /// <summary>
        /// Creates a database connection for the provided
        /// <paramref name="dbAction"/>, and closes it after completion.
        /// </summary>
        public async Task RunAsync(Func<ISqlScriptConnection, Task> dbAction)
        {
            if (dbAction == null) throw new ArgumentNullException(nameof(dbAction));

            using (var session = Connect()) {
                await dbAction.Invoke(session);
            }
        }

        /// <summary>
        /// Creates a database connection for the provided
        /// <paramref name="dbAction"/>, and closes it after completion.
        /// </summary>
        /// <typeparam name="T">Type of result returned by <paramref name="dbAction"/>.</typeparam>
        public async Task<T> RunAsync<T>(Func<ISqlScriptConnection, Task<T>> dbAction)
        {
            if (dbAction == null) throw new ArgumentNullException(nameof(dbAction));

            using (var session = Connect()) {
                return await dbAction.Invoke(session);
            }
        }

        /// <summary>
        /// Creates a connection to the database.
        /// </summary>
        protected virtual DbConnection OnCreateConnection()
        {
            return new SqlConnection(ConnectionString);
        }

        private void OnConnectionCreated(IDbConnection connection)
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
