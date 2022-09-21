using Fido2NetLib;
using HotChocolate.Types.Fido2.Scalars;

namespace HotChocolate.Types.Fido2;

// ReSharper disable once ClassNeverInstantiated.Global
public class PublicKeyCredentialUserEntityType : ObjectType<Fido2User>
{
    protected override void Configure(IObjectTypeDescriptor<Fido2User> descriptor)
    {
        descriptor.BindFieldsExplicitly()
            .Name(WellKnownObjectTypes.PublicKeyCredentialUserEntity)
            .Description("[Documentation](https://w3c.github.io/webauthn/#dictdef-publickeycredentialuserentity)");

        descriptor.Field(f => f.Id)
            .Type<NonNullType<Base64Type>>()
            .Description("[Documentation](https://w3c.github.io/webauthn/#dom-publickeycredentialuserentity-id)");

        descriptor.Field(f => f.DisplayName)
            .Type<NonNullType<StringType>>()
            .Description("[Documentation](https://w3c.github.io/webauthn/#dom-publickeycredentialuserentity-displayname)");

        descriptor.Field(f => f.Name)
            .Type<NonNullType<StringType>>()
            .Description("[Documentation](https://w3c.github.io/webauthn/#dom-publickeycredentialentity-name)");
    }
}
