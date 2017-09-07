using Dapper.Scripts.Collection;
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace Dapper.Scripts.Connection
{
    [System.ComponentModel.DesignerCategory("")]
    public class SqlScriptConnection : DbConnection, ISqlScriptConnection
    {
        private readonly ISqlScriptCollection scriptCollection;

        public SqlConnection SqlConnection {get;}

        public override string ConnectionString {
            get => SqlConnection.ConnectionString;
            set => SqlConnection.ConnectionString = value;
        }

        public override int ConnectionTimeout => SqlConnection.ConnectionTimeout;
        public override string Database => SqlConnection.Database;
        public override string DataSource => SqlConnection.DataSource;
        public override string ServerVersion => SqlConnection.ServerVersion;
        public override ConnectionState State => SqlConnection.State;


        public SqlScriptConnection(ISqlScriptCollection scripts, SqlConnection connection)
        {
            this.scriptCollection = scripts ?? throw new ArgumentNullException(nameof(scripts));
            this.SqlConnection = connection ?? throw new ArgumentNullException(nameof(connection));
        }

        protected override void Dispose(bool disposing)
        {
            SqlConnection?.Dispose();

            base.Dispose(disposing);
        }

        public override void Open()
        {
            SqlConnection.Open();
        }

        public override Task OpenAsync(CancellationToken cancellationToken)
        {
            return SqlConnection.OpenAsync(cancellationToken);
        }

        public override void Close()
        {
            SqlConnection.Close();
        }

        protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
        {
            return SqlConnection.BeginTransaction();
        }

        protected override DbCommand CreateDbCommand()
        {
            return SqlConnection.CreateCommand();
        }

        public override void ChangeDatabase(string databaseName)
        {
            SqlConnection.ChangeDatabase(databaseName);
        }

        public string GetScriptSql(string key, object param = null)
        {
            return scriptCollection.GetScriptSql(key, param);
        }
    }
}
