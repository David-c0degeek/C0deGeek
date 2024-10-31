using System.Reflection;
using Core.Models;
using Extensions;

namespace Core;

// TypeSafeEnumBase.cs
/// <summary>
/// Provides a base implementation for type-safe enums, offering strongly-typed enumeration values with additional metadata.
/// </summary>
/// <typeparam name="T">The specific type-safe enum type that derives from this base.</typeparam>
public abstract class TypeSafeEnumBase<T> : CodeNameModel, IComparable where T : CodeNameModel
{
    /// <summary>
    /// Initializes a new instance of the type-safe enum.
    /// </summary>
    /// <param name="code">The unique code representing this enum value.</param>
    /// <param name="name">The human-readable name for this enum value.</param>
    protected TypeSafeEnumBase(string code, string name) : base(code, name) { }

    /// <summary>
    /// Compares this instance with another type-safe enum value.
    /// </summary>
    /// <param name="other">The object to compare with this instance.</param>
    /// <returns>A value indicating the relative ordering of the objects being compared.</returns>
    public int CompareTo(object? other)
    {
        return string.Compare(Code, ((TypeSafeEnumBase<T>?)other)?.Code, StringComparison.InvariantCultureIgnoreCase);
    }

    /// <summary>
    /// Returns the name of this enum value.
    /// </summary>
    /// <returns>The name of this enum value.</returns>
    public override string ToString() => Name;

    /// <summary>
    /// Retrieves all defined values for this type-safe enum type.
    /// </summary>
    /// <returns>An enumerable collection of all defined enum values.</returns>
    public static IEnumerable<T> GetAll()
    {
        var type = typeof(T);
        var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);

        return fields
            .Select(info => info.GetValue(null))
            .OfType<T>()
            .ToList();
    }

    /// <summary>
    /// Retrieves a specific enum value by its code.
    /// </summary>
    /// <param name="code">The code to search for. Can be a comma-separated list of codes.</param>
    /// <returns>The matching enum value, or null if not found.</returns>
    public static T? GetByCode(string code)
    {
        var type = typeof(T);
        var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);

        return fields
            .Select(info => info.GetValue(null))
            .OfType<T>()
            .FirstOrDefault(x => x.Code.Split(",").Contains(code));
    }

    /// <summary>
    /// Generates a validation message for an unsupported code value.
    /// </summary>
    /// <param name="codeValue">The invalid code value.</param>
    /// <returns>A formatted validation message.</returns>
    public static string GetValidationMessageForUnsupportedCode(string codeValue)
    {
        var type = typeof(T);
        var typeName = $"{type.Name}.Code";
        return GetAll()
            .Select(x => x.Code)
            .ToList()
            .GetValidationMessageForStringValueNotInList(typeName, codeValue);
    }

    /// <summary>
    /// Determines whether the specified object is equal to the current object.
    /// </summary>
    /// <param name="obj">The object to compare with the current object.</param>
    /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
    public override bool Equals(object? obj)
    {
        if (obj is not TypeSafeEnumBase<T> otherValue) 
            return false;

        var typeMatches = GetType() == obj.GetType();
        var valueMatches = Code.Equals(otherValue.Code);

        return typeMatches && valueMatches;
    }

    /// <summary>
    /// Serves as the default hash function.
    /// </summary>
    /// <returns>A hash code for the current object.</returns>
    public override int GetHashCode() => Code.GetHashCode();
}
