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
            .AddType<PublicKeyCredentialType>()
            .AddType<PublicKeyCredentialRequestOptionsType>()
            .AddType<PublicKeyCredentialAttestationInputType>()
            .AddType<AttestationVerificationSuccessType>()
            .AddType<AssertionVerificationResultType>()
            .AddType<PublicKeyCredentialAssertionInputType>()
            // todo: sort and add descriptions (auto?)
            .AddType(new EnumMemberType<AttestationConveyancePreference>(WellKnownScalarTypes
                .AttestationConveyancePreferenceStringEnum))
            .AddType(new EnumMemberType<PublicKeyCredentialType>(WellKnownScalarTypes.PublicKeyCredentialTypeStringEnum))
            .AddType(new EnumMemberType<AuthenticatorAttachment>(WellKnownScalarTypes.AuthenticatorAttachmentStringEnum))
            .AddType(new EnumMemberType<UserVerificationRequirement>(WellKnownScalarTypes.UserVerificationRequirementStringEnum))
            .AddType(new EnumMemberType<AuthenticatorTransport>(WellKnownScalarTypes.AuthenticatorTransportStringEnum));
}
