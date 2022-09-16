namespace HotChocolate.Types.Fido2.Scalars;

internal static class ThrowHelper
{
    public static SerializationException Base64_ParseValue_IsInvalid(IType type)
    {
        return new SerializationException(
            ErrorBuilder.New()
                .SetMessage(ScalarResources.Base64Type_IsInvalid_ParseValue)
                .SetCode(ErrorCodes.Scalars.InvalidRuntimeType)
                .SetExtension("actualType", WellKnownScalarTypes.Base64)
                .Build(),
            type);
    }

    public static SerializationException Base64_ParseLiteral_IsInvalid(IType type)
    {
        return new SerializationException(
            ErrorBuilder.New()
                .SetMessage(ScalarResources.Base64Type_IsInvalid_ParseLiteral)
                .SetCode(ErrorCodes.Scalars.InvalidSyntaxFormat)
                .SetExtension("actualType", WellKnownScalarTypes.Base64)
                .Build(),
            type);
    }

    // todo: update error message
    public static SerializationException EnumMember_ParseValue_IsInvalid(IType type)
    {
        return new SerializationException(
            ErrorBuilder.New()
                .SetMessage(ScalarResources.EnumMemberType_IsInvalid_ParseValue)
                .SetCode(ErrorCodes.Scalars.InvalidRuntimeType)
                .SetExtension("actualType", WellKnownScalarTypes.EnumMember)
                .Build(),
            type);
    }

    // todo: update error message
    public static SerializationException EnumMember_ParseLiteral_IsInvalid(IType type)
    {
        return new SerializationException(
            ErrorBuilder.New()
                .SetMessage(ScalarResources.EnumMemberType_IsInvalid_ParseLiteral)
                .SetCode(ErrorCodes.Scalars.InvalidSyntaxFormat)
                .SetExtension("actualType", WellKnownScalarTypes.EnumMember)
                .Build(),
            type);
    }
}
