using Dapper.Scripts.Collection;
using Dapper.Scripts.Connection;
using NUnit.Framework;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace Dapper.Scripts.IntegrationTests
{
    [TestFixture]
    public class ConnectionTests
    {
        private SqlScriptConnectionFactory database;


        [OneTimeSetUp]
        public void CreateDatabase()
        {
            var scripts = new SqlScriptCollection();
            database = new SqlScriptConnectionFactory(scripts);
            database.ConnectionString = ConfigurationManager.ConnectionStrings["Testing"].ConnectionString;
        }

        [Test]
        public void CanConnect()
        {
            using (var connection = database.Connect()) {}
        }

        [Test]
        public void CanQuery()
        {
            using (var connection = database.Open()) {
                var fruitList = connection.Query("select [Name] from [dbo].[Fruit]").ToArray();
                var allFruit = fruitList.Select(x => (string)x.Name).ToArray();
                TestContext.Out.WriteLine(string.Join(", ", allFruit));
            }
        }

        [Test]
        public async Task CanQueryAsync()
        {
            using (var connection = await database.OpenAsync()) {
                var fruitList = (await connection.QueryAsync("select [Name] from [dbo].[Fruit]")).ToArray();
                var allFruit = fruitList.Select(x => (string)x.Name).ToArray();
                TestContext.Out.WriteLine(string.Join(", ", allFruit));
            }
        }
    }
}
