using Dapper.Scripts.Collection;
using Dapper.Scripts.Connection;
using System.Reflection;

namespace Dapper.Scripts.Tests
{
    internal static class Database
    {
        public static SqlScriptConnectionFactory Testing {get;}


        static Database()
        {
        #if NETCOREAPP1_0
            var assembly = typeof(Database).GetTypeInfo().Assembly;
        #else
            var assembly = Assembly.GetExecutingAssembly();
        #endif

            var scripts = new SqlScriptCollection();
            scripts.Add.FromAssembly(assembly, "Dapper.Scripts.Tests.Scripts");

            Testing = new SqlScriptConnectionFactory(scripts) {
                ConnectionString = "Server=.\\SQLExpress; Integrated Security=true; Initial Catalog=DapperScripts_Testing;",
            };
        }
    }
}
