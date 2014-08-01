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
        StringIsNOTNullOrEmpty,
        StringIsNullOrWhiteSpace,
        DateValueMustBeGreaterThanMinDate,
        EmptyGuid,
        EmptyEnumerable
    }
}
