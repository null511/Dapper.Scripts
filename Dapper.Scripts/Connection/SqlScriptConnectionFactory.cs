using Dapper.Scripts.Collection;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Dapper.Scripts.Connection
{
    public class SqlScriptConnectionFactory
    {
        private readonly ISqlScriptCollection scriptCollection;

        public string ConnectionString {get; set;}


        public SqlScriptConnectionFactory(ISqlScriptCollection scripts)
        {
            scriptCollection = scripts;
        }

        public SqlScriptConnection Connect()
        {
            return new SqlScriptConnection(scriptCollection, new SqlConnection()) {
                ConnectionString = ConnectionString,
            };
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
    }
}
