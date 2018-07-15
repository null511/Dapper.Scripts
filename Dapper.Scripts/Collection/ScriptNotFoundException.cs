using System;

namespace Dapper.Scripts.Collection
{
    /// <summary>
    /// An exception describing a SQL script that was not found.
    /// </summary>
    public class ScriptNotFoundException : Exception
    {
        /// <summary>
        /// The key of the SQL script.
        /// </summary>
        public string ScriptKey {get;}

        /// <summary>
        /// Creates a new instance of <see cref="ScriptNotFoundException"/>.
        /// </summary>
        /// <param name="scriptKey">The key of the SQL script.</param>
        public ScriptNotFoundException(string scriptKey) : base($"SQL-Script '{scriptKey}' was not found!")
        {
            this.ScriptKey = scriptKey;
        }
    }
}
