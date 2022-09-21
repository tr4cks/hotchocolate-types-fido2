using Fido2NetLib.Objects;
using HotChocolate.Types.Fido2.Scalars;

namespace HotChocolate.Types.Fido2;

public class AttestationVerificationSuccessType : ObjectType<AttestationVerificationSuccess>
{
    protected override void Configure(IObjectTypeDescriptor<AttestationVerificationSuccess> descriptor)
    {
        // todo: documentation

        descriptor.BindFieldsExplicitly()
            .Name(WellKnownObjectTypes.AttestationVerificationSuccess);

        // AssertionVerificationResult
        descriptor.Field(f => f.CredentialId)
            .Type<NonNullType<Base64Type>>();

        descriptor.Field(f => f.Counter)
            .Type<NonNullType<UnsignedIntType>>();

        // AttestationVerificationSuccess
        descriptor.Field(f => f.PublicKey)
            .Type<NonNullType<Base64Type>>();

        descriptor.Field(f => f.User)
            .Type<NonNullType<PublicKeyCredentialUserEntityType>>();

        descriptor.Field(f => f.CredType)
            .Type<NonNullType<StringType>>();

        descriptor.Field(f => f.Aaguid)
            .Type<NonNullType<UuidType>>();
    }
}
