using System;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using SpartanExtensions.Exceptions;
using System.Collections.Generic;

namespace SpartanExtensions
{
    /// <summary>
    /// Guard dog sniffing if parameter or objects property has acceptable value. If not, exception is thrown.
    /// </summary>
    public static class Guard
    {
        /// <summary>
        /// Valid formats(for conditions: max digits before and after 2, min - 0):
        /// 1) dd,dd or dd.dd;
        /// 2) .dd or ,dd;
        /// 3) dd
        /// </summary>
        public static void DoubleFormat(double value, int minDigitsBeforeComma,
            int maxDigitsBeforeComma, int minDigitsAfterComma, int maxDigitsAfterComma,
            string variableName, string methodName)
        {
            var expression = string.Format("^(([0-9]{{{0},{1}}})|([0-9]{{{0},{1}}}[,.][0-9]{{{2},{3}}}))$",
                                        minDigitsBeforeComma, maxDigitsBeforeComma, minDigitsAfterComma, maxDigitsAfterComma);
            var regEx = new Regex(expression);
            if (!regEx.IsMatch(value.ToString(CultureInfo.InvariantCulture)))
                throw new Exception(
                    $"Double value doesn't meet the requirements. Expected value: {minDigitsBeforeComma} - minimum digits before comma, {maxDigitsBeforeComma} - maximum digits before comma, {minDigitsAfterComma} - minimum digits after comma, {maxDigitsAfterComma} - maximum digits after comma. Actual value: {value}. Variable- {variableName}. Method- {methodName}.");
        }

        /// <summary>
        /// Throws exception if enumerable is empty.
        /// </summary>
        public static void AgainstEmptyEnumerable<T>(IEnumerable<T> value, string variableName, string methodName)
        {
            if (!value.Any())
                throw new Exception($"Enumerable cannot be empty. Variable- {variableName}. Method- {methodName}.");
        }

        /// <summary>
        /// Throws exception object is null.
        /// </summary>
        public static void AgainstNull(object value, string variableName, string methodName)
        {
            if (value == null)
                throw new Exception($"Object cannot be null. Variable- {variableName}. Method- {methodName}.");
        }

        /// <summary>
        /// Throws exception if decimal value is zero.
        /// </summary>
        public static void AgainstZero(decimal value, string variableName, string methodName)
        {
            if (value == 0)
                throw new Exception($"Value cannot be zero. Variable- {variableName}. Method- {methodName}.");
        }

        /// <summary>
        /// Throws exception if decimal value is negative or zero.
        /// </summary>
        public static void AgainstNegativeOrZero(decimal value, string variableName, string methodName)
        {
            if (value <= 0)
                throw new Exception($"value cannot be negative or zero. Variable- {variableName}. Method- {methodName}.");
        }

        /// <summary>
        /// Throws exception if decimal value is negative.
        /// </summary>
        public static void AgainstNegative(decimal value, string variableName, string methodName)
        {
            if (value < 0)
                throw new Exception($"Value cannot be negative. Variable- {variableName}. Method- {methodName}.");
        }

        /// <summary>
        /// Throws exception if string is null or empty.
        /// </summary>
        public static void AgainstStringIsNullOrEmpty(string value, string variableName, string methodName)
        {
            if (String.IsNullOrEmpty(value))
                throw new Exception($"String cannot be null or empty. Variable- {variableName}. Method- {methodName}.");
        }

        /// <summary>
        /// Throws exception if string contains a value.
        /// </summary>
        public static void AgainstStringIsNOTNullOrEmpty(string value, string variableName, string methodName)
        {
            if (!String.IsNullOrEmpty(value))
                throw new Exception($"String must be null or empty. Variable- {variableName}. Method- {methodName}.");
        }

        /// <summary>
        /// Throws exception if string is null or whitespace.
        /// </summary>
        public static void AgainstStringIsNullOrWhiteSpace(string value, string variableName, string methodName)
        {
            if (String.IsNullOrWhiteSpace(value))
                throw new Exception($"String cannot be null, empty, or consist only of white-space characters. Variable- {variableName}, Method- {methodName}.");
        }

        /// <summary>
        /// Throws exception if guid is empty.
        /// </summary>
        public static void AgainstEmptyGuid(Guid value, string variableName, string methodName)
        {
            if (value == Guid.Empty)
                throw new Exception($"Guid cannot be empty. Variable- {variableName}. Method- {methodName}.");
        }

        /// <summary>
        /// Throws exception if property's value is null.
        /// </summary>
        public static void GuardAgainstNull<T, TProp>(this T obj, Expression<Func<T, TProp>> field)
        {
            var value = obj.GetPropertyValue(field);
            if (value == null)
                throw new UnexpectedObjectValueException<T>(field.GetFieldName(),
                    null, UnexpectedObjectValueExceptionReasons.NullReference);
        }

        /// <summary>
        /// Throws exception if property's value is zero.
        /// </summary>
        public static void GuardAgainstZero<T, TProp>(this T obj, Expression<Func<T, TProp>> field)
        {
            var value = Convert.ToDecimal(obj.GetPropertyValue(field));
            if (value == 0)
                throw new UnexpectedObjectValueException<T>(field.GetFieldName(),
                    value, UnexpectedObjectValueExceptionReasons.ZeroValue);
        }

        /// <summary>
        /// Throws exception if property's value is negative.
        /// </summary>
        public static void GuardAgainstNegative<T, TProp>(this T obj, Expression<Func<T, TProp>> field)
        {
            var value = Convert.ToDecimal(obj.GetPropertyValue(field));
            if (value < 0)
                throw new UnexpectedObjectValueException<T>(field.GetFieldName(),
                    value, UnexpectedObjectValueExceptionReasons.NegativeValue);
        }

        /// <summary>
        /// Throws exception if property's value is null or empty string.
        /// </summary>
        public static void GuardAgainstStringIsNullOrEmpty<T, TProp>(this T obj, Expression<Func<T, TProp>> field)
        {
            var value = (string)obj.GetPropertyValue(field);
            if (String.IsNullOrEmpty(value))
                throw new UnexpectedObjectValueException<T>(field.GetFieldName(),
                    value, UnexpectedObjectValueExceptionReasons.StringIsNullOrEmpty);
        }

        /// <summary>
        /// Throws exception if property's value is not an empty string.
        /// </summary>
        public static void GuardAgainstStringIsNOTNullOrEmpty<T, TProp>(this T obj, Expression<Func<T, TProp>> field)
        {
            var value = (string)obj.GetPropertyValue(field);
            if (!String.IsNullOrEmpty(value))
                throw new UnexpectedObjectValueException<T>(field.GetFieldName(),
                    value, UnexpectedObjectValueExceptionReasons.StringIsNOTNullOrEmpty);
        }

        /// <summary>
        /// Throws exception if property's value is null or whitespace string.
        /// </summary>
        public static void GuardAgainstStringIsNullOrWhiteSpace<T, TProp>(this T obj, Expression<Func<T, TProp>> field)
        {
            var value = (string)obj.GetPropertyValue(field);
            if(String.IsNullOrWhiteSpace(value))
                throw new UnexpectedObjectValueException<T>(field.GetFieldName(),
                    value, UnexpectedObjectValueExceptionReasons.StringIsNullOrWhiteSpace);
        }

        /// <summary>
        /// Throws exception if property's value is less than date 01.01.1900.
        /// </summary>
        public static void GuardAgainstMinDate<T, TProp>(this T obj, Expression<Func<T, TProp>> field)
        {
            var value = DateTime.Parse(obj.GetPropertyValue(field).ToString());
            if (value <= new DateTime(1900, 1, 1))
                throw new UnexpectedObjectValueException<T>(field.GetFieldName(), value,
                    UnexpectedObjectValueExceptionReasons.DateValueMustBeGreaterThanMinDate);
        }

        /// <summary>
        /// Throws exception if property's value is negative or zero.
        /// </summary>
        public static void GuardAgainstNegativeAndZero<T, TProp>(this T obj, Expression<Func<T, TProp>> field)
        {
            var value = Convert.ToDecimal(obj.GetPropertyValue(field));
            if (value <= 0)
                throw new UnexpectedObjectValueException<T>(field.GetFieldName(),
                    value, UnexpectedObjectValueExceptionReasons.NegativeOrZeroValue);
        }

        /// <summary>
        /// Throws exception if property's value is an empty guid.
        /// </summary>
        public static void GuardAgainstEmptyGuid<T, TProp>(this T obj, Expression<Func<T, TProp>> field)
        {
            var valueObj = obj.GetPropertyValue(field);
            if (valueObj == null)
                throw new UnexpectedObjectValueException<T>(field.GetFieldName(),
                    null, UnexpectedObjectValueExceptionReasons.NullReference);

            var value = valueObj.ToString();
            if (new Guid(value) == Guid.Empty)
                throw new UnexpectedObjectValueException<T>(field.GetFieldName(),
                    value, UnexpectedObjectValueExceptionReasons.EmptyGuid);
        }

        /// <summary>
        /// Throws exception if property's value is an empty enumerable.
        /// </summary>
        public static void GuardAgainstEmptyEnumerable<T, TProp, TEnumerableItem>(this T obj, Expression<Func<T, TProp>> field)
        {
            var enumerable = (IEnumerable<TEnumerableItem>)obj.GetPropertyValue(field);
            if (!enumerable.Any())
                throw new UnexpectedObjectValueException<T>(field.GetFieldName(),
                    null, UnexpectedObjectValueExceptionReasons.EmptyEnumerable);
        }
    }
}
