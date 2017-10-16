using System;
using System.Linq.Expressions;
using System.Linq;

namespace SpartanExtensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Gets enum attribute value by enum value.
        /// </summary>
        /// <typeparam name="TEnum">Type of the enum.</typeparam>
        /// <typeparam name="TAttribute">Type of the attribute</typeparam>
        /// <typeparam name="TProp">Type of the attribute's property by which to compare.</typeparam>
        /// <param name="value">Attribute value by which to search the enum attribute values.</param>
        /// <param name="field">Attribute's property by which to compare.</param>
        /// <returns></returns>
        public static string ToAttributeSpecifiedValue<TEnum, TAttribute, TProp>(this TEnum value, Expression<Func<TAttribute, TProp>> field)
            where TEnum : struct, IConvertible
            where TAttribute : Attribute
        {
            var enumType = typeof(TEnum);
            if (!enumType.IsEnum)
                return null;

            var member = enumType.GetMember(value.ToString()).FirstOrDefault();
            if (member == null)
                return null;

            var attribute = (TAttribute)member.GetCustomAttributes(inherit: false).FirstOrDefault();

            return (string) attribute?.GetMemberValue(field);
        }

    }
}
