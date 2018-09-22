using Dapper.Scripts.Collection;
using Dapper.Scripts.Connection;
using System.Data;
using System.Threading;
using Xunit;

namespace Dapper.Scripts.Tests.UnitTests
{
    public class ScriptCommandDefinitionTests
    {
        [Fact]
        [Trait("Category", "Unit")]
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

                    Assert.Equal("test sql", commandDefinition.CommandText);
                    Assert.Equal(scriptCommandDefinition.Parameters, commandDefinition.Parameters);
                    Assert.Equal(scriptCommandDefinition.Transaction, commandDefinition.Transaction);
                    Assert.Equal(scriptCommandDefinition.CommandTimeout, commandDefinition.CommandTimeout);
                    Assert.Equal(scriptCommandDefinition.CommandType, commandDefinition.CommandType);
                    Assert.Equal(scriptCommandDefinition.Flags, commandDefinition.Flags);
                    Assert.Equal(scriptCommandDefinition.CancellationToken, commandDefinition.CancellationToken);
                }
            }
        }
    }
}
