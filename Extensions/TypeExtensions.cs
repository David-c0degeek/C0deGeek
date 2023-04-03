using System.Collections.Concurrent;
using System.Reflection;

namespace Extensions;

public static class TypeExtensions
{
    private static readonly ConcurrentDictionary<Type, bool> IsSimpleTypeCache = new();

    public static IEnumerable<PropertyInfo> GetPropertiesWithPrivate(this Type type)
    {
        return type.GetProperties(
            BindingFlags.NonPublic |
            BindingFlags.Public |
            BindingFlags.Instance |
            BindingFlags.Static
        );
    }

    public static FieldInfo[] GetFieldsWithPrivate(this Type type)
    {
        return type.GetFields(
            BindingFlags.NonPublic |
            BindingFlags.Public |
            BindingFlags.Instance |
            BindingFlags.Static
        );
    }

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