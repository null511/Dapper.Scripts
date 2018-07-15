﻿using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace Dapper.Scripts.Tests
{
    [TestFixture, Category("integration")]
    public class SqlQueryTests
    {
        public TestContext TestContext {get; set;}


        [Test]
        public void CanSqlQuery()
        {
            using (var connection = Database.Testing.Open()) {
                var fruitList = connection.Query("select [Name] from [dbo].[Fruit]").ToArray();
                var allFruit = fruitList.Select(x => (string)x.Name).ToArray();
                TestContext.WriteLine(string.Join(", ", allFruit));

                if (!allFruit.Any())
                    Assert.Fail("No items were returned!");
            }
        }

        [Test]
        public async Task CanSqlQueryAsync()
        {
            using (var connection = await Database.Testing.OpenAsync()) {
                var fruitList = (await connection.QueryAsync("select [Name] from [dbo].[Fruit]")).ToArray();
                var allFruit = fruitList.Select(x => (string)x.Name).ToArray();
                TestContext.WriteLine(string.Join(", ", allFruit));

                if (!allFruit.Any())
                    Assert.Fail("No items were returned!");
            }
        }
    }
}