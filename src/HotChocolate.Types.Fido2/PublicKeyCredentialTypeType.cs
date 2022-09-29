using Fido2NetLib.Objects;

namespace HotChocolate.Types.Fido2;

// ReSharper disable once ClassNeverInstantiated.Global
public class PublicKeyCredentialTypeType : EnumType<PublicKeyCredentialType>
{
    protected override void Configure(IEnumTypeDescriptor<PublicKeyCredentialType> descriptor)
    {
        descriptor.BindValuesExplicitly()
            .Name(WellKnownObjectTypes.PublicKeyCredentialType)
            .Description(ObjectTypeResources.PublicKeyCredentialTypeType_Description);

        descriptor.Value(PublicKeyCredentialType.PublicKey)
            .Description(ObjectTypeResources.PublicKeyCredentialTypeType_PublicKey_Description);
    }
}
