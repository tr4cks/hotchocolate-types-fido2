using Fido2NetLib;
using Fido2NetLib.Objects;
using HotChocolate.Types.Fido2.Scalars;

namespace HotChocolate.Types.Fido2;

public class PublicKeyCredentialAssertionInputType : InputObjectType<AuthenticatorAssertionRawResponse>
{
    protected override void Configure(IInputObjectTypeDescriptor<AuthenticatorAssertionRawResponse> descriptor)
    {
        descriptor.BindFieldsExplicitly()
            .Name(WellKnownObjectTypes.PublicKeyCredentialAssertionInput)
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
            .Type<AuthenticatorAssertionResponseInputType>()
            .Description("[Documentation](https://w3c.github.io/webauthn/#dom-publickeycredential-response)");

        descriptor.Field(f => f.Extensions)
            .Type<AuthenticationExtensionsClientOutputsType>()
            .Description("[Documentation](https://w3c.github.io/webauthn/#dictdef-authenticationextensionsclientoutputs)");
    }
}

// ReSharper disable once ClassNeverInstantiated.Global
public class AuthenticatorAssertionResponseInputType : InputObjectType<AuthenticatorAssertionRawResponse.AssertionResponse>
{
    protected override void Configure(IInputObjectTypeDescriptor<AuthenticatorAssertionRawResponse.AssertionResponse> descriptor)
    {
        descriptor.BindFieldsExplicitly()
            .Name(WellKnownObjectTypes.AuthenticatorAssertionResponseInput)
            .Description("https://w3c.github.io/webauthn/#authenticatorassertionresponse");

        descriptor.Field(f => f.ClientDataJson)
            .Name("clientDataJSON")
            .Type<NonNullType<Base64Type>>()
            .Description("[Documentation](https://w3c.github.io/webauthn/#dom-authenticatorresponse-clientdatajson)");

        descriptor.Field(f => f.AuthenticatorData)
            .Type<NonNullType<Base64Type>>()
            .Description("[Documentation](https://w3c.github.io/webauthn/#dom-authenticatorassertionresponse-authenticatordata)");

        descriptor.Field(f => f.Signature)
            .Type<NonNullType<Base64Type>>()
            .Description("[Documentation](https://w3c.github.io/webauthn/#dom-authenticatorassertionresponse-signature)");

        descriptor.Field(f => f.UserHandle)
            .Type<Base64Type>()
            .Description("[Documentation](https://w3c.github.io/webauthn/#dom-authenticatorassertionresponse-userhandle)");
    }
}
