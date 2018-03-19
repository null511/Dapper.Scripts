using Dapper.Scripts.Collection;
using Dapper.Scripts.Connection;
using NUnit.Framework;
using System.Data;
using System.Threading;

namespace Dapper.Scripts.Tests.UnitTests
{
    [TestFixture, Category("unit")]
    public class ScriptCommandDefinitionTests
    {
        [Test]
        public void ToSqlDefinitionTest()
        {
            var scripts = new SqlScriptCollection();
            scripts.Add.FromString("key", "test sql");

            var connectionFactory = new SqlScriptConnectionFactory(scripts);

            using (var tokenSource = new CancellationTokenSource()) {
                var scriptCommandDefinition = new ScriptCommandDefinition {
                    Key = "key",
                    Parameters = new {
                        param = "test",
                    },
                    Transaction = null,
                    CommandTimeout = 123,
                    CommandType = CommandType.StoredProcedure,
                    Flags = CommandFlags.Pipelined,
                    CancellationToken = tokenSource.Token,
                };

                using (var connection = connectionFactory.Connect()) {
                    var commandDefinition = scriptCommandDefinition.ToSqlDefinition(connection);

                    Assert.That(commandDefinition.CommandText, Is.EqualTo("test sql"), "CommandText does not match!");
                    Assert.That(commandDefinition.Parameters, Is.EqualTo(scriptCommandDefinition.Parameters), "Parameters does not match!");
                    Assert.That(commandDefinition.Transaction, Is.EqualTo(scriptCommandDefinition.Transaction), "Transaction does not match!");
                    Assert.That(commandDefinition.CommandTimeout, Is.EqualTo(scriptCommandDefinition.CommandTimeout), "CommandTimeout does not match!");
                    Assert.That(commandDefinition.CommandType, Is.EqualTo(scriptCommandDefinition.CommandType), "CommandType does not match!");
                    Assert.That(commandDefinition.Flags, Is.EqualTo(scriptCommandDefinition.Flags), "Flags does not match!");
                    Assert.That(commandDefinition.CancellationToken, Is.EqualTo(scriptCommandDefinition.CancellationToken), "CancellationToken does not match!");
                }
            }
        }
    }
}
