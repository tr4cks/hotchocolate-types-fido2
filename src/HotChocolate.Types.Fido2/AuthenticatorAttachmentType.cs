using Fido2NetLib.Objects;

namespace HotChocolate.Types.Fido2;

// ReSharper disable once ClassNeverInstantiated.Global
public class AuthenticatorAttachmentType : EnumType<AuthenticatorAttachment>
{
    protected override void Configure(
        IEnumTypeDescriptor<AuthenticatorAttachment> descriptor)
    {
        descriptor.BindValuesExplicitly()
            .Name(WellKnownObjectTypes.AuthenticatorAttachment)
            .Description(ObjectTypeResources.AuthenticatorAttachmentType_Description);

        descriptor.Value(AuthenticatorAttachment.Platform)
            .Description(ObjectTypeResources
                .AuthenticatorAttachmentType_Platform_Description);

        descriptor.Value(AuthenticatorAttachment.CrossPlatform)
            .Description(ObjectTypeResources
                .AuthenticatorAttachmentType_CrossPlatform_Description);
    }
}
