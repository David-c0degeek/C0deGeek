using System.ComponentModel.DataAnnotations;
using Extensions;

namespace Core.Validations;

/// <summary>
///     Validation for en enum type
/// </summary>
public class CodeEnumMemberValueValidationAttribute : ValidationAttribute
{
    private readonly List<string> _allEnumMemberValues;

    private readonly Enum _theEum;

    /// <summary>
    ///     Validation for en enum type
    /// </summary>
    /// <param name="enumType">The type of the enum</param>
    public CodeEnumMemberValueValidationAttribute(Type enumType)
    {
        if (!enumType.IsEnum)
            throw new Exception($"Type {nameof(enumType)} must be an enum");

        _theEum = (Enum)Enum.ToObject(enumType, -1);

        _allEnumMemberValues = _theEum.GetAllEnumMemberValues();
    }

    protected override ValidationResult IsValid(object? value,
        ValidationContext validationContext)
    {
        if (value is null)
            return ValidationResult.Success!;

        var propertyValue = value.ToString();

        if (string.IsNullOrEmpty(propertyValue))
            return ValidationResult.Success!;

        return !_allEnumMemberValues.Contains(propertyValue)
            ? new ValidationResult(_theEum.GetValidationMessageForEnumMemberValue(propertyValue))
            : ValidationResult.Success!;
    }
}