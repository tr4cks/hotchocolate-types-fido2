namespace HotChocolate.Types.Fido2.Scalars;

// todo: use resource file for messages
internal static class ThrowHelper
{
    public static SerializationException Base64_ParseValue_IsInvalid(IType type)
    {
        return new SerializationException(
            ErrorBuilder.New()
                .SetMessage("Base64Type cannot parse the provided value. The provided value is not encoded in base 64.")
                .SetCode(ErrorCodes.Scalars.InvalidRuntimeType)
                .SetExtension("actualType", WellKnownScalarTypes.Base64)
                .Build(),
            type);
    }

    public static SerializationException Base64_ParseLiteral_IsInvalid(IType type)
    {
        return new SerializationException(
            ErrorBuilder.New()
                .SetMessage("Base64Type cannot parse the provided literal. The provided value is not encoded in base 64.")
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
                .SetMessage("EnumMemberType cannot parse the provided value. The provided value is not encoded in base 64.")
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
                .SetMessage("EnumMemberType cannot parse the provided literal. The provided value is not encoded in base 64.")
                .SetCode(ErrorCodes.Scalars.InvalidSyntaxFormat)
                .SetExtension("actualType", WellKnownScalarTypes.EnumMember)
                .Build(),
            type);
    }
}
