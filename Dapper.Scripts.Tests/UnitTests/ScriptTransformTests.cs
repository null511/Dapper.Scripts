using Dapper.Scripts.Collection;
using Xunit;

namespace Dapper.Scripts.Tests.UnitTests
{
    public class ScriptTransformTests
    {
        private const string SqlLiteral = "select * from [Test]";

        private readonly object _params;


        public ScriptTransformTests()
        {
            _params = new {
                TableName = "[Test]",
            };
        }

        [Fact]
        [Trait("Category", "unit")]
        public void ScriptReplacesTags()
        {
            var collection = new SqlScriptCollection();
            collection.Add.FromDirectory(".\\Scripts");
            var sql = collection.GetScriptSql("SqlTransformTest.sql", _params);
            Assert.Equal(SqlLiteral, sql.Trim());
        }
    }
}
