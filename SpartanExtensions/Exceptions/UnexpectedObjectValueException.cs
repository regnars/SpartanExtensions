using System;

namespace SpartanExtensions.Exceptions
{
    /// <summary>
    /// Unexpected object value exception class.
    /// </summary>
    /// <typeparam name="T">Type of the object to initialize.</typeparam>
    public class UnexpectedObjectValueException<T> : Exception
    {
        /// <summary>
        /// Failed object's to initialize type.
        /// </summary>
        public Type ObjectToInitializeType { get; protected set; }
        
        /// <summary>
        /// Failed property name.
        /// </summary>
        public string FailedPropertyName { get; protected set; }
        
        /// <summary>
        /// Reason of failing.
        /// </summary>
        public UnexpectedObjectValueExceptionReasons Reason { get; protected set; }

        private const string ErrorMessage = "Unexpected value in type \"{0}\" method. Property \"{1}\", value \"{2}\", reason \"{3}\".";

        internal UnexpectedObjectValueException(string failedPropertyName, object value, UnexpectedObjectValueExceptionReasons reason)
            : base(string.Format(ErrorMessage, typeof(T).Name, failedPropertyName, value, reason))
        {
            ObjectToInitializeType = typeof(T);
            FailedPropertyName = failedPropertyName;
            Reason = reason;
        }
    }
}
