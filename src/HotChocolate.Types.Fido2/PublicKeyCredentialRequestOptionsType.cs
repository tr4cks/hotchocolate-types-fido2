using Fido2NetLib;
using Fido2NetLib.Objects;
using HotChocolate.Types.Fido2.Scalars;

namespace HotChocolate.Types.Fido2;

// ReSharper disable once ClassNeverInstantiated.Global
public class PublicKeyCredentialRequestOptionsType : ObjectType<AssertionOptions>
{
    protected override void Configure(IObjectTypeDescriptor<AssertionOptions> descriptor)
    {
        descriptor.BindFieldsExplicitly()
            .Name(WellKnownObjectTypes.PublicKeyCredentialRequestOptions)
            .Description(ObjectTypeResources.PublicKeyCredentialRequestOptionsType_Description);

        descriptor.Field(f => f.Challenge)
            .Type<NonNullType<Base64UrlType>>()
            .Description(ObjectTypeResources.PublicKeyCredentialRequestOptionsType_Challenge_Description);

        descriptor.Field(f => f.Timeout)
            .Type<UnsignedLongType>()
            .Description(ObjectTypeResources.PublicKeyCredentialRequestOptionsType_Timeout_Description);

        descriptor.Field(f => f.RpId)
            .Type<StringType>()
            .Description(ObjectTypeResources.PublicKeyCredentialRequestOptionsType_RpId_Description);

        descriptor.Field(f => f.AllowCredentials)
            .Type<ListType<NonNullType<PublicKeyCredentialDescriptorType>>>()
            .Description(ObjectTypeResources.PublicKeyCredentialRequestOptionsType_AllowCredentials_Description);

        descriptor.Field(f => f.UserVerification)
            .Type<EnumMemberType<UserVerificationRequirement>>()
            .Description(ObjectTypeResources.PublicKeyCredentialRequestOptionsType_UserVerification_Description);

        descriptor.Field(f => f.Extensions)
            .Type<AuthenticationExtensionsClientInputsType>()
            .Description(ObjectTypeResources.PublicKeyCredentialRequestOptionsType_Extensions_Description);
    }
}
