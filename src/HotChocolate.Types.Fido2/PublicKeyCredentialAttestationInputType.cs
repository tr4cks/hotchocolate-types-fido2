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
            .Description("[Description](https://w3c.github.io/webauthn/#publickeycredential)");

        descriptor.Field(f => f.Id)
            .Type<NonNullType<Base64Type>>()
            .Description("[Documentation](https://w3c.github.io/webappsec-credential-management/#dom-credential-id)");

        descriptor.Field(f => f.Type)
            .Type<NonNullType<EnumMemberType<PublicKeyCredentialType>>>()
            .Description("[Documentation](https://w3c.github.io/webappsec-credential-management/#dom-credential-type)");

        descriptor.Field(f => f.RawId)
            .Type<NonNullType<Base64Type>>()
            .Description("[Documentation](https://w3c.github.io/webauthn/#dom-publickeycredential-rawid)");

        descriptor.Field(f => f.Response)
            .Type<NonNullType<AuthenticatorAttestationResponseInputType>>()
            .Description("[Documentation](https://w3c.github.io/webauthn/#dom-publickeycredential-response)");

        descriptor.Field(f => f.Extensions)
            .Type<AuthenticationExtensionsClientOutputsType>()
            .Description("[Documentation](https://w3c.github.io/webauthn/#dictdef-authenticationextensionsclientoutputs)");
    }
}

// ReSharper disable once ClassNeverInstantiated.Global
public class AuthenticatorAttestationResponseInputType : InputObjectType<AuthenticatorAttestationRawResponse.ResponseData>
{
    protected override void Configure(IInputObjectTypeDescriptor<AuthenticatorAttestationRawResponse.ResponseData> descriptor)
    {
        descriptor.BindFieldsExplicitly()
            .Name(WellKnownObjectTypes.AuthenticatorAttestationResponseInput)
            .Description("[Description](https://w3c.github.io/webauthn/#authenticatorattestationresponse)");

        descriptor.Field(f => f.ClientDataJson)
            .Name("clientDataJSON")
            .Type<NonNullType<Base64Type>>()
            .Description("[Description](https://w3c.github.io/webauthn/#dom-authenticatorresponse-clientdatajson)");

        descriptor.Field(f => f.AttestationObject)
            .Type<Base64Type>()
            .Description("[Description](https://w3c.github.io/webauthn/#dom-authenticatorattestationresponse-attestationobject)");
    }
}
