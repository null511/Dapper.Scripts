using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Dapper.Scripts.Collection
{
    public class SqlScriptLoader
    {
        private readonly IDictionary<string, string> scriptCollection;

        public SqlScriptLoader(IDictionary<string, string> scriptCollection)
        {
            this.scriptCollection = scriptCollection;
        }

        public void FromAssembly(Assembly assembly, string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));

            var sqlResources = assembly
                .GetManifestResourceNames()
                .Where(x => x.StartsWith(path));

            foreach (var resource in sqlResources) {
                var key = resource.Substring(path.Length);
                //key = Path.GetFileName(key);

                using (var stream = assembly.GetManifestResourceStream(resource)) {
                    if (stream == null)
                        throw new ApplicationException($"Resource '{resource}' was not found!");
                    
                    using (var reader = new StreamReader(stream)) {
                        scriptCollection[key] = reader.ReadToEnd();
                    }
                }
            }
        }

        public void FromFileSystem(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));

            foreach (var file in Directory.GetFiles(path, "*.sql")) {
                var key = Path.GetFileName(file);
                scriptCollection[key] = File.ReadAllText(file);
            }
        }
    }
}
