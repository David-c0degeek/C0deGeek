using Extensions;

namespace Core.Validations;

/// <summary>
/// Validates that a value matches an enum member value
/// </summary>
public class CodeEnumMemberValueValidationAttribute : EnumValidationAttributeBase
{
    public CodeEnumMemberValueValidationAttribute(Type enumType) 
        : base(enumType, e => e.GetAllEnumMemberValues()) { }

    protected override string GetValidationMessage(string invalidValue) 
        => TheEnum.GetValidationMessageForEnumMemberValue(invalidValue);
}