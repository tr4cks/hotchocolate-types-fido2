using Fido2NetLib;
using Fido2NetLib.Objects;
using HotChocolate.Execution.Configuration;
using HotChocolate.Types.Fido2.Scalars;
using Microsoft.Extensions.DependencyInjection;

namespace HotChocolate.Types.Fido2.Extensions;

public static class RequestExecutorBuilderExtensions
{
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
            // todo: Add descriptions to enum strings (auto?)
            .AddType<AttestationConveyancePreferenceType>()
            .AddType(new EnumMemberType<AttestationConveyancePreference>(WellKnownScalarTypes.AttestationConveyancePreferenceStringEnum))
            .AddType<AuthenticatorAttachmentType>()
            .AddType(new EnumMemberType<AuthenticatorAttachment>(WellKnownScalarTypes.AuthenticatorAttachmentStringEnum))
            .AddType<AuthenticatorTransportType>()
            .AddType(new EnumMemberType<AuthenticatorTransport>(WellKnownScalarTypes.AuthenticatorTransportStringEnum))
            .AddType<PublicKeyCredentialTypeType>()
            .AddType(new EnumMemberType<PublicKeyCredentialType>(WellKnownScalarTypes.PublicKeyCredentialTypeStringEnum))
            .AddType<UserVerificationRequirementType>()
            .AddType(new EnumMemberType<UserVerificationRequirement>(WellKnownScalarTypes.UserVerificationRequirementStringEnum));
}
