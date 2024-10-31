using System.Collections.Concurrent;
using System.Reflection;

namespace Extensions;

/// <summary>
/// Provides extension methods for Type reflection operations.
/// </summary>
public static class TypeExtensions
{
    /// <summary>
    /// Cache to store results of IsSimpleType checks for better performance.
    /// </summary>
    private static readonly ConcurrentDictionary<Type, bool> IsSimpleTypeCache = new();

    /// <summary>
    /// Gets all properties of a type, including private ones.
    /// </summary>
    /// <param name="type">The type to get properties from.</param>
    /// <returns>An enumerable collection of PropertyInfo objects representing all properties.</returns>
    /// <remarks>
    /// This method includes:
    /// - Public and private properties
    /// - Instance and static properties
    /// </remarks>
    public static IEnumerable<PropertyInfo> GetPropertiesWithPrivate(this Type type)
    {
        return type.GetProperties(
            BindingFlags.NonPublic |
            BindingFlags.Public |
            BindingFlags.Instance |
            BindingFlags.Static
        );
    }

    /// <summary>
    /// Gets all fields of a type, including private ones.
    /// </summary>
    /// <param name="type">The type to get fields from.</param>
    /// <returns>An array of FieldInfo objects representing all fields.</returns>
    /// <remarks>
    /// This method includes:
    /// - Public and private fields
    /// - Instance and static fields
    /// </remarks>
    public static FieldInfo[] GetFieldsWithPrivate(this Type type)
    {
        return type.GetFields(
            BindingFlags.NonPublic |
            BindingFlags.Public |
            BindingFlags.Instance |
            BindingFlags.Static
        );
    }

    /// <summary>
    /// Determines whether a type is a simple type (primitive, string, decimal, DateTime, etc.).
    /// Results are cached for performance.
    /// </summary>
    /// <param name="type">The type to check.</param>
    /// <returns>true if the type is a simple type; otherwise, false.</returns>
    /// <remarks>
    /// Simple types include:
    /// - Primitive types
    /// - Enums
    /// - String
    /// - Decimal
    /// - DateTime/DateTimeOffset
    /// - DateOnly
    /// - TimeSpan
    /// - Guid
    /// - Nullable versions of the above types
    /// </remarks>
    public static bool IsSimpleType(this Type type)
    {
        if (IsSimpleTypeCache.TryGetValue(type, out var isSimple))
            return isSimple;

        isSimple = type.IsPrimitive ||
                   type.IsEnum ||
                   type == typeof(string) ||
                   type == typeof(decimal) ||
                   type == typeof(DateTime) ||
                   type == typeof(DateTimeOffset) ||
                   type == typeof(DateOnly) ||
                   type == typeof(TimeSpan) ||
                   type == typeof(Guid) ||
                   IsNullableSimpleType(type);

        return IsSimpleTypeCache.GetOrAdd(type, isSimple);

        static bool IsNullableSimpleType(Type t)
        {
            var underlyingType = Nullable.GetUnderlyingType(t);
            return underlyingType != null && IsSimpleType(underlyingType);
        }
    }
}