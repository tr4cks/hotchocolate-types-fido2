using Fido2NetLib.Objects;
using HotChocolate.Types.Fido2.Scalars;

namespace HotChocolate.Types.Fido2;

// ReSharper disable once ClassNeverInstantiated.Global
public class PublicKeyCredentialDescriptorType : ObjectType<PublicKeyCredentialDescriptor>
{
    protected override void Configure(
        IObjectTypeDescriptor<PublicKeyCredentialDescriptor> descriptor)
    {
        descriptor.BindFieldsExplicitly()
            .Name(WellKnownObjectTypes.PublicKeyCredentialDescriptor)
            .Description(
                ObjectTypeResources.PublicKeyCredentialDescriptorType_Description);

        // is not consistent with the specification
        descriptor.Field(f => f.Type)
            .Type<EnumMemberType<PublicKeyCredentialType>>()
            .Description(ObjectTypeResources
                .PublicKeyCredentialDescriptorType_Type_Description);

        descriptor.Field(f => f.Id)
            .Type<NonNullType<Base64UrlType>>()
            .Description(ObjectTypeResources
                .PublicKeyCredentialDescriptorType_Id_Description);

        descriptor.Field(f => f.Transports)
            .Type<ListType<NonNullType<EnumMemberType<AuthenticatorTransport>>>>()
            .Description(ObjectTypeResources
                .PublicKeyCredentialDescriptorType_Transports_Description);
    }
}
