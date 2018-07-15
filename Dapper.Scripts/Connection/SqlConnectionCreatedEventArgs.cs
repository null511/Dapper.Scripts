using System;
using System.Data;

namespace Dapper.Scripts.Connection
{
    /// <summary>
    /// Describes the creation of a connection.
    /// </summary>
    public class SqlConnectionCreatedEventArgs : EventArgs
    {
        /// <summary>
        /// The database connection created by <see cref="SqlScriptConnectionFactory"/>.
        /// </summary>
        public IDbConnection Connection {get; set;}
    }
}
