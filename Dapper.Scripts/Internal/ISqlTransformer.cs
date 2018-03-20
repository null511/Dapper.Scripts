using System.Collections.Generic;

namespace Dapper.Scripts.Internal
{
    /// <summary>
    /// Describes an implementation that transforms
    /// an SQL string using dictionary values.
    /// </summary>
    public interface ISqlTransformer
    {
        /// <summary>
        /// Uses the provided <paramref name="valueCollection"/>
        /// to transform the <paramref name="sql"/> string.
        /// </summary>
        /// <typeparam name="T">Type of Value used by the dictionary.</typeparam>
        /// <param name="sql">The source SQL string.</param>
        /// <param name="valueCollection">Collection of named tag values.</param>
        /// <returns>Transformed SQL string.</returns>
        string Replace<T>(string sql, IDictionary<string, T> valueCollection);
    }
}
