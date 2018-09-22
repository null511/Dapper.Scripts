using Dapper.Scripts.Connection;
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

        [Fact]
        [Trait("Category", "Integration")]
        public async Task CanConnect_WithEvent()
        {
            var calledEvent = false;

            void ConnectionCreatedEvent(object sender, SqlConnectionCreatedEventArgs e) {
                calledEvent = true;
            }

            try {
                Database.Testing.ConnectionCreated += ConnectionCreatedEvent;

                using (var connection = Database.Testing.Connect()) {
                    await connection.OpenAsync();
                }

                Assert.True(calledEvent, "ConnectionCreated Event was not fired!");
            }
            finally {
                Database.Testing.ConnectionCreated -= ConnectionCreatedEvent;
            }
        }
    }
}
