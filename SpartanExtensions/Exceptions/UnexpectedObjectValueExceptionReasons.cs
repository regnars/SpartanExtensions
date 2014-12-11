namespace SpartanExtensions.Exceptions
{
    public enum UnexpectedObjectValueExceptionReasons
    {
        /// <summary>
        /// Value is null
        /// </summary>
        NullReference,
        
        /// <summary>
        /// Value is negative
        /// </summary>
        NegativeValue,
        
        /// <summary>
        /// Value is zero
        /// </summary>
        ZeroValue,
        
        /// <summary>
        /// Value is negative or zero
        /// </summary>
        NegativeOrZeroValue,
        
        /// <summary>
        /// Value must be positive
        /// </summary>
        MustBePositive,
        
        /// <summary>
        /// String value is null or empty
        /// </summary>
        StringIsNullOrEmpty,
        
        // ReSharper disable once InconsistentNaming
        /// <summary>
        /// String value is NOT null or empty
        /// </summary>
        StringIsNOTNullOrEmpty,
        
        /// <summary>
        /// String is null or whitespace
        /// </summary>
        StringIsNullOrWhiteSpace,
        
        /// <summary>
        /// DateValue is smaller than minimal date specified by DateTime.Min
        /// </summary>
        DateValueMustBeGreaterThanMinDate,
        
        /// <summary>
        /// Guid value is empty
        /// </summary>
        EmptyGuid,
        
        /// <summary>
        /// Enumerable is empty
        /// </summary>
        EmptyEnumerable
    }
}
