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

        internal IDbConnection ConnectionBase {get;}

        public override string ConnectionString {
            get => ConnectionBase.ConnectionString;
            set => ConnectionBase.ConnectionString = value;
        }

        public override int ConnectionTimeout => ConnectionBase.ConnectionTimeout;
        public override string Database => ConnectionBase.Database;
        public override string DataSource => (ConnectionBase as SqlConnection)?.DataSource;
        public override string ServerVersion => (ConnectionBase as SqlConnection)?.ServerVersion;
        public override ConnectionState State => ConnectionBase.State;


        public SqlScriptConnection(ISqlScriptCollection scripts, IDbConnection connection)
        {
            this.scriptCollection = scripts ?? throw new ArgumentNullException(nameof(scripts));
            this.ConnectionBase = connection ?? throw new ArgumentNullException(nameof(connection));
        }

        protected override void Dispose(bool disposing)
        {
            ConnectionBase?.Dispose();

            base.Dispose(disposing);
        }

        public override void Open()
        {
            ConnectionBase.Open();
        }

        public override async Task OpenAsync(CancellationToken cancellationToken)
        {
            if (ConnectionBase is SqlConnection sqlConnection)
                await sqlConnection.OpenAsync(cancellationToken);

            ConnectionBase.Open();
        }

        public override void Close()
        {
            ConnectionBase.Close();
        }

        protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
        {
            if (ConnectionBase is SqlConnection sqlConnection)
                return sqlConnection.BeginTransaction(isolationLevel);

            return ConnectionBase.BeginTransaction(isolationLevel) as DbTransaction;
        }

        protected override DbCommand CreateDbCommand()
        {
            if (ConnectionBase is SqlConnection sqlConnection)
                return sqlConnection.CreateCommand();

            return ConnectionBase.CreateCommand() as DbCommand;
        }

        public override void ChangeDatabase(string databaseName)
        {
            ConnectionBase.ChangeDatabase(databaseName);
        }

        public string GetScriptSql(string key, object param = null)
        {
            return scriptCollection.GetScriptSql(key, param);
        }
    }
}
