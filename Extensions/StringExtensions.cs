namespace Extensions
{
    public static class StringExtension
    {
        public static bool IsNullOrEmptyOrWhiteSpace(this string value)
        {
            return string.IsNullOrEmpty(value) || value.Trim().Length == 0;
        }
    }
}