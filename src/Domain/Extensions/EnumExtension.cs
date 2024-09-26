using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Domain.Extensions;

public static class EnumExtension
{
    public static T ToEnum<T>(this int value)
    {
        return (T)Enum.Parse(typeof(T), value.ToString(), true);
    }

    public static T ToEnum<T>(this string value)
    {
        return (T)Enum.Parse(typeof(T), value, true);
    }

    public static string GetDisplayName(this Enum enu)
    {
        var attr = GetDisplayAttribute(enu);
        return attr != null ? attr.Name : enu.ToString();
    }

    private static DisplayAttribute GetDisplayAttribute(object value)
    {
        Type type = value.GetType();
        if (!type.IsEnum)
        {
            throw new ArgumentException(string.Format("Tipo {0} não é um enumerador", type));
        }

        // Get the enum field.
        var field = type.GetField(value.ToString());
        return field == null ? null : field.GetCustomAttribute<DisplayAttribute>();
    }
}
