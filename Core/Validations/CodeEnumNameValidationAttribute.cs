using Extensions;

namespace Core.Validations;

/// <summary>
/// Validates that a value matches an enum name.
/// </summary>
public class CodeEnumNameValidationAttribute : EnumValidationAttributeBase
{
    /// <summary>
    /// Initializes a new instance of the CodeEnumNameValidationAttribute class.
    /// </summary>
    /// <param name="enumType">The enum type to validate against.</param>
    /// <exception cref="ArgumentException">Thrown when enumType is not an enum type.</exception>
    public CodeEnumNameValidationAttribute(Type enumType) 
        : base(enumType, e => Enum.GetNames(e.GetType()).ToList()) { }

    /// <summary>
    /// Gets a validation message for an invalid enum name.
    /// </summary>
    /// <param name="invalidValue">The invalid enum name.</param>
    /// <returns>A validation message describing the valid enum names.</returns>
    protected override string GetValidationMessage(string invalidValue) 
        => TheEnum.GetValidationMessageForName(invalidValue);
}