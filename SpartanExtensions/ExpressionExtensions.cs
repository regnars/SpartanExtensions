using System;
using System.Linq.Expressions;

namespace SpartanExtensions
{
    public static class ExpressionExtensions
    {
        public static string GetFieldName<T, TProp>(this Expression<Func<T, TProp>> field)
        {
            return ((MemberExpression)field.Body).Member.Name;
        }
    }
}
