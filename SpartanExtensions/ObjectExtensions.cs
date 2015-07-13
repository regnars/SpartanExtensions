using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace SpartanExtensions
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// Gets property value using the reflection.
        /// </summary>
        public static object GetPropertyValue<T, TProp>(this T obj, Expression<Func<T, TProp>> property)
        {
            return (typeof(T)).GetProperty(property.GetFieldName(),
                BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
                .GetValue(obj, null);
        }

        /// <summary>
        /// Gets property value using the reflection.
        /// </summary>
        public static object GetPropertyValue<T>(this T obj, string propertyName)
        {
            return (typeof(T)).GetProperty(propertyName,
                BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
                .GetValue(obj, null);
        }

        /// <summary>
        /// Returns a string representing the current object without the worry of checking the object against null.
        /// </summary>
        public static string ToNullSafeString(this object obj)
        {
            var result = string.Empty;
            if (obj != null)
                result = obj.ToString();
            return result;
        }

        public static List<string> GetPublicStaticFields(this object obj)
        {
            var fields = obj.GetType().GetFields(BindingFlags.Public | BindingFlags.Static);
            return fields.Select(f => f.GetValue(obj).ToString()).ToList();
        }
    }
}
