using Dapper.Scripts.Connection;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Dapper.Scripts.Tests
{
    [TestFixture, Category("integration")]
    public class ConnectionTests
    {
        [Test]
        public async Task CanConnect()
        {
            using (var connection = Database.Testing.Connect()) {
                await connection.OpenAsync();
            }
        }

        [Test]
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

                Assert.IsTrue(calledEvent, "ConnectionCreated Event was not fired!");
            }
            finally {
                Database.Testing.ConnectionCreated -= ConnectionCreatedEvent;
            }
        }
    }
}
