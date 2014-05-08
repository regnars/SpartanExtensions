namespace SpartanExtensions.Exceptions
{
    public enum UnexpectedObjectValueExceptionReasons
    {
        NullReference,
        NegativeValue,
        ZeroValue,
        NegativeOrZeroValue,
        MustBePositive,
        StringIsNullOrEmpty,
        StringIsNullOrWhiteSpace,
        DateValueMustBeGreaterThanMinDate,
        EmptyGuid,
        EmptyEnumerable
    }
}
