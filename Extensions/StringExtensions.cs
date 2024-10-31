namespace Extensions;

/// <summary>
/// Provides extension methods for string operations.
/// </summary>
public static class StringExtension
{
    /// <summary>
    /// Determines whether a string is null, empty, or consists only of white-space characters.
    /// This method combines the functionality of string.IsNullOrEmpty and string.IsNullOrWhiteSpace.
    /// </summary>
    /// <param name="value">The string to test.</param>
    /// <returns>true if the value is null, empty, or consists only of white-space characters; otherwise, false.</returns>
    public static bool IsNullOrEmptyOrWhiteSpace(this string value)
    {
        return string.IsNullOrEmpty(value) || value.Trim().Length == 0;
    }
}