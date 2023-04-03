namespace Extensions;

public static class EnumerableExtensions
{
    public static string GetValidationMessageForStringValueNotInList(this IEnumerable<string> stringList,
        string stringName, string stringValue)
    {
        return $"{stringName} ({stringValue}) must be one of {string.Join("|", stringList)}";
    }
}