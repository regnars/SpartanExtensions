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

        /// <summary>
        /// Gets an attribute corresponding to the type.
        /// </summary>
        /// <param name="type"></param>
        public static TAttribute GetAttribute<TAttribute>(this Type type)
            where TAttribute : Attribute
        {
            var attributeType = typeof(TAttribute);
            var attribute = (TAttribute)Attribute.GetCustomAttribute(type, attributeType);

            if(attribute == null)
                throw new Exception(string.Format("Can't find an attribute of a given the type \"{0}\" for the type \"{1}\".", attributeType.FullName, type.FullName));

            return attribute;
        }
    }
}
