using System;
using System.Reflection;

namespace Dapper.Scripts.Collection
{
    /// <summary>
    /// Describes an Exception that occurs when an embedded assembly resource file is not found.
    /// </summary>
    public class ResourceNotFoundException : Exception
    {
        /// <summary>
        /// Creates a new empty instance of <see cref="ResourceNotFoundException"/>.
        /// </summary>
        public ResourceNotFoundException() {}

        /// <summary>
        /// Creates a new empty instance of <see cref="ResourceNotFoundException"/> using the provided message.
        /// </summary>
        public ResourceNotFoundException(string message) : base(message) {}

        /// <summary>
        /// Creates a new empty instance of <see cref="ResourceNotFoundException"/> using the default message and provided assembly name.
        /// </summary>
        public ResourceNotFoundException(string resourceName, string assemblyName) : base($"Resource \"{resourceName}\" was not found in assembly \"{assemblyName}\"!") {}

        /// <summary>
        /// Creates a new empty instance of <see cref="ResourceNotFoundException"/> using the default message and provided assembly.
        /// </summary>
        public ResourceNotFoundException(string resourceName, Assembly assembly) : base($"Resource \"{resourceName}\" was not found in assembly \"{assembly.FullName}\"!") {}
    }
}
