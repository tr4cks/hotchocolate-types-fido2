using Fido2NetLib.Objects;
using HotChocolate.Types.Fido2.Scalars;

namespace HotChocolate.Types.Fido2;

// ReSharper disable once ClassNeverInstantiated.Global
public class AttestationVerificationSuccessType : ObjectType<AttestationVerificationSuccess>
{
    protected override void Configure(IObjectTypeDescriptor<AttestationVerificationSuccess> descriptor)
    {
        descriptor.BindFieldsExplicitly()
            .Name(WellKnownObjectTypes.AttestationVerificationSuccess)
            .Description(ObjectTypeResources.AttestationVerificationSuccessType_Description);

        // AssertionVerificationResult
        descriptor.Field(f => f.CredentialId)
            .Type<NonNullType<Base64UrlType>>();

        descriptor.Field(f => f.Counter)
            .Type<NonNullType<UnsignedIntType>>();

        // AttestationVerificationSuccess
        descriptor.Field(f => f.PublicKey)
            .Type<NonNullType<Base64UrlType>>();

        descriptor.Field(f => f.User)
            .Type<NonNullType<PublicKeyCredentialUserEntityType>>();

        descriptor.Field(f => f.CredType)
            .Type<NonNullType<StringType>>();

        descriptor.Field(f => f.Aaguid)
            .Type<NonNullType<UuidType>>();
    }
}
