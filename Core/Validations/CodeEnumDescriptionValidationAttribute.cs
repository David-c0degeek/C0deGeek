using System.ComponentModel.DataAnnotations;
using Extensions;

namespace Core.Validations;

///<summary>
///     Validation for en enum type
/// </summary>
public class CodeEnumDescriptionValidationAttribute : ValidationAttribute
{
    private readonly List<string> _allEnumDescriptions;

    private readonly Enum _theEum;

    /// <summary>
    ///     Validation for en enum type
    /// </summary>
    /// <param name="enumType">The type of the enum</param>
    public CodeEnumDescriptionValidationAttribute(Type enumType)
    {
        if (!enumType.IsEnum)
            throw new Exception($"Type {nameof(enumType)} must be an enum");

        _theEum = (Enum)Enum.ToObject(enumType, -1);

        _allEnumDescriptions = _theEum.GetAllDescriptions();
    }

    protected override ValidationResult IsValid(object? value,
        ValidationContext validationContext)
    {
        if (value is null)
            return ValidationResult.Success!;

        var propertyValue = value.ToString();

        if (string.IsNullOrEmpty(propertyValue))
            return ValidationResult.Success!;

        return !_allEnumDescriptions.Contains(propertyValue)
            ? new ValidationResult(_theEum.GetValidationMessageForDescription(propertyValue))
            : ValidationResult.Success!;
    }
}