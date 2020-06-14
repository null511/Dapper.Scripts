using System.Reflection;

namespace Dapper.Scripts
{
    internal class UsageExample
    {
        public static SqlScriptConnectionFactory DatabaseName {get;}


        static UsageExample()
        {
            var scripts = new SqlScriptCollection();
            scripts.Add.FromAssembly("namespace.Sql.");

            DatabaseName = new SqlScriptConnectionFactory(scripts);
            DatabaseName.ConnectionString = "Server: xxx;";
        }

        public Task DeleteAsync(long id)
        {
            using var connection = DatabaseName.Connect();
            return connection.ExecuteScriptAsync("Example.Delete.sql", new {id});
        }
    }
}
