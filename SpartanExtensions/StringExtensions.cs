using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
namespace SpartanExtensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Gets enum value by its attribute value.
        /// </summary>
        /// <typeparam name="TEnum">Type of the enum.</typeparam>
        /// <typeparam name="TAttribute">Type of the attribute.</typeparam>
        /// <typeparam name="TProp">Type of the attribute's property by which to compare.</typeparam>
        /// <param name="attributeValue">Attribute value by which to search the enum attribute values.</param>
        /// <param name="field">Attribute's property by which to compare.</param>
        /// <returns></returns>
        public static TEnum? ToEnumBySpecifiedAttributeValue<TEnum, TAttribute, TProp>(this string attributeValue, Expression<Func<TAttribute, TProp>> field)
            where TEnum : struct, IConvertible
            where TAttribute : Attribute
        {
            TEnum? enumValue = null;

            var enumValues = Enum.GetValues(typeof(TEnum)).Cast<TEnum>().ToList();

            enumValues.ForEach(ev =>
            {
                var memberInfo = typeof(TEnum).GetMember(ev.ToString()).FirstOrDefault();

                if (memberInfo == null)
                    return;

                var attribute = (TAttribute)memberInfo.GetCustomAttributes(typeof(TAttribute), inherit: false).FirstOrDefault();
                if (attribute == null)
                    return;

                var compareAgainstValue = (string)attribute.GetPropertyValue(field.GetFieldName());

                if (!string.IsNullOrEmpty(compareAgainstValue)
                    && compareAgainstValue.ToLower() == attributeValue.ToLower())
                {
                    TEnum currentEnumVal;
                    Enum.TryParse(ev.ToString(), out currentEnumVal);
                    enumValue = currentEnumVal;
                    return;
                }
            });

            return enumValue;
        }
    }
}
