using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Dapper.Scripts.Tests.IntegrationTests
{
    public class ScriptQueryTests
    {
        [Fact]
        [Trait("Category", "Integration")]
        public void CanScriptQuery()
        {
            using var connection = Database.Testing.Open();
            var fruitList = connection.QueryScript("SelectAllFruitNames.sql").ToArray();
            var allFruit = fruitList.Select(x => (string)x.Name).ToArray();
            Console.Out.WriteLine(string.Join(", ", allFruit));

            Assert.NotEmpty(allFruit);
        }

        [Fact]
        [Trait("Category", "Integration")]
        public async Task CanScriptQueryAsync()
        {
            await using var connection = await Database.Testing.OpenAsync();
            var fruitList = (await connection.QueryScriptAsync("SelectAllFruitNames.sql")).ToArray();
            var allFruit = fruitList.Select(x => (string)x.Name).ToArray();
            await Console.Out.WriteLineAsync(string.Join(", ", allFruit));

            Assert.NotEmpty(allFruit);
        }
    }
}
