namespace SpartanExtensions
{
    public static class BooleanExtensions
    {
        public static string ToConditionalString(this bool value,
            string valueIfTrue, string valueIfFalse)
        {
            return value ? valueIfTrue : valueIfFalse;
        }
    }
}
