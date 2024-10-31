using System.Globalization;

namespace Extensions;

/// <summary>
/// Provides extension methods for decimal values.
/// </summary>
public static class DecimalExtensions
{
    /// <summary>
    /// Converts the decimal to a string, removing trailing zeros and decimal point if necessary.
    /// </summary>
    /// <param name="value">The decimal value to convert.</param>
    /// <returns>A normalized string representation of the decimal.</returns>
    public static string ToNormalizedString(this decimal value)
    {
        var separator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
        var decimalAsString = value.ToString(CultureInfo.CurrentCulture);

        return decimalAsString.Contains(separator)
            ? decimalAsString.TrimEnd('0').TrimEnd(separator.ToCharArray())
            : decimalAsString;
    }

    /// <summary>
    /// Normalizes a decimal value by removing trailing zeros after the decimal point.
    /// </summary>
    /// <param name="value">The decimal value to normalize.</param>
    /// <returns>A normalized decimal value.</returns>
    public static decimal Normalize(this decimal value)
    {
        return value / 1.000000000000000000000000000000000m;
    }

    /// <summary>
    /// Converts the decimal to a percentage string representation.
    /// </summary>
    /// <param name="value">The decimal value to convert.</param>
    /// <returns>A string representation of the decimal as a percentage.</returns>
    public static string ToPercentageString(this decimal value)
    {
        return $"{value.Normalize()}%";
    }
}