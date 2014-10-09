using System;
using System.IO;
using System.Reflection;

namespace SpartanExtensions
{
    public static class TypeExtensions
    {
        /// <summary>
        /// Gets the type's assembly output file path.
        /// </summary>
        public static string GetAssemblyOutputPath(this Type type)
        {
            var assemblyLocation = Assembly.GetAssembly(type).Location;
            var fileName = Path.GetFileName(assemblyLocation);
            // ReSharper disable PossibleNullReferenceException
            return assemblyLocation.Replace(fileName, string.Empty);
            // ReSharper restore PossibleNullReferenceException
        }
    }
}
