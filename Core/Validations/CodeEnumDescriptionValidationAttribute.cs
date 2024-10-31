using Extensions;

namespace Core.Validations;

/// <summary>
/// Validates that a value matches an enum description
/// </summary>
public class CodeEnumDescriptionValidationAttribute : EnumValidationAttributeBase
{
    public CodeEnumDescriptionValidationAttribute(Type enumType) 
        : base(enumType, e => e.GetAllDescriptions()) { }

    protected override string GetValidationMessage(string invalidValue) 
        => TheEnum.GetValidationMessageForDescription(invalidValue);
}