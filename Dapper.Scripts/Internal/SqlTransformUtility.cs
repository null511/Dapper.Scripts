using System;
using System.Collections.Generic;
using System.Text;

namespace Dapper.Scripts.Internal
{
    /// <summary>
    /// Transforms a string containing SQL by replacing
    /// custom named tags with dictionary values.
    /// </summary>
    public class SqlTransformUtility : ISqlTransformer
    {
        /// <summary>
        /// The transform behavior to use when a tag value
        /// is not found in the dictionary.
        /// </summary>
        public TagNotFoundBehavior NotFoundBehavior {get; set;}

        /// <summary>
        /// The string used to identify the start of a tag.
        /// </summary>
        public string TagStartChars {get; set;}

        /// <summary>
        /// The string used to identify the end of a tag.
        /// </summary>
        public string TagStopChars {get; set;}


        /// <summary>
        /// Creates a new instance of the <see cref="SqlTransformUtility"/>
        /// using the default values.
        /// </summary>
        public SqlTransformUtility()
        {
            NotFoundBehavior = TagNotFoundBehavior.Source;
            TagStartChars = "[<";
            TagStopChars = ">]";
        }

        /// <summary>
        /// Replaces all tags found in <paramref name="sql"/> using
        /// the provided <paramref name="valueCollection"/>.
        /// </summary>
        /// <typeparam name="T">Type of Value used by the dictionary.</typeparam>
        /// <param name="sql">The source SQL string.</param>
        /// <param name="valueCollection">Collection of named tag values.</param>
        /// <returns>Transformed SQL string.</returns>
        public string Replace<T>(string sql, IDictionary<string, T> valueCollection)
        {
            if (string.IsNullOrEmpty(sql)) return sql;
            if (valueCollection == null) throw new ArgumentNullException(nameof(valueCollection));

            var read_pos = 0;
            var result = new StringBuilder();
            while (read_pos < sql.Length) {
                var x = sql.IndexOf(TagStartChars, read_pos, StringComparison.Ordinal);
                if (x < 0) break;

                var y = sql.IndexOf(TagStopChars, x, StringComparison.Ordinal);
                if (y < 0) break;

                result.Append(sql, read_pos, x - read_pos);
                read_pos = y + TagStopChars.Length;

                var item_key = sql.Substring(x + TagStartChars.Length, y - x - TagStartChars.Length);

                if (valueCollection.TryGetValue(item_key, out T item_value)) {
                    result.Append(item_value);
                    continue;
                }

                switch (NotFoundBehavior) {
                    case TagNotFoundBehavior.Source:
                        result.Append(sql.Substring(x, y - x + TagStartChars.Length));
                        break;
                }
            }

            if (read_pos < sql.Length) {
                result.Append(sql, read_pos, sql.Length - read_pos);
            }

            return result.ToString();
        }
    }

    /// <summary>
    /// List of behavior types to perform when a tag is not found.
    /// </summary>
    public enum TagNotFoundBehavior
    {
        /// <summary>
        /// Print nothing.
        /// </summary>
        Empty,

        /// <summary>
        /// Print the source text/tag.
        /// </summary>
        Source,
    }
}
