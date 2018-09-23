using Dapper.Scripts.Collection;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Dapper.Scripts.Tests.UnitTests
{
    public class ScriptCollectionTests
    {
        private const string SqlLiteral = "select * from [Test]";

        private readonly Assembly assembly;


        public ScriptCollectionTests()
        {
#if NETCOREAPP
            assembly = GetType().GetTypeInfo().Assembly;
#else
            assembly = Assembly.GetExecutingAssembly();
#endif
        }

        [Fact]
        [Trait("Category", "unit")]
        public void LoadsAllScriptsFromAssembly()
        {
            var collection = new SqlScriptCollection();
            collection.Add.FromAssembly(assembly, "Dapper.Scripts.Tests.Scripts");
            var sql = collection.GetScriptSql("SqlTest.sql");
            Assert.Equal(SqlLiteral, sql.Trim());
        }

        [Fact]
        [Trait("Category", "unit")]
        public async Task LoadsAllScriptsFromAssemblyAsync()
        {
            var collection = new SqlScriptCollection();
            await collection.Add.FromAssemblyAsync(assembly, "Dapper.Scripts.Tests.Scripts");
            var sql = collection.GetScriptSql("SqlTest.sql");
            Assert.Equal(SqlLiteral, sql.Trim());
        }

        [Fact]
        [Trait("Category", "unit")]
        public void LoadsAllScriptsFromAssemblyWithEncoding()
        {
            var collection = new SqlScriptCollection();
            collection.Add.FromAssembly(assembly, "Dapper.Scripts.Tests.Scripts", Encoding.UTF8);
            var sql = collection.GetScriptSql("SqlTest.sql");
            Assert.Equal(SqlLiteral, sql.Trim());
        }

        [Fact]
        [Trait("Category", "unit")]
        public void LoadsAllScriptsFromDirectory()
        {
            var collection = new SqlScriptCollection();
            collection.Add.FromDirectory(".\\Scripts");
            var sql = collection.GetScriptSql("SqlTest.sql");
            Assert.Equal(SqlLiteral, sql.Trim());
        }

        [Fact]
        [Trait("Category", "unit")]
        public void LoadsAllScriptsFromFile()
        {
            var collection = new SqlScriptCollection();
            collection.Add.FromFile(".\\Scripts\\SqlTest.sql");
            var sql = collection.GetScriptSql("SqlTest.sql");
            Assert.Equal(SqlLiteral, sql.Trim());
        }
    }
}
