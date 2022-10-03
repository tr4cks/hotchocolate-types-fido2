using Fido2NetLib;
using Fido2NetLib.Objects;
using HotChocolate.Types.Fido2.Scalars;

namespace HotChocolate.Types.Fido2;

// ReSharper disable once ClassNeverInstantiated.Global
public class PublicKeyCredentialAttestationInputType : InputObjectType<AuthenticatorAttestationRawResponse>
{
    protected override void Configure(IInputObjectTypeDescriptor<AuthenticatorAttestationRawResponse> descriptor)
    {
        descriptor.BindFieldsExplicitly()
            .Name(WellKnownObjectTypes.PublicKeyCredentialAttestationInput)
            .Description(ObjectTypeResources.PublicKeyCredentialAttestationInputType_Description);

        descriptor.Field(f => f.Id)
            .Type<NonNullType<Base64Type>>()
            .Description(ObjectTypeResources.PublicKeyCredentialAttestationInputType_Id_Description);

        descriptor.Field(f => f.Type)
            .Type<NonNullType<EnumMemberType<PublicKeyCredentialType>>>()
            .Description(ObjectTypeResources.PublicKeyCredentialAttestationInputType_Type_Description);

        descriptor.Field(f => f.RawId)
            .Type<NonNullType<Base64Type>>()
            .Description(ObjectTypeResources.PublicKeyCredentialAttestationInputType_RawId_Description);

        descriptor.Field(f => f.Response)
            .Type<NonNullType<AuthenticatorAttestationResponseInputType>>()
            .Description(ObjectTypeResources.PublicKeyCredentialAttestationInputType_Response_Description);

        descriptor.Field(f => f.Extensions)
            .Type<AuthenticationExtensionsClientOutputsType>()
            .Description(ObjectTypeResources.PublicKeyCredentialAttestationInputType_Extensions_Description);
    }
}

// ReSharper disable once ClassNeverInstantiated.Global
public class AuthenticatorAttestationResponseInputType : InputObjectType<AuthenticatorAttestationRawResponse.ResponseData>
{
    protected override void Configure(IInputObjectTypeDescriptor<AuthenticatorAttestationRawResponse.ResponseData> descriptor)
    {
        descriptor.BindFieldsExplicitly()
            .Name(WellKnownObjectTypes.AuthenticatorAttestationResponseInput)
            .Description(ObjectTypeResources.AuthenticatorAttestationResponseInputType_Description);

        descriptor.Field(f => f.ClientDataJson)
            .Name("clientDataJSON")
            .Type<NonNullType<Base64Type>>()
            .Description(ObjectTypeResources.AuthenticatorAttestationResponseInputType_ClientDataJson_Description);

        descriptor.Field(f => f.AttestationObject)
            .Type<Base64Type>()
            .Description(ObjectTypeResources.AuthenticatorAttestationResponseInputType_AttestationObject_Description);
    }
}
