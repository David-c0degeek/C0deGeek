using System.Globalization;

namespace Extensions;

public static class DecimalExtensions
{
    public static string ToNormalizedString(this decimal value)
    {
        var separator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
        var decimalAsString = value.ToString(CultureInfo.CurrentCulture);

        return decimalAsString.Contains(separator)
            ? decimalAsString.TrimEnd('0').TrimEnd(separator.ToCharArray())
            : decimalAsString;
    }

    public static decimal Normalize(this decimal value)
    {
        return value / 1.000000000000000000000000000000000m;
    }

    public static string ToPercentageString(this decimal value)
    {
        return $"{value.Normalize()}%";
    }
}