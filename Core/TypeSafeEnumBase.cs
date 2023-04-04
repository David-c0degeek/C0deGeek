using System.Reflection;
using Core.Models;
using Extensions;

namespace Core;

public abstract class TypeSafeEnumBase<T> : CodeNameModel, IComparable where T : CodeNameModel
{
    protected TypeSafeEnumBase(string code, string name) : base(code,
        name)
    {
    }

    public int CompareTo(object? other)
    {
        return string.Compare(Code, ((TypeSafeEnumBase<T>?)other)?.Code, StringComparison.InvariantCultureIgnoreCase);
    }

    public override string ToString()
    {
        return Name;
    }

    public static IEnumerable<T> GetAll()
    {
        var type = typeof(T);
        var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);

        return fields
            .Select(info => info.GetValue(null))
            .OfType<T>()
            .ToList();
    }

    public static T? GetByCode(string code)
    {
        var type = typeof(T);
        var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);

        return fields
            .Select(info => info.GetValue(null))
            .OfType<T>()
            .FirstOrDefault(x => x.Code.Split(",").Contains(code));
    }

    public static string GetValidationMessageForUnsupportedCode(string codeValue)
    {
        var type = typeof(T);

        var typeName = $"{type.Name}.Code";

        var msg = GetAll()
            .Select(x => x.Code)
            .ToList()
            .GetValidationMessageForStringValueNotInList(
                typeName,
                codeValue);

        return msg;
    }

    public override bool Equals(object? obj)
    {
        if (obj is not TypeSafeEnumBase<T> otherValue) return false;

        var typeMatches = GetType() == obj.GetType();
        var valueMatches = Code.Equals(otherValue.Code);

        return typeMatches && valueMatches;
    }

    public override int GetHashCode()
    {
        return Code.GetHashCode();
    }
}