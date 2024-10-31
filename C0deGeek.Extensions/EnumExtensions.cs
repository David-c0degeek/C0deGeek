using System.ComponentModel;
using System.Runtime.Serialization;

namespace Extensions;

/// <summary>
/// Provides extension methods for enum types.
/// </summary>
public static class EnumExtensions
{
    /// <summary>
    /// Determines if an enumeration value is equal to any of the provided values.
    /// </summary>
    /// <typeparam name="T">The type of the enum.</typeparam>
    /// <param name="enumeration">The enum value to check.</param>
    /// <param name="enums">The enum values to compare against.</param>
    /// <returns>true if the enumeration matches any of the provided values; otherwise, false.</returns>
    internal static bool IsOneOf<T>(T enumeration, params T[] enums) where T : Enum
    {
        return enums.Contains(enumeration);
    }

    /// <summary>
    /// Gets the description of an enum value from its Description attribute.
    /// </summary>
    /// <param name="input">The enum value.</param>
    /// <returns>The description from the Description attribute, or the enum value's name if no description is found.</returns>
    /// <remarks>
    /// This method looks for the [Description("XYZ")] attribute on enum members.
    /// If no Description attribute is found, it returns the enum value's string representation.
    /// </remarks>
    public static string GetDescription(this Enum input)
    {
        var genericEnumType = input.GetType();
        var memberInfo = genericEnumType.GetMember(input.ToString());
        
        if (memberInfo.Length <= 0) 
            return input.ToString();

        var attribs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

        return attribs.Length != 0
            ? ((DescriptionAttribute)attribs.ElementAt(0)).Description
            : input.ToString();
    }

    /// <summary>
    /// Gets all descriptions from an enum type's values.
    /// </summary>
    /// <param name="input">Any value of the enum type.</param>
    /// <returns>A list of descriptions for all values in the enum.</returns>
    /// <remarks>
    /// This method collects the Description attribute values for all members of the enum.
    /// For members without a Description attribute, their names are used.
    /// </remarks>
    public static List<string> GetAllDescriptions(this Enum input)
    {
        return (from Enum e in Enum.GetValues(input.GetType()) select e.GetDescription()).ToList();
    }

    /// <summary>
    /// Gets the EnumMember value of an enum value.
    /// </summary>
    /// <param name="inputEnum">The enum value.</param>
    /// <returns>The value from the EnumMember attribute, or the enum value's name if no EnumMember is found.</returns>
    /// <remarks>
    /// This method looks for the [EnumMember(Value = "XYZ")] attribute on enum members.
    /// If no EnumMember attribute is found, it returns the enum value's string representation.
    /// </remarks>
    public static string? GetEnumMemberValue(this Enum inputEnum)
    {
        var genericEnumType = inputEnum.GetType();
        var memberInfo = genericEnumType.GetMember(inputEnum.ToString());
        
        if (memberInfo.Length <= 0) 
            return inputEnum.ToString();

        var attribs = memberInfo[0].GetCustomAttributes(false).OfType<EnumMemberAttribute>().FirstOrDefault();

        return attribs == null
            ? inputEnum.ToString()
            : attribs.Value;
    }

    /// <summary>
    /// Gets all EnumMember values from an enum type.
    /// </summary>
    /// <param name="inputEnum">Any value of the enum type.</param>
    /// <returns>A list of EnumMember values for all values in the enum.</returns>
    /// <remarks>
    /// This method collects the EnumMember attribute values for all members of the enum.
    /// For members without an EnumMember attribute, their names are used.
    /// </remarks>
    public static List<string> GetAllEnumMemberValues(this Enum inputEnum)
    {
        var genericEnumType = inputEnum.GetType();
        return (from Enum e in Enum.GetValues(genericEnumType) select e.GetEnumMemberValue()).ToList();
    }

    /// <summary>
    /// Creates a validation message for an invalid enum name.
    /// </summary>
    /// <param name="inputEnum">The enum type being validated.</param>
    /// <param name="inputName">The invalid name that was provided.</param>
    /// <returns>A validation message indicating valid enum names.</returns>
    public static string GetValidationMessageForName(this Enum inputEnum, string inputName)
    {
        var allEnumNames = Enum.GetNames(inputEnum.GetType()).ToList();
        return allEnumNames.GetValidationMessageForStringValueNotInList(inputEnum.GetType().Name, inputName);
    }

    /// <summary>
    /// Creates a validation message for an invalid enum description.
    /// </summary>
    /// <param name="inputEnum">The enum type being validated.</param>
    /// <param name="inputDescription">The invalid description that was provided.</param>
    /// <returns>A validation message indicating valid enum descriptions.</returns>
    public static string GetValidationMessageForDescription(this Enum inputEnum, string inputDescription)
    {
        var allDescriptions = inputEnum.GetAllDescriptions();
        return allDescriptions.GetValidationMessageForStringValueNotInList(inputEnum.GetType().Name,
            inputDescription);
    }

    /// <summary>
    /// Creates a validation message for an invalid enum member value.
    /// </summary>
    /// <param name="inputEnum">The enum type being validated.</param>
    /// <param name="inputEnumMemberValue">The invalid member value that was provided.</param>
    /// <returns>A validation message indicating valid enum member values.</returns>
    public static string GetValidationMessageForEnumMemberValue(this Enum inputEnum, string inputEnumMemberValue)
    {
        var allEnumMemberValues = inputEnum.GetAllEnumMemberValues();
        return allEnumMemberValues.GetValidationMessageForStringValueNotInList(inputEnum.GetType().Name,
            inputEnumMemberValue);
    }

    /// <summary>
    /// Converts a Type to an Enum instance.
    /// </summary>
    /// <param name="inputType">The type to convert.</param>
    /// <returns>An Enum instance of the specified type.</returns>
    /// <exception cref="Exception">Thrown when the input type is not an enum type.</exception>
    public static Enum ToEnum(this Type inputType)
    {
        if (!inputType.IsEnum)
            throw new Exception($"Type {nameof(inputType)} must be an enum");

        return (Enum)Enum.ToObject(inputType, -1);
    }
}