using System.Reflection;
using System.Runtime.Serialization;

namespace ClinicVet.PetManager.Job.Domain.Helpers.v1;

public static class EnumHelper
{
    public static string DataBaseValue<T>(this T enumValue) where T : Enum
    {
        var memberInfo = typeof(T).GetField(enumValue.ToString());
        var enumAttribute = memberInfo?.GetCustomAttribute<EnumMemberAttribute>();
        return enumAttribute?.Value ?? enumValue.ToString();
    }
}