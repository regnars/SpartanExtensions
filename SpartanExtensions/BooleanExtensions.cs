namespace SpartanExtensions
{
    public static class BooleanExtensions
    {
        /// <summary>
        /// Converts boolean to string. For example if used like this yourbool.ToConditionalString("Yes", "No") it will return strings "Yes" or "No" depending on the boolean value.
        /// </summary>
        public static string ToConditionalString(this bool value,
            string valueIfTrue, string valueIfFalse)
        {
            return value ? valueIfTrue : valueIfFalse;
        }
    }
}
