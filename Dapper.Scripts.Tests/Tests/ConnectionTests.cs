using Dapper.Scripts.Connection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Dapper.Scripts.Tests
{
    [TestClass]
    public class ConnectionTests
    {
        [TestMethod]
        public async Task CanConnect()
        {
            using (var connection = Database.Testing.Connect()) {
                await connection.OpenAsync();
            }
        }

        [TestMethod]
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
