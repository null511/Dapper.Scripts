using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Dapper.Scripts
{
    /// <summary>
    /// Manages a collection of SQL scripts in-memory.
    /// Scripts can be added by scanning an assembly for embedded resources,
    /// or from a physical directory.
    /// </summary>
    public class SqlScriptCollection : ISqlScriptCollection
    {
        private readonly Dictionary<string, string> scripts;

        public ScriptLoader Add {get;}


        /// <summary>
        /// Creates a case-insensitive script dictionary.
        /// </summary>
        public SqlScriptCollection()
        {
            scripts = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            Add = new ScriptLoader(scripts);
        }

        /// <summary>
        /// Creates a script dictionary using the provided comparer.
        /// </summary>
        public SqlScriptCollection(StringComparer keyComparer)
        {
            scripts = new Dictionary<string, string>(keyComparer);
        }

        public void LoadEmbeddedFiles(Assembly assembly, string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));

            var sqlResources = assembly
                .GetManifestResourceNames()
                .Where(x => x.StartsWith(path));

            foreach (var resource in sqlResources) {
                var key = resource.Substring(path.Length);
                key = Path.GetFileNameWithoutExtension(key);

                using (var stream = assembly.GetManifestResourceStream(resource)) {
                    if (stream == null)
                        throw new ApplicationException($"Resource '{resource}' was not found!");
                    
                    using (var reader = new StreamReader(stream)) {
                        scripts[key] = reader.ReadToEnd();
                    }
                }
            }
        }

        public void LoadExternalFiles(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));

            foreach (var file in Directory.GetFiles(path, "*.sql")) {
                var key = Path.GetFileNameWithoutExtension(file);
                scripts[key] = File.ReadAllText(file);
            }
        }

        public string GetScriptSql(string key, object param = null)
        {
            if (!scripts.TryGetValue(key, out string sql))
                throw new ApplicationException($"SQL-Script '{key}' was not found!");

            var args = param == null
                ? new Dictionary<string, object>()
                : ToDictionary(param, StringComparer.OrdinalIgnoreCase);

            sql = sql.MoustacheReplace(args, MoustacheNotFoundBehavior.Empty);

            return sql;
        }

        public static IDictionary<string, object> ToDictionary(object parameters, IEqualityComparer<string> comparer = null)
        {
            return parameters.GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Select(property => new KeyValuePair<string, object>(property.Name, property.GetValue(parameters)))
                .ToDictionary(x => x.Key, x => x.Value, comparer);
        }
    }
}
