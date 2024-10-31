using System.ComponentModel.DataAnnotations;

namespace Core.Validations;

/// <summary>
/// Base class for enum-based validation attributes that provides common validation functionality.
/// </summary>
public abstract class EnumValidationAttributeBase : ValidationAttribute 
{
    /// <summary>
    /// Gets the enum instance used for validation.
    /// </summary>
    protected readonly Enum TheEnum;

    /// <summary>
    /// Gets the list of valid values for the enum.
    /// </summary>
    protected readonly List<string> ValidValues;

    /// <summary>
    /// Initializes a new instance of the EnumValidationAttributeBase class.
    /// </summary>
    /// <param name="enumType">The type of enum to validate against.</param>
    /// <param name="valueSelector">A function that extracts valid values from the enum.</param>
    /// <exception cref="ArgumentException">Thrown when enumType is not an enum type.</exception>
    protected EnumValidationAttributeBase(Type enumType, Func<Enum, List<string>> valueSelector)
    {
        if (!enumType.IsEnum)
            throw new ArgumentException($"Type {enumType.Name} must be an enum", nameof(enumType));

        TheEnum = (Enum)Enum.ToObject(enumType, -1);
        ValidValues = valueSelector(TheEnum);
    }

    /// <summary>
    /// Determines whether the specified value is valid according to the validation rules.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="validationContext">The context information about the validation operation.</param>
    /// <returns>A ValidationResult indicating whether validation was successful.</returns>
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null || string.IsNullOrEmpty(value.ToString()))
            return ValidationResult.Success!;

        var propertyValue = value.ToString()!;
        
        return !ValidValues.Contains(propertyValue)
            ? new ValidationResult(GetValidationMessage(propertyValue))
            : ValidationResult.Success!;
    }

    /// <summary>
    /// Gets a validation message for an invalid value.
    /// </summary>
    /// <param name="invalidValue">The invalid value that failed validation.</param>
    /// <returns>A validation message describing why the value is invalid.</returns>
    protected abstract string GetValidationMessage(string invalidValue);
}