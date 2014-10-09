using System;
using System.Linq.Expressions;

namespace SpartanExtensions
{
    public static class ExpressionExtensions
    {
        /// <summary>
        /// Gets the name of the property
        /// </summary>
        public static string GetFieldName<T, TProp>(this Expression<Func<T, TProp>> field)
        {
            return ((MemberExpression)field.Body).Member.Name;
        }
    }
}
