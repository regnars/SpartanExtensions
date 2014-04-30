using System;
using System.Linq.Expressions;
using System.Reflection;

namespace SpartanExtensions
{
    public static class ObjectExtensions
    {
        public static object GetPropertyValue<T, TProp>(this T obj, Expression<Func<T, TProp>> property)
        {
            return (typeof(T)).GetProperty(property.GetFieldName(),
                BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
                .GetValue(obj, null);
        }
    }
}
