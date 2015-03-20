﻿using System;
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
        /// Returns a string representing the current object without the worry of checking the object against null.
        /// </summary>
        public static string ToNullSafeString(this object obj)
        {
            var result = string.Empty;
            if (obj != null)
                result = obj.ToString();
            return result;
        }

        public static List<string> GetPublicInstanceProperties(this object obj)
        {
            var properties = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            return properties.Select(x => x.GetValue(obj, null).ToString()).ToList();
        }
    }
}
