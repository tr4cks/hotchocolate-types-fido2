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
            // todo: sort
            .RegisterService<IFido2>()
            .AddType<PublicKeyCredentialCreationOptionsType>()
            .AddType<PublicKeyCredentialRequestOptionsType>()
            .AddType<PublicKeyCredentialAttestationInputType>()
            .AddType<AttestationVerificationSuccessType>()
            .AddType<AssertionVerificationResultType>()
            .AddType<PublicKeyCredentialAssertionInputType>()
            // todo: sort and add descriptions (auto?)
            .AddType<AttestationConveyancePreferenceType>()
            .AddType(new EnumMemberType<AttestationConveyancePreference>(WellKnownScalarTypes
                .AttestationConveyancePreferenceStringEnum))
            .AddType<PublicKeyCredentialTypeType>()
            .AddType(new EnumMemberType<PublicKeyCredentialType>(WellKnownScalarTypes.PublicKeyCredentialTypeStringEnum))
            .AddType<AuthenticatorAttachmentType>()
            .AddType(new EnumMemberType<AuthenticatorAttachment>(WellKnownScalarTypes.AuthenticatorAttachmentStringEnum))
            .AddType<UserVerificationRequirementType>()
            .AddType(new EnumMemberType<UserVerificationRequirement>(WellKnownScalarTypes.UserVerificationRequirementStringEnum))
            .AddType<AuthenticatorTransportType>()
            .AddType(new EnumMemberType<AuthenticatorTransport>(WellKnownScalarTypes.AuthenticatorTransportStringEnum));
}
