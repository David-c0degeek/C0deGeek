using Extensions;

namespace Core.Validations;

/// <summary>
/// Validates that a value matches an enum name
/// </summary>
public class CodeEnumNameValidationAttribute : EnumValidationAttributeBase
{
    public CodeEnumNameValidationAttribute(Type enumType) 
        : base(enumType, e => Enum.GetNames(e.GetType()).ToList()) { }

    protected override string GetValidationMessage(string invalidValue) 
        => TheEnum.GetValidationMessageForName(invalidValue);
}