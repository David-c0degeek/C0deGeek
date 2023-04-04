using System.ComponentModel;
using System.Runtime.Serialization;

namespace Extensions;

public static class EnumExtensions
{
    internal static bool IsOneOf<T>(T enumeration, params T[] enums) where T : Enum
    {
        return enums.Contains(enumeration);
    }

    /// <summary>
    ///     Usage: set the [Description("XYZ")] attribute above an enum member
    /// </summary>
    /// <param name="input">A generic enum</param>
    /// <returns>Enum member description</returns>
    public static string GetDescription(this Enum input)
    {
        var genericEnumType = input.GetType();

        var memberInfo = genericEnumType.GetMember(input.ToString());
        if (memberInfo.Length <= 0) return input.ToString();

        var attribs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

        return attribs.Any()
            ? ((DescriptionAttribute)attribs.ElementAt(0)).Description
            : input.ToString();
    }

    /// <summary>
    ///     Usage: set the [Description("XYZ")] attribute above an enum member
    /// </summary>
    /// <param name="input">A generic enum</param>
    /// <returns>All Enum member descriptions</returns>
    public static List<string> GetAllDescriptions(this Enum input)
    {
        return (from Enum e in Enum.GetValues(input.GetType()) select e.GetDescription()).ToList();
    }

    /// <summary>
    ///     Usage: set the [EnumMember(Value = "XYZ")] attribute above an enum member
    /// </summary>
    /// <param name="inputEnum">A generic enum</param>
    /// <returns>Enum member description</returns>
    public static string? GetEnumMemberValue(this Enum inputEnum)
    {
        var genericEnumType = inputEnum.GetType();

        var memberInfo = genericEnumType.GetMember(inputEnum.ToString());
        if (memberInfo.Length <= 0) return inputEnum.ToString();

        var attribs = memberInfo[0].GetCustomAttributes(false).OfType<EnumMemberAttribute>().FirstOrDefault();

        return attribs == null
            ? inputEnum.ToString()
            : attribs.Value;
    }

    /// <summary>
    ///     Usage: set the [EnumMember(Value = "XYZ")] attribute above an enum member
    /// </summary>
    /// <param name="inputEnum">A generic enum</param>
    /// <returns>All Enum member descriptions</returns>
    public static List<string> GetAllEnumMemberValues(this Enum inputEnum)
    {
        var genericEnumType = inputEnum.GetType();
        return (from Enum e in Enum.GetValues(genericEnumType) select e.GetEnumMemberValue()).ToList();
    }

    /// <summary>
    ///     Generate an error message when comparing a string-value to all Names in an enum
    /// </summary>
    /// <param name="inputEnum">A generic enum</param>
    /// <param name="inputName">The name that is tested whether it's in the enum</param>
    /// <returns>Enum member description</returns>
    public static string GetValidationMessageForName(this Enum inputEnum, string inputName)
    {
        var allEnumNames = Enum.GetNames(inputEnum.GetType()).ToList();

        return allEnumNames.GetValidationMessageForStringValueNotInList(inputEnum.GetType().Name, inputName);
    }

    /// <summary>
    ///     Generate an error message when comparing a string-value to all
    ///     Descriptions (a [Description("XYZ")] attribute above an enum member) of an enum
    /// </summary>
    /// <param name="inputEnum">A generic enum</param>
    /// <param name="inputDescription">The description that is tested whether it's in the enum</param>
    /// <returns>Enum member description</returns>
    public static string GetValidationMessageForDescription(this Enum inputEnum, string inputDescription)
    {
        var allDescriptions = inputEnum.GetAllDescriptions();

        return allDescriptions.GetValidationMessageForStringValueNotInList(inputEnum.GetType().Name,
            inputDescription);
    }

    /// <summary>
    ///     Generate an error message when comparing a string-value to all
    ///     EnumMember-Values (an [EnumMember(Value = "XYZ")] attribute above an enum member) of an enum
    /// </summary>
    /// <param name="inputEnum">A generic enum</param>
    /// <param name="inputEnumMemberValue">The member value that is tested whether it's in the enum</param>
    /// <returns>Enum member description</returns>
    public static string GetValidationMessageForEnumMemberValue(this Enum inputEnum, string inputEnumMemberValue)
    {
        var allEnumMemberValues = inputEnum.GetAllEnumMemberValues();

        return allEnumMemberValues.GetValidationMessageForStringValueNotInList(inputEnum.GetType().Name,
            inputEnumMemberValue);
    }

    public static Enum ToEnum(this Type inputType)
    {
        if (!inputType.IsEnum)
            throw new Exception($"Type {nameof(inputType)} must be an enum");

        return (Enum)Enum.ToObject(inputType, -1);
    }
}