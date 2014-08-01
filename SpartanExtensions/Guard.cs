using System;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using SpartanExtensions.Exceptions;
using System.Collections.Generic;

namespace SpartanExtensions
{
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
            var expression = String.Format("^(([0-9]{{{0},{1}}})|([0-9]{{{0},{1}}}[,.][0-9]{{{2},{3}}}))$",
                                        minDigitsBeforeComma, maxDigitsBeforeComma, minDigitsAfterComma, maxDigitsAfterComma);
            var regEx = new Regex(expression);
            if (!regEx.IsMatch(value.ToString(CultureInfo.InvariantCulture)))
                throw new Exception(String.Format("Double value doesn't meet the requirements. Expected value: {0} - minimum digits before comma, {1} - maximum digits before comma, {2} - minimum digits after comma, {3} - maximum digits after comma. Actual value: {4}. Variable- {5}. Method- {6}.",
                    minDigitsBeforeComma, maxDigitsBeforeComma, minDigitsAfterComma, maxDigitsAfterComma, value, variableName, methodName));
        }

        public static void AgainstEmptyEnumerable<T>(IEnumerable<T> value, string variableName, string methodName)
        {
            if (!value.Any())
                throw new Exception(string.Format("Enumerable cannot be empty. Variable- {0}. Method- {1}.",
                    variableName, methodName));
        }

        public static void AgainstNull(object value, string variableName, string methodName)
        {
            if (value == null)
                throw new Exception(string.Format("Object cannot be null. Variable- {0}. Method- {1}.",
                    variableName, methodName));
        }

        public static void AgainstZero(decimal value, string variableName, string methodName)
        {
            if (value == 0)
                throw new Exception(string.Format("Value cannot be zero. Variable- {0}. Method- {1}.",
                    variableName, methodName));
        }

        public static void AgainstNegativeOrZero(decimal value, string variableName, string methodName)
        {
            if (value <= 0)
                throw new Exception(string.Format("value cannot be negative or zero. Variable- {0}. Method- {1}.",
                    variableName, methodName));
        }

        public static void AgainstNegative(decimal value, string variableName, string methodName)
        {
            if (value < 0)
                throw new Exception(string.Format("Value cannot be negative. Variable- {0}. Method- {1}.",
                    variableName, methodName));
        }

        public static void AgainstStringIsNullOrEmpty(string value, string variableName, string methodName)
        {
            if (String.IsNullOrEmpty(value))
                throw new Exception(string.Format("String cannot be null or empty. Variable- {0}. Method- {1}.",
                    variableName, methodName));
        }

        public static void AgainstStringIsNOTNullOrEmpty(string value, string variableName, string methodName)
        {
            if (!String.IsNullOrEmpty(value))
                throw new Exception(string.Format("String must be null or empty. Variable- {0}. Method- {1}.",
                    variableName, methodName));
        }

        public static void AgainstStringIsNullOrWhiteSpace(string value, string variableName, string methodName)
        {
            if (String.IsNullOrWhiteSpace(value))
                throw new Exception(string.Format("String cannot be null, empty, or consist only of white-space characters. Variable- {0}, Method- {1}.",
                    variableName, methodName));
        }

        public static void AgainstEmptyGuid(Guid value, string variableName, string methodName)
        {
            if (value == Guid.Empty)
                throw new Exception(string.Format("Guid cannot be empty. Variable- {0}. Method- {1}.",
                    variableName, methodName));
        }

        public static void GuardAgainstNull<T, TProp>(this T obj, Expression<Func<T, TProp>> field)
        {
            var value = obj.GetPropertyValue(field);
            if (value == null)
                throw new UnexpectedObjectValueException<T>(field.GetFieldName(),
                    null, UnexpectedObjectValueExceptionReasons.NullReference);
        }

        public static void GuardAgainstZero<T, TProp>(this T obj, Expression<Func<T, TProp>> field)
        {
            var value = Convert.ToDecimal(obj.GetPropertyValue(field));
            if (value == 0)
                throw new UnexpectedObjectValueException<T>(field.GetFieldName(),
                    value, UnexpectedObjectValueExceptionReasons.ZeroValue);
        }

        public static void GuardAgainstNegative<T, TProp>(this T obj, Expression<Func<T, TProp>> field)
        {
            var value = Convert.ToDecimal(obj.GetPropertyValue(field));
            if (value < 0)
                throw new UnexpectedObjectValueException<T>(field.GetFieldName(),
                    value, UnexpectedObjectValueExceptionReasons.NegativeValue);
        }

        public static void GuardAgainstStringIsNullOrEmpty<T, TProp>(this T obj, Expression<Func<T, TProp>> field)
        {
            var value = (string)obj.GetPropertyValue(field);
            if (String.IsNullOrEmpty(value))
                throw new UnexpectedObjectValueException<T>(field.GetFieldName(),
                    value, UnexpectedObjectValueExceptionReasons.StringIsNullOrEmpty);
        }

        public static void GuardAgainstStringIsNOTNullOrEmpty<T, TProp>(this T obj, Expression<Func<T, TProp>> field)
        {
            var value = (string)obj.GetPropertyValue(field);
            if (!String.IsNullOrEmpty(value))
                throw new UnexpectedObjectValueException<T>(field.GetFieldName(),
                    value, UnexpectedObjectValueExceptionReasons.StringIsNOTNullOrEmpty);
        }

        public static void GuardAgainstStringIsNullOrWhiteSpace<T, TProp>(this T obj, Expression<Func<T, TProp>> field)
        {
            var value = (string)obj.GetPropertyValue(field);
            if(String.IsNullOrWhiteSpace(value))
                throw new UnexpectedObjectValueException<T>(field.GetFieldName(),
                    value, UnexpectedObjectValueExceptionReasons.StringIsNullOrWhiteSpace);
        }

        public static void GuardAgainstMinDate<T, TProp>(this T obj, Expression<Func<T, TProp>> field)
        {
            var value = DateTime.Parse(obj.GetPropertyValue(field).ToString());
            if (value <= new DateTime(1900, 1, 1))
                throw new UnexpectedObjectValueException<T>(field.GetFieldName(), value,
                    UnexpectedObjectValueExceptionReasons.DateValueMustBeGreaterThanMinDate);
        }

        public static void GuardAgainstNegativeAndZero<T, TProp>(this T obj, Expression<Func<T, TProp>> field)
        {
            var value = Convert.ToDecimal(obj.GetPropertyValue(field));
            if (value <= 0)
                throw new UnexpectedObjectValueException<T>(field.GetFieldName(),
                    value, UnexpectedObjectValueExceptionReasons.NegativeOrZeroValue);
        }

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

        public static void GuardAgainstEmptyEnumerable<T, TProp, TEnumerableItem>(this T obj, Expression<Func<T, TProp>> field)
        {
            var enumerable = (IEnumerable<TEnumerableItem>)obj.GetPropertyValue(field);
            if (!enumerable.Any())
                throw new UnexpectedObjectValueException<T>(field.GetFieldName(),
                    null, UnexpectedObjectValueExceptionReasons.EmptyEnumerable);
        }
    }
}
