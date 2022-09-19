using System.Text.Json;
using Fido2NetLib;
using Fido2NetLib.Objects;
using HotChocolate.Types.Fido2.Scalars;

namespace HotChocolate.Types.Fido2;

// todo: replace description with resource file

// ReSharper disable once ClassNeverInstantiated.Global
public class PublicKeyCredentialRequestOptionsType : ObjectType<AssertionOptions>
{
    protected override void Configure(IObjectTypeDescriptor<AssertionOptions> descriptor)
    {
        descriptor.BindFieldsExplicitly()
            .Name(WellKnownObjectTypes.PublicKeyCredentialRequestOptions)
            .Description("[Documentation](https://w3c.github.io/webauthn/#dictionary-assertion-options)");

        descriptor.Field(f => f.Challenge)
            .Type<NonNullType<Base64Type>>()
            .Description("[Documentation](https://w3c.github.io/webauthn/#dom-publickeycredentialrequestoptions-challenge)");

        descriptor.Field(f => f.Timeout)
            .Type<UnsignedLongType>()
            .Description("[Documentation](https://w3c.github.io/webauthn/#dom-publickeycredentialrequestoptions-timeout)");

        descriptor.Field(f => f.RpId)
            .Type<StringType>()
            .Description("[Documentation](https://w3c.github.io/webauthn/#dom-publickeycredentialrequestoptions-rpid)");

        descriptor.Field(f => f.AllowCredentials)
            .Type<ListType<NonNullType<PublicKeyCredentialDescriptorType>>>()
            .Description("[Documentation](https://w3c.github.io/webauthn/#dom-publickeycredentialrequestoptions-allowcredentials)");

        descriptor.Field(f => f.UserVerification)
            .Type<EnumMemberType<UserVerificationRequirement>>()
            .Description("[Documentation](https://w3c.github.io/webauthn/#dom-publickeycredentialrequestoptions-userverification)");

        descriptor.Field(f => f.Extensions)
            .Type<StringType>()
            .Resolve(context => JsonSerializer.Serialize(context.Parent<AssertionOptions>().Extensions))
            .Description("[Documentation](https://w3c.github.io/webauthn/#dom-publickeycredentialrequestoptions-extensions)");
    }
}
