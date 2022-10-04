using Fido2NetLib;
using Fido2NetLib.Objects;
using HotChocolate.Execution.Configuration;
using HotChocolate.Types.Fido2.Scalars;
using Microsoft.Extensions.DependencyInjection;

namespace HotChocolate.Types.Fido2.Extensions;

public static class RequestExecutorBuilderExtensions
{
    private static EnumMemberType<TEnum> CreateWebAuthnEnum<TEnum>(
        string name,
        string originalName)
        where TEnum : struct, Enum => new(name,
        string.Format(ScalarResources.EnumMemberType_WebAuthn_Description, originalName,
            string.Join(", ", EnumNameMapper<TEnum>.GetNames().Select(x => $"`{x}`"))));

    public static IRequestExecutorBuilder AddFido2(this IRequestExecutorBuilder @this) =>
        @this
            // Register IFido2 service
            .RegisterService<IFido2>()

            // Fido2 input object types
            .AddType<PublicKeyCredentialAssertionInputType>()
            .AddType<PublicKeyCredentialAttestationInputType>()

            // Fido2 object types
            .AddType<AssertionVerificationResultType>()
            .AddType<AttestationConveyancePreferenceType>()
            .AddType<AttestationVerificationSuccessType>()
            .AddType<PublicKeyCredentialCreationOptionsType>()
            .AddType<PublicKeyCredentialRequestOptionsType>()

            // Fido2 enum types
            .AddType<AttestationConveyancePreferenceType>()
            .AddType(CreateWebAuthnEnum<AttestationConveyancePreference>(
                WellKnownScalarTypes.AttestationConveyancePreferenceStringEnum,
                WellKnownObjectTypes.AttestationConveyancePreference))
            .AddType<AuthenticatorAttachmentType>()
            .AddType(CreateWebAuthnEnum<AuthenticatorAttachment>(
                WellKnownScalarTypes.AuthenticatorAttachmentStringEnum,
                WellKnownObjectTypes.AuthenticatorAttachment))
            .AddType<AuthenticatorTransportType>()
            .AddType(CreateWebAuthnEnum<AuthenticatorTransport>(
                WellKnownScalarTypes.AuthenticatorTransportStringEnum,
                WellKnownObjectTypes.AuthenticatorTransport))
            .AddType<PublicKeyCredentialTypeType>()
            .AddType(CreateWebAuthnEnum<PublicKeyCredentialType>(
                WellKnownScalarTypes.PublicKeyCredentialTypeStringEnum,
                WellKnownObjectTypes.PublicKeyCredentialType))
            .AddType<UserVerificationRequirementType>()
            .AddType(CreateWebAuthnEnum<UserVerificationRequirement>(
                WellKnownScalarTypes.UserVerificationRequirementStringEnum,
                WellKnownObjectTypes.UserVerificationRequirement));
}
