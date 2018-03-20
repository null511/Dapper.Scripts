using Dapper.Scripts.Collection;
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace Dapper.Scripts.Connection
{
    /// <summary>
    /// Binds a database connection to a collection of SQL scripts.
    /// </summary>
    [System.ComponentModel.DesignerCategory("")]
    public class SqlScriptConnection : DbConnection, ISqlScriptConnection
    {
        private readonly ISqlScriptCollection scriptCollection;
        private readonly DbConnection baseConnection;

        /// <summary>
        /// Gets the current connection as a <seealso cref="System.Data.SqlClient.SqlConnection"/>.
        /// </summary>
        public SqlConnection SqlConnection => baseConnection as SqlConnection;

        /// <summary>
        /// Gets or Sets the string used to open a database connection.
        /// </summary>
        public override string ConnectionString {
            get => baseConnection.ConnectionString;
            set => baseConnection.ConnectionString = value;
        }

        /// <summary>
        /// Gets the time to wait while establishing a connection
        /// before terminating the attempt and generating an error.
        /// </summary>
        public override int ConnectionTimeout => baseConnection.ConnectionTimeout;

        /// <summary>
        /// Gets the name of the current database after a connection
        /// is opened, or the database name specified in the connection
        /// string before the connection is opened.
        /// </summary>
        public override string Database => baseConnection.Database;

        /// <summary>
        /// Gets the name of the instance of SQL Server to which to connect.
        /// </summary>
        public override string DataSource => SqlConnection?.DataSource;

        /// <summary>
        /// Gets a string that contains the version of the instance
        /// of SQL server to which the client is connected.
        /// </summary>
        public override string ServerVersion => SqlConnection?.ServerVersion;

        /// <summary>
        /// Gets the state of the connection.
        /// </summary>
        public override ConnectionState State => baseConnection.State;


        /// <summary>
        /// Creates a new instance of <see cref="SqlScriptCollection"/>
        /// binding the provided database connection and script collection.
        /// </summary>
        /// <param name="scripts"></param>
        /// <param name="connection"></param>
        public SqlScriptConnection(ISqlScriptCollection scripts, DbConnection connection)
        {
            this.scriptCollection = scripts ?? throw new ArgumentNullException(nameof(scripts));
            this.baseConnection = connection ?? throw new ArgumentNullException(nameof(connection));
        }

        /// <summary>
        /// Releases all resources used by the connection.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            baseConnection?.Dispose();

            base.Dispose(disposing);
        }

        /// <summary>
        /// Opens a connection to the database.
        /// </summary>
        public override void Open()
        {
            baseConnection.Open();
        }

        /// <summary>
        /// Opens a connection to the database asynchronously.
        /// </summary>
        /// <param name="cancellationToken">Token for cancelling connection request.</param>
        public override async Task OpenAsync(CancellationToken cancellationToken)
        {
            if (baseConnection is SqlConnection sqlConnection)
                await sqlConnection.OpenAsync(cancellationToken);
            else
                baseConnection.Open();
        }

        /// <summary>
        /// Closes the connection to the database.
        /// </summary>
        public override void Close()
        {
            baseConnection.Close();
        }

        /// <summary>
        /// Starts a database transaction with the specified isolation level.
        /// </summary>
        /// <param name="isolationLevel">The isolation level under which the transaction should run.</param>
        /// <exception cref="SqlException"/>
        /// <exception cref="InvalidOperationException"/>
        protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
        {
            if (baseConnection is SqlConnection sqlConnection)
                return sqlConnection.BeginTransaction(isolationLevel);
            else
                return baseConnection.BeginTransaction(isolationLevel);
        }

        /// <summary>
        /// Creates and returns a <seealso cref="SqlCommand"/>
        /// object associated with the connection.
        /// </summary>
        protected override DbCommand CreateDbCommand()
        {
            if (baseConnection is SqlConnection sqlConnection)
                return sqlConnection.CreateCommand();
            else
                return baseConnection.CreateCommand();
        }

        /// <summary>
        /// Changes the current database for an open connection.
        /// </summary>
        /// <param name="database">The name of the database to use.</param>
        public override void ChangeDatabase(string database)
        {
            if (baseConnection is SqlConnection sqlConnection)
                sqlConnection.ChangeDatabase(database);
            else
                baseConnection.ChangeDatabase(database);
        }

        /// <summary>
        /// Gets the SQL script identified by '<paramref name="key"/>', optionally transformed by '<paramref name="valueCollection"/>'.
        /// </summary>
        /// <param name="key">The scripts key.</param>
        /// <param name="valueCollection">Optional collection of values for transforming script.</param>
        public string GetScriptSql(string key, object valueCollection = null)
        {
            return scriptCollection.GetScriptSql(key, valueCollection);
        }
    }
}
