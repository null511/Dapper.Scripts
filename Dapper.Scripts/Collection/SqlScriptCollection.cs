using Dapper.Scripts.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Dapper.Scripts.Collection
{
    /// <summary>
    /// Manages a collection of SQL scripts in-memory.
    /// Scripts can be added by scanning an assembly for embedded resources,
    /// or from a physical directory.
    /// </summary>
    public class SqlScriptCollection : ISqlScriptCollection
    {
        protected readonly Dictionary<string, string> scriptCollection;

        public SqlScriptLoader Add {get;}
        public MoustacheReplace Transform {get;}


        /// <summary>
        /// Creates a case-insensitive script dictionary.
        /// </summary>
        public SqlScriptCollection()
        {
            scriptCollection = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            Add = new SqlScriptLoader(scriptCollection);
            Transform = new MoustacheReplace();
        }

        /// <summary>
        /// Creates a script dictionary using the provided comparer.
        /// </summary>
        public SqlScriptCollection(StringComparer keyComparer)
        {
            scriptCollection = new Dictionary<string, string>(keyComparer);
        }

        public string GetScriptSql(string key, object param = null)
        {
            if (!scriptCollection.TryGetValue(key, out string sql))
                throw new ApplicationException($"SQL-Script '{key}' was not found!");

            return OnTransform(sql, param);
        }

        protected virtual string OnTransform(string sql, object param)
        {
            var args = param == null
                ? new Dictionary<string, object>()
                : ToDictionary(param, StringComparer.OrdinalIgnoreCase);

            return Transform.Replace(sql, args);
        }

        private static IDictionary<string, object> ToDictionary(object parameters, IEqualityComparer<string> comparer = null)
        {
            return parameters.GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Select(property => new KeyValuePair<string, object>(property.Name, property.GetValue(parameters)))
                .ToDictionary(x => x.Key, x => x.Value, comparer);
        }
    }
}
