using System;
using System.Collections.Generic;
using System.Text;

namespace Dapper.Scripts.Text
{
    /// <summary>
    /// Builds a SQL statement by appending text and auto-indexed parameters.
    /// Designed for batching SQL statements.
    /// </summary>
    public class SqlBuilder
    {
        private readonly StringBuilder builderText;
        private readonly Dictionary<string, object> paramList;

        /// <summary>
        /// Gets the SQL text that has been appended to the builder.
        /// </summary>
        public string Text => builderText.ToString();

        /// <summary>
        /// Gets a collection of SQL parameters that have been added to the builder.
        /// </summary>
        public IReadOnlyDictionary<string, object> Parameters => paramList;


        /// <summary>
        /// Creates a new and empty SQL builder instance.
        /// </summary>
        public SqlBuilder()
        {
            builderText = new StringBuilder();
            paramList = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Appends SQL text to the builder instance.
        /// </summary>
        /// <param name="text">The SQL text to be appended to the builder.</param>
        public void Append(string text)
        {
            builderText.Append(text);
        }

        /// <summary>
        /// Appends SQL text to the builder instance,
        /// followed by the default line terminator.
        /// </summary>
        /// <param name="text">The SQL text to be appended to the builder.</param>
        public void AppendLine(string text = null)
        {
            builderText.AppendLine(text);
        }

        /// <summary>
        /// Generates and appends a uniquely named SQL parameter to the collection.
        /// </summary>
        /// <param name="value">The value of the parameter.</param>
        /// <returns>The unique parameter name. ex: @__pN</returns>
        public string AppendIndexedParam(object value)
        {
            var index = paramList.Count;
            var paramName = $"@__p{index}_";
            paramList[paramName] = value;
            return paramName;
        }

        /// <summary>
        /// Appends a named SQL parameter to the collection.
        /// </summary>
        /// <param name="name">The name of the SQL parameter.</param>
        /// <param name="value">The value of the SQL parameter</param>
        public void AppendParam(string name, object value)
        {
            if (paramList.ContainsKey(name)) throw new Exception($"The named parameter '{name}' already exists!");

            paramList[name] = value;
        }

        /// <summary>
        /// Gets a string containing the SQL text of this instance,
        /// with all parameters injected as text. Useful for debugging.
        /// </summary>
        public string ToDebugString()
        {
            var sql = builderText.ToString();
            return SqlDebugPrinter.GetDebugString(sql, paramList);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return builderText.ToString();
        }
    }
}