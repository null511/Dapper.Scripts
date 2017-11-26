namespace Dapper.Scripts.Collection
{
    /// <summary>
    /// Describes a readonly collection of SQL scripts.
    /// </summary>
    public interface ISqlScriptCollection
    {
        /// <summary>
        /// Get the SQL script identified by '<paramref name="key"/>'.
        /// </summary>
        /// <param name="key">The script key.</param>
        /// <param name="param">Optional collection of values for transforming script.</param>
        string GetScriptSql(string key, object param = null);
    }
}
