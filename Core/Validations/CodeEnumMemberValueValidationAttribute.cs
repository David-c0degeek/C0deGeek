using Extensions;

namespace Core.Validations;

/// <summary>
/// Validates that a value matches an enum member value attribute value.
/// </summary>
public class CodeEnumMemberValueValidationAttribute : EnumValidationAttributeBase
{
    /// <summary>
    /// Initializes a new instance of the CodeEnumMemberValueValidationAttribute class.
    /// </summary>
    /// <param name="enumType">The enum type to validate against.</param>
    /// <exception cref="ArgumentException">Thrown when enumType is not an enum type.</exception>
    public CodeEnumMemberValueValidationAttribute(Type enumType) 
        : base(enumType, e => e.GetAllEnumMemberValues()) { }

    /// <summary>
    /// Gets a validation message for an invalid enum member value.
    /// </summary>
    /// <param name="invalidValue">The invalid member value.</param>
    /// <returns>A validation message describing the valid enum member values.</returns>
    protected override string GetValidationMessage(string invalidValue) 
        => TheEnum.GetValidationMessageForEnumMemberValue(invalidValue);
}