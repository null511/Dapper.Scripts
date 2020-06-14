using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.Scripts.Collection
{
    /// <summary>
    /// Loads SQL text from various locations, and
    /// appends it to the provided dictionary.
    /// </summary>
    public class SqlScriptLoader
    {
        private readonly IDictionary<string, string> scriptCollection;


        /// <summary>
        /// Creates a new instance of <see cref="SqlScriptLoader"/>
        /// using the provided dictionary.
        /// </summary>
        /// <param name="scriptCollection">The collection of SQL scripts to populate.</param>
        public SqlScriptLoader(IDictionary<string, string> scriptCollection)
        {
            this.scriptCollection = scriptCollection;
        }

        /// <summary>
        /// Loads all Embedded Resources found within the given assembly path.
        /// </summary>
        /// <param name="assembly">The assembly containing the embedded resources.</param>
        /// <param name="path">The full root-path of the resource items.</param>
        public void FromAssembly(Assembly assembly, string path)
        {
            foreach (var resource in FindResourceKeys(assembly, path))
            {
                var key = resource.Substring(path.Length).TrimStart('.');

                scriptCollection[key] = ReadResourceAsString(assembly, resource);
            }
        }

        /// <summary>
        /// Loads all Embedded Resources found within the provided path of the calling assembly.
        /// </summary>
        /// <param name="path">The full root-path of the resource items.</param>
        public void FromAssembly(string path)
        {
            FromAssembly(Assembly.GetCallingAssembly(), path);
        }

        /// <summary>
        /// Loads all Embedded Resources found within the given assembly path.
        /// </summary>
        /// <param name="assembly">The assembly containing the embedded resources.</param>
        /// <param name="path">The full root-path of the resource items.</param>
        public async Task FromAssemblyAsync(Assembly assembly, string path)
        {
            var taskList = FindResourceKeys(assembly, path).Select(resource => {
                var key = resource.Substring(path.Length).TrimStart('.');

                return Task.Run(async () => {
                    scriptCollection[key] = await ReadResourceAsStringAsync(assembly, resource);
                });
            }).ToArray();

            await Task.WhenAll(taskList);
        }

        /// <summary>
        /// Loads all Embedded Resources found within the provided path of the calling assembly.
        /// </summary>
        /// <param name="path">The full root-path of the resource items.</param>
        public Task FromAssemblyAsync(string path)
        {
            return FromAssemblyAsync(Assembly.GetCallingAssembly(), path);
        }

        /// <summary>
        /// Loads all Embedded Resources found within the given assembly path.
        /// </summary>
        /// <param name="assembly">The assembly containing the embedded resources.</param>
        /// <param name="path">The full root-path of the resource items.</param>
        /// <param name="encoding">The encoding used to read the files.</param>
        public void FromAssembly(Assembly assembly, string path, Encoding encoding)
        {
            foreach (var resource in FindResourceKeys(assembly, path)) {
                var key = resource.Substring(path.Length).TrimStart('.');

                scriptCollection[key] = ReadResourceAsString(assembly, resource, encoding);
            }
        }

        /// <summary>
        /// Loads all File System entries found within the given directory.
        /// </summary>
        public void FromDirectory(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));

            foreach (var file in Directory.GetFiles(path, "*.sql")) {
                var key = Path.GetFileName(file);
                scriptCollection[key] = File.ReadAllText(file);
            }
        }

        /// <summary>
        /// Loads the File System entry.
        /// </summary>
        public void FromFile(string filename)
        {
            if (string.IsNullOrEmpty(filename))
                throw new ArgumentNullException(nameof(filename));

            var key = Path.GetFileName(filename);
            scriptCollection[key] = File.ReadAllText(filename);
        }

        /// <summary>
        /// Adds the provided sql text to the collection.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="sql"></param>
        public void FromString(string key, string sql)
        {
            scriptCollection[key] = sql;
        }

        private IEnumerable<string> FindResourceKeys(Assembly assembly, string path)
        {
            if (assembly == null)
                throw new ArgumentNullException(nameof(assembly));

            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));

            return assembly
                .GetManifestResourceNames()
                .Where(x => x.StartsWith(path));
        }

        private string ReadResourceAsString(Assembly assembly, string resource)
        {
            using (var stream = assembly.GetManifestResourceStream(resource)) {
                if (stream == null) throw new ResourceNotFoundException(resource, assembly);
                    
                using (var reader = new StreamReader(stream)) {
                    return reader.ReadToEnd();
                }
            }
        }

        private string ReadResourceAsString(Assembly assembly, string resource, Encoding encoding)
        {
            using (var stream = assembly.GetManifestResourceStream(resource)) {
                if (stream == null) throw new ResourceNotFoundException(resource, assembly);
                    
                using (var reader = new StreamReader(stream, encoding)) {
                    return reader.ReadToEnd();
                }
            }
        }

        private Task<string> ReadResourceAsStringAsync(Assembly assembly, string resource)
        {
            using (var stream = assembly.GetManifestResourceStream(resource)) {
                if (stream == null) throw new ResourceNotFoundException(resource, assembly);
                    
                using (var reader = new StreamReader(stream)) {
                    return reader.ReadToEndAsync();
                }
            }
        }
    }
}
