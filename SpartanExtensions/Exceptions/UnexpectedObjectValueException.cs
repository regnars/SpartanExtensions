using System;

namespace SpartanExtensions.Exceptions
{
    public class UnexpectedObjectValueException<T> : Exception
    {
        public Type ObjectToInitializeType { get; protected set; }
        public string FailedPropertyName { get; protected set; }
        public UnexpectedObjectValueExceptionReasons Reason { get; protected set; }

        private const string ErrorMessage = "Unexpected value in type \"{0}\" method. Property \"{1}\", value \"{2}\", reason \"{3}\".";

        public UnexpectedObjectValueException(string failedPropertyName, object value, UnexpectedObjectValueExceptionReasons reason)
            : base(string.Format(ErrorMessage, typeof(T).Name, failedPropertyName, value, reason))
        {
            ObjectToInitializeType = typeof(T);
            FailedPropertyName = failedPropertyName;
            Reason = reason;
        }
    }
}
