using System;
using System.Reflection;

namespace SpartanExtensions
{
    public static class MethodBaseExtensions
    {
        public static string GetCurrentMethodFullName<T>(this MethodBase method)
        {
            return (string.Format("{0}.{1}", typeof(T).FullName, method.Name));
        }

        public static string GetCurrentMethodFullName(this MethodBase method, Type type)
        {
            return (string.Format("{0}.{1}", type.FullName, method.Name));
        }
    }
}
