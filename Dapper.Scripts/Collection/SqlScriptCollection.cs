using Dapper.Scripts.Internal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dapper.Scripts.Collection
{
    /// <summary>
    /// Manages a collection of SQL scripts in-memory.
    /// Scripts can be added by scanning an assembly for embedded resources,
    /// or from a physical directory.
    /// </summary>
    public class SqlScriptCollection : ISqlScriptCollection
    {
        /// <summary>
        /// Gets the collection of key-based scripts.
        /// </summary>
        protected Dictionary<string, string> ScriptCollection {get;}

        /// <summary>
        /// Gets an instance of <see cref="SqlScriptLoader"/>
        /// which adds scripts to this collection.
        /// </summary>
        public SqlScriptLoader Add {get;}

        /// <summary>
        /// Gets or sets the implementation used to transform SQL scripts.
        /// </summary>
        public ISqlTransformer Transform {get; set;}


        /// <summary>
        /// Creates a case-insensitive script dictionary.
        /// </summary>
        public SqlScriptCollection()
        {
            ScriptCollection = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            Add = new SqlScriptLoader(ScriptCollection);
            Transform = new SqlTransformUtility();
        }

        /// <summary>
        /// Creates a script dictionary using the provided comparer.
        /// </summary>
        public SqlScriptCollection(StringComparer keyComparer)
        {
            ScriptCollection = new Dictionary<string, string>(keyComparer);
        }

        /// <summary>
        /// Gets a script using the provided <paramref name="key"/>.
        /// </summary>
        /// <param name="key">The key referencing the SQL script.</param>
        /// <param name="param">An optional collection of parameters to be transformed into the SQL script.</param>
        /// <returns>A transformed SQL script from this collection.</returns>
        /// <exception cref="ScriptNotFoundException" />
        public string GetScriptSql(string key, object param = null)
        {
            if (!ScriptCollection.TryGetValue(key, out string sql))
                throw new ScriptNotFoundException(key);

            return OnTransform(sql, param);
        }

        /// <summary>
        /// Transforms the provided <paramref name="sql"/> string.
        /// </summary>
        /// <param name="sql">The SQL string.</param>
        /// <param name="param">An optional collection of parameters.</param>
        /// <returns>A transformed SQL string.</returns>
        protected virtual string OnTransform(string sql, object param = null)
        {
            if (param == null || Transform == null) return sql;

            if (!(param is IDictionary<string, object> args))
                args = ToDictionary(param, StringComparer.OrdinalIgnoreCase);

            return Transform.Replace(sql, args);
        }

        private static IDictionary<string, object> ToDictionary(object parameters, IEqualityComparer<string> comparer = null)
        {
            return parameters.GetType().GetProperties()
                .Select(property => new KeyValuePair<string, object>(property.Name, property.GetValue(parameters)))
                .ToDictionary(x => x.Key, x => x.Value, comparer);
        }
    }
}
