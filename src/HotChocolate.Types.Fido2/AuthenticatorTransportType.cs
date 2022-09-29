using Fido2NetLib.Objects;

namespace HotChocolate.Types.Fido2;

// ReSharper disable once ClassNeverInstantiated.Global
public class AuthenticatorTransportType : EnumType<AuthenticatorTransport>
{
    protected override void Configure(IEnumTypeDescriptor<AuthenticatorTransport> descriptor)
    {
        descriptor.BindValuesExplicitly()
            .Name(WellKnownObjectTypes.AuthenticatorTransport)
            .Description(ObjectTypeResources.AuthenticatorTransportType_Description);

        descriptor.Value(AuthenticatorTransport.Usb)
            .Description(ObjectTypeResources.AuthenticatorTransportType_Usb_Description);

        descriptor.Value(AuthenticatorTransport.Nfc)
            .Description(ObjectTypeResources.AuthenticatorTransportType_Nfc_Description);

        descriptor.Value(AuthenticatorTransport.Ble)
            .Description(ObjectTypeResources.AuthenticatorTransportType_Ble_Description);

        descriptor.Value(AuthenticatorTransport.Internal)
            .Description(ObjectTypeResources.AuthenticatorTransportType_Internal_Description);
    }
}
