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
        DateValueMustBeGreaterThanMinDate,
        EmptyGuid,
        EmptyEnumerable
    }
}
