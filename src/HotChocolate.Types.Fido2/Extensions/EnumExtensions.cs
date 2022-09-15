using System.Reflection;
using System.Runtime.Serialization;

namespace HotChocolate.Types.Fido2.Extensions;

internal static class EnumExtensions
{
    // todo: refactor?
    public static string? GetEnumMemberValue<TEnum>(this TEnum @this)
        where TEnum : Enum =>
        typeof(TEnum)
            .GetTypeInfo()
            .DeclaredMembers
            .SingleOrDefault(x => x.Name == @this.ToString())
            ?.GetCustomAttribute<EnumMemberAttribute>(false)
            ?.Value;


    public static TEnum? GetEnumFromEnumMemberValue<TEnum>(string enumMemberValue)
        where TEnum : struct, Enum =>
        typeof(TEnum).GetFields()
            .SingleOrDefault(x => x.GetCustomAttribute<EnumMemberAttribute>(false)?.Value == enumMemberValue)?
            .GetValue(null) as TEnum?;
}
