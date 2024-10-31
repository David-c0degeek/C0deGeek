using Extensions;

namespace Core.Validations;

/// <summary>
/// Validates that a value matches an enum description attribute value.
/// </summary>
public class CodeEnumDescriptionValidationAttribute : EnumValidationAttributeBase
{
    /// <summary>
    /// Initializes a new instance of the CodeEnumDescriptionValidationAttribute class.
    /// </summary>
    /// <param name="enumType">The enum type to validate against.</param>
    /// <exception cref="ArgumentException">Thrown when enumType is not an enum type.</exception>
    public CodeEnumDescriptionValidationAttribute(Type enumType) 
        : base(enumType, e => e.GetAllDescriptions()) { }

    /// <summary>
    /// Gets a validation message for an invalid enum description value.
    /// </summary>
    /// <param name="invalidValue">The invalid description value.</param>
    /// <returns>A validation message describing the valid enum descriptions.</returns>
    protected override string GetValidationMessage(string invalidValue) 
        => TheEnum.GetValidationMessageForDescription(invalidValue);
}
