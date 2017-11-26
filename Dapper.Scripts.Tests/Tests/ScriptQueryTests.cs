using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace Dapper.Scripts.Tests
{
    [TestClass]
    public class ScriptQueryTests
    {
        public TestContext TestContext {get; set;}


        [TestMethod]
        public void CanScriptQuery()
        {
            using (var connection = Database.Testing.Open()) {
                var fruitList = connection.QueryScript("SelectAllFruitNames.sql").ToArray();
                var allFruit = fruitList.Select(x => (string)x.Name).ToArray();
                TestContext.WriteLine(string.Join(", ", allFruit));

                if (!allFruit.Any())
                    Assert.Fail("No items were returned!");
            }
        }

        [TestMethod]
        public async Task CanScriptQueryAsync()
        {
            using (var connection = await Database.Testing.OpenAsync()) {
                var fruitList = (await connection.QueryScriptAsync("SelectAllFruitNames.sql")).ToArray();
                var allFruit = fruitList.Select(x => (string)x.Name).ToArray();
                TestContext.WriteLine(string.Join(", ", allFruit));

                if (!allFruit.Any())
                    Assert.Fail("No items were returned!");
            }
        }
    }
}
