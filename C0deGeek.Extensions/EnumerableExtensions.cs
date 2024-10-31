namespace Extensions;

/// <summary>
/// Provides extension methods for IEnumerable types.
/// </summary>
public static class EnumerableExtensions
{
    /// <summary>
    /// Creates a validation message for a string value that is not present in a list of valid values.
    /// </summary>
    /// <param name="stringList">The list of valid string values.</param>
    /// <param name="stringName">The name of the string property being validated.</param>
    /// <param name="stringValue">The invalid value that was provided.</param>
    /// <returns>A formatted validation message indicating the invalid value and the list of valid values.</returns>
    public static string GetValidationMessageForStringValueNotInList(this IEnumerable<string> stringList,
        string stringName, string stringValue)
    {
        return $"{stringName} ({stringValue}) must be one of {string.Join("|", stringList)}";
    }
}
