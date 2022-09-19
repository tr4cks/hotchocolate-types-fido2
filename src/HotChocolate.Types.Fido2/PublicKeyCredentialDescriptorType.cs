using Fido2NetLib.Objects;
using HotChocolate.Types.Fido2.Scalars;

namespace HotChocolate.Types.Fido2;

public class PublicKeyCredentialDescriptorType : ObjectType<PublicKeyCredentialDescriptor>
{
    protected override void Configure(IObjectTypeDescriptor<PublicKeyCredentialDescriptor> descriptor)
    {
        descriptor.BindFieldsExplicitly()
            .Name(WellKnownObjectTypes.PublicKeyCredentialDescriptor)
            .Description("[Documentation](https://w3c.github.io/webauthn/#dictdef-publickeycredentialdescriptor)");

        // is not consistent with the specification
        descriptor.Field(f => f.Type)
            .Type<EnumMemberType<PublicKeyCredentialType>>()
            .Description("[Documentation](https://w3c.github.io/webauthn/#dom-publickeycredentialdescriptor-type)");

        descriptor.Field(f => f.Id)
            .Type<NonNullType<Base64Type>>()
            .Description("[Documentation](https://w3c.github.io/webauthn/#dom-publickeycredentialdescriptor-id)");

        descriptor.Field(f => f.Transports)
            .Type<ListType<NonNullType<EnumMemberType<AuthenticatorTransport>>>>()
            .Description("[Documentation](https://w3c.github.io/webauthn/#dom-publickeycredentialdescriptor-transports)");
    }
}
