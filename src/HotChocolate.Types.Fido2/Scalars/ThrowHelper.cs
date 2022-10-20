using System.Diagnostics.CodeAnalysis;

namespace HotChocolate.Types.Fido2.Scalars;

[ExcludeFromCodeCoverage]
internal static class ThrowHelper
{
    public static SerializationException Base64Url_ParseValue_IsInvalid(IType type)
    {
        return new SerializationException(
            ErrorBuilder.New()
                .SetMessage(ScalarResources.Base64UrlType_IsInvalid_ParseValue)
                .SetCode(ErrorCodes.Scalars.InvalidRuntimeType)
                .SetExtension("actualType", WellKnownScalarTypes.Base64Url)
                .Build(),
            type);
    }

    public static SerializationException Base64Url_ParseLiteral_IsInvalid(IType type)
    {
        return new SerializationException(
            ErrorBuilder.New()
                .SetMessage(ScalarResources.Base64UrlType_IsInvalid_ParseLiteral)
                .SetCode(ErrorCodes.Scalars.InvalidSyntaxFormat)
                .SetExtension("actualType", WellKnownScalarTypes.Base64Url)
                .Build(),
            type);
    }

    public static SerializationException EnumMember_ParseValue_IsInvalid(
        IType type,
        string runtimeTypeName)
    {
        return new SerializationException(
            ErrorBuilder.New()
                .SetMessage(string.Format(
                    ScalarResources.EnumMemberType_IsInvalid_ParseValue, runtimeTypeName))
                .SetCode(ErrorCodes.Scalars.InvalidRuntimeType)
                .SetExtension("actualType", type.TypeName().ToString())
                .Build(),
            type);
    }

    public static SerializationException EnumMember_ParseLiteral_IsInvalid(
        IType type,
        string runtimeTypeName)
    {
        return new SerializationException(
            ErrorBuilder.New()
                .SetMessage(string.Format(
                    ScalarResources.EnumMemberType_IsInvalid_ParseLiteral,
                    runtimeTypeName))
                .SetCode(ErrorCodes.Scalars.InvalidSyntaxFormat)
                .SetExtension("actualType", type.TypeName().ToString())
                .Build(),
            type);
    }

    public static SerializationException TypedDictionary_ParseValue_IsInvalid(
        IType type,
        string runtimeTypeName)
    {
        return new SerializationException(
            ErrorBuilder.New()
                .SetMessage(string.Format(
                    ScalarResources.TypedDictionaryType_IsInvalid_ParseValue,
                    runtimeTypeName))
                .SetCode(ErrorCodes.Scalars.InvalidRuntimeType)
                .SetExtension("actualType", type.TypeName().ToString())
                .Build(),
            type);
    }

    public static SerializationException TypedDictionary_ParseLiteral_IsInvalid(
        IType type,
        string runtimeTypeName)
    {
        return new SerializationException(
            ErrorBuilder.New()
                .SetMessage(string.Format(
                    ScalarResources.TypedDictionaryType_IsInvalid_ParseLiteral,
                    runtimeTypeName))
                .SetCode(ErrorCodes.Scalars.InvalidSyntaxFormat)
                .SetExtension("actualType", type.TypeName().ToString())
                .Build(),
            type);
    }
}
