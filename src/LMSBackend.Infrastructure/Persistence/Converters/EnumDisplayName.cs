using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

public class EnumDisplayName<TEnum> : ValueConverter<TEnum, string>
    where TEnum : struct, Enum
{
    public EnumDisplayName()
        : base(
            enumValue => GetDisplayName(enumValue),
            stringValue => ParseEnum(stringValue))
    { }

    private static string GetDisplayName(TEnum value)
    {
        return typeof(TEnum)
            .GetMember(value.ToString())
            .First()
            .GetCustomAttribute<DisplayAttribute>()?
            .Name
            ?? value.ToString();
    }

    private static TEnum ParseEnum(string value)
    {
        foreach (var field in typeof(TEnum).GetFields())
        {
            var display = field.GetCustomAttribute<DisplayAttribute>()?.Name;
            if (display == value)
                return (TEnum)field.GetValue(null)!;
        }

        return Enum.Parse<TEnum>(value);
    }
}
