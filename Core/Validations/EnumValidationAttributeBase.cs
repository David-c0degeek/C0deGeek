using System.ComponentModel.DataAnnotations;

namespace Core.Validations;

/// <summary>
/// Base class for enum-based validation attributes
/// </summary>
public abstract class EnumValidationAttributeBase : ValidationAttribute 
{
    protected readonly Enum TheEnum;
    protected readonly List<string> ValidValues;

    protected EnumValidationAttributeBase(Type enumType, Func<Enum, List<string>> valueSelector)
    {
        if (!enumType.IsEnum)
            throw new ArgumentException($"Type {enumType.Name} must be an enum", nameof(enumType));

        TheEnum = (Enum)Enum.ToObject(enumType, -1);
        ValidValues = valueSelector(TheEnum);
    }

    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null || string.IsNullOrEmpty(value.ToString()))
            return ValidationResult.Success!;

        var propertyValue = value.ToString()!;
        
        return !ValidValues.Contains(propertyValue)
            ? new ValidationResult(GetValidationMessage(propertyValue))
            : ValidationResult.Success!;
    }

    protected abstract string GetValidationMessage(string invalidValue);
}