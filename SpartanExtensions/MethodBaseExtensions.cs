using System;
using System.Reflection;

namespace SpartanExtensions
{
    public static class MethodBaseExtensions
    {
        /// <summary>
        /// Gets the current type method's full name. e.g. TypeName.MethodName
        /// </summary>
        public static string GetCurrentMethodFullName<T>(this MethodBase method)
        {
            return (string.Format("{0}.{1}", typeof(T).FullName, method.Name));
        }

        /// <summary>
        /// Gets the current type method's full name. e.g. TypeName.MethodName
        /// </summary>
        public static string GetCurrentMethodFullName(this MethodBase method, Type type)
        {
            return (string.Format("{0}.{1}", type.FullName, method.Name));
        }
    }
}
