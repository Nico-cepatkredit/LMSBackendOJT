using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace LMSBackend.Application.Common.Extensions
{
    public static class EnumExtensions
    {
        public static string GetLabel(this Enum value)
        {
            return value
                .GetType()
                .GetMember(value.ToString())
                .First()
                .GetCustomAttribute<DisplayAttribute>()?
                .Name
                ?? value.ToString();
        }
    }
}