using System;
using System.Reflection;

namespace Dapper.Scripts.Collection
{
    public class ResourceNotFoundException : Exception
    {
        public ResourceNotFoundException() {}

        public ResourceNotFoundException(string message) : base(message) {}

        public ResourceNotFoundException(string resourceName, string assemblyName) : base($"Resource \"{resourceName}\" was not found in assembly \"{assemblyName}\"!") {}

        public ResourceNotFoundException(string resourceName, Assembly assembly) : base($"Resource \"{resourceName}\" was not found in assembly \"{assembly.FullName}\"!") {}
    }
}
