using System.Threading.Tasks;
using Xunit;

namespace Dapper.Scripts.Tests.IntegrationTests
{
    public class ConnectionTests
    {
        [Fact]
        [Trait("Category", "Integration")]
        public async Task CanConnect()
        {
            using (var connection = Database.Testing.Connect()) {
                await connection.OpenAsync();
            }
        }
    }
}
