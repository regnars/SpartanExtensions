using System;
using System.Linq;
using System.Collections.Generic;
namespace SpartanExtensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Gets enum value by its attribute value.
        /// </summary>
        /// <typeparam name="TEnum">Type of the enum.</typeparam>
        /// <typeparam name="TAttribute">Type of the attribute.</typeparam>
        /// <param name="attributeValue">Attribute value by which to search.</param>
        /// <param name="compareAgainstAttributeProperty">Attribute property name by which to search.</param>
        /// <returns></returns>
        public static TEnum GetEnumValueByAttributeValue<TEnum, TAttribute>(this string attributeValue, string compareAgainstAttributeProperty)
            where TEnum : struct, IConvertible
        {
            var enumValue = default(TEnum);

            var enumValues = Enum.GetValues(typeof(TEnum)).Cast<TEnum>().ToList();

            enumValues.ForEach(ev =>
            {
                var memberInfo = typeof(TEnum).GetMember(ev.ToString()).FirstOrDefault();

                if (memberInfo == null)
                    return;

                var attribute = (TAttribute)memberInfo.GetCustomAttributes(typeof(TAttribute), inherit: false).FirstOrDefault();
                if (attribute == null)
                    return;

                var compareAgainstValue = (string)attribute.GetPropertyValue(compareAgainstAttributeProperty);

                if (compareAgainstValue == attributeValue)
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
