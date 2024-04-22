using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Utility.Extensions;

public static class EnumExtension
{
    public static string? GetDisplayName(this Enum enumValue)
    {
        Type enumType = enumValue.GetType();
        MemberInfo[] memberInfo = enumType.GetMember(enumValue.ToString());
        if ((memberInfo.Length > 0))
        {
            var attribs = memberInfo[0].GetCustomAttributes(typeof(DisplayAttribute), false);
            if ((attribs.Length > 0))
            {
                return ((DisplayAttribute)attribs[0]).Name;
            }
        }

        return enumValue.ToString();
    }
}