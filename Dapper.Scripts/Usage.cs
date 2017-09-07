using System.Reflection;

namespace Dapper.Scripts
{
    internal static class Usage
    {
        public static SqlScriptConnectionFactory DatabaseName {get;}


        static Usage()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var scripts = new SqlScriptCollection();
            scripts.Add.FromAssembly(assembly, "namespace.Sql.");

            DatabaseName = new SqlScriptConnectionFactory(scripts);
            DatabaseName.ConnectionString = "Server: xxx;";
        }
    }
}
