using Fido2NetLib;
using Fido2NetLib.Objects;
using HotChocolate.Types.Fido2.Scalars;

namespace HotChocolate.Types.Fido2;

// ReSharper disable once ClassNeverInstantiated.Global
public class PublicKeyCredentialAssertionInputType : 
    InputObjectType<AuthenticatorAssertionRawResponse>
{
    protected override void Configure(
        IInputObjectTypeDescriptor<AuthenticatorAssertionRawResponse> descriptor)
    {
        descriptor.BindFieldsExplicitly()
            .Name(WellKnownObjectTypes.PublicKeyCredentialAssertionInput)
            .Description(ObjectTypeResources
                .PublicKeyCredentialAssertionInputType_Description);

        descriptor.Field(f => f.Id)
            .Type<NonNullType<Base64UrlType>>()
            .Description(ObjectTypeResources
                .PublicKeyCredentialAssertionInputType_Id_Description);

        descriptor.Field(f => f.Type)
            .Type<NonNullType<EnumMemberType<PublicKeyCredentialType>>>()
            .Description(ObjectTypeResources
                .PublicKeyCredentialAssertionInputType_Type_Description);

        descriptor.Field(f => f.RawId)
            .Type<NonNullType<Base64UrlType>>()
            .Description(ObjectTypeResources
                .PublicKeyCredentialAssertionInputType_RawId_Description);

        descriptor.Field(f => f.Response)
            .Type<AuthenticatorAssertionResponseInputType>()
            .Description(ObjectTypeResources
                .PublicKeyCredentialAssertionInputType_Response_Description);

        descriptor.Field(f => f.Extensions)
            .Type<AuthenticationExtensionsClientOutputsType>()
            .Description(ObjectTypeResources
                .PublicKeyCredentialAssertionInputType_Extensions_Description);
    }
}

// ReSharper disable once ClassNeverInstantiated.Global
public class AuthenticatorAssertionResponseInputType : 
    InputObjectType<AuthenticatorAssertionRawResponse.AssertionResponse>
{
    protected override void Configure(
        IInputObjectTypeDescriptor<AuthenticatorAssertionRawResponse.AssertionResponse> descriptor)
    {
        descriptor.BindFieldsExplicitly()
            .Name(WellKnownObjectTypes.AuthenticatorAssertionResponseInput)
            .Description(ObjectTypeResources
                .AuthenticatorAssertionResponseInputType_Description);

        descriptor.Field(f => f.ClientDataJson)
            .Name("clientDataJSON")
            .Type<NonNullType<Base64UrlType>>()
            .Description(ObjectTypeResources
                .AuthenticatorAssertionResponseInputType_ClientDataJson_Description);

        descriptor.Field(f => f.AuthenticatorData)
            .Type<NonNullType<Base64UrlType>>()
            .Description(ObjectTypeResources
                .AuthenticatorAssertionResponseInputType_AuthenticatorData_Description);

        descriptor.Field(f => f.Signature)
            .Type<NonNullType<Base64UrlType>>()
            .Description(ObjectTypeResources
                .AuthenticatorAssertionResponseInputType_Signature_Description);

        descriptor.Field(f => f.UserHandle)
            .Type<Base64UrlType>()
            .Description(ObjectTypeResources
                .AuthenticatorAssertionResponseInputType_UserHandle_Description);
    }
}
