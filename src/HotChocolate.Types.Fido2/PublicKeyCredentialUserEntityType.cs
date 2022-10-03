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
            .Description(ObjectTypeResources.PublicKeyCredentialUserEntityType_Description);

        descriptor.Field(f => f.Id)
            .Type<NonNullType<Base64Type>>()
            .Description(ObjectTypeResources.PublicKeyCredentialUserEntityType_Id_Description);

        descriptor.Field(f => f.DisplayName)
            .Type<NonNullType<StringType>>()
            .Description(ObjectTypeResources.PublicKeyCredentialUserEntityType_DisplayName_Description);

        descriptor.Field(f => f.Name)
            .Type<NonNullType<StringType>>()
            .Description(ObjectTypeResources.PublicKeyCredentialUserEntityType_Name_Description);
    }
}
