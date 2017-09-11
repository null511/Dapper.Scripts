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
        private readonly DbConnection baseConnection;

        public SqlConnection SqlConnection => baseConnection as SqlConnection;

        public override string ConnectionString {
            get => baseConnection.ConnectionString;
            set => baseConnection.ConnectionString = value;
        }

        public override int ConnectionTimeout => baseConnection.ConnectionTimeout;
        public override string Database => baseConnection.Database;
        public override string DataSource => SqlConnection?.DataSource;
        public override string ServerVersion => SqlConnection?.ServerVersion;
        public override ConnectionState State => baseConnection.State;


        public SqlScriptConnection(ISqlScriptCollection scripts, DbConnection connection)
        {
            this.scriptCollection = scripts ?? throw new ArgumentNullException(nameof(scripts));
            this.baseConnection = connection ?? throw new ArgumentNullException(nameof(connection));
        }

        protected override void Dispose(bool disposing)
        {
            baseConnection?.Dispose();

            base.Dispose(disposing);
        }

        public override void Open()
        {
            baseConnection.Open();
        }

        public override async Task OpenAsync(CancellationToken cancellationToken)
        {
            if (SqlConnection != null)
                await SqlConnection.OpenAsync(cancellationToken);
            else
                baseConnection.Open();
        }

        public override void Close()
        {
            baseConnection.Close();
        }

        protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
        {
            if (SqlConnection != null)
                return SqlConnection.BeginTransaction(isolationLevel);
            else
                return baseConnection.BeginTransaction(isolationLevel) as DbTransaction;
        }

        protected override DbCommand CreateDbCommand()
        {
            if (SqlConnection != null)
                return SqlConnection.CreateCommand();
            else
                return baseConnection.CreateCommand() as DbCommand;
        }

        public override void ChangeDatabase(string databaseName)
        {
            baseConnection.ChangeDatabase(databaseName);
        }

        public string GetScriptSql(string key, object param = null)
        {
            return scriptCollection.GetScriptSql(key, param);
        }
    }
}
