using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Dapper.Scripts.Tests.IntegrationTests
{
    public class SqlQueryTests
    {
        [Fact]
        [Trait("Category", "Integration")]
        public void CanSqlQuery()
        {
            using var connection = Database.Testing.Open();
            var fruitList = connection.Query("select [Name] from [dbo].[Fruit]").ToArray();
            var allFruit = fruitList.Select(x => (string)x.Name).ToArray();
            Console.Out.WriteLine(string.Join(", ", allFruit));

            Assert.NotEmpty(allFruit);
        }

        [Fact]
        [Trait("Category", "Integration")]
        public async Task CanSqlQueryAsync()
        {
            await using var connection = await Database.Testing.OpenAsync();
            var fruitList = (await connection.QueryAsync("select [Name] from [dbo].[Fruit]")).ToArray();
            var allFruit = fruitList.Select(x => (string)x.Name).ToArray();
            await Console.Out.WriteLineAsync(string.Join(", ", allFruit));

            Assert.NotEmpty(allFruit);
        }
    }
}
