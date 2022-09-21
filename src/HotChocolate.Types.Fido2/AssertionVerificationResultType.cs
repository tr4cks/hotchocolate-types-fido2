using Fido2NetLib.Objects;
using HotChocolate.Types.Fido2.Scalars;

namespace HotChocolate.Types.Fido2;

public class AssertionVerificationResultType : ObjectType<AssertionVerificationResult>
{
    protected override void Configure(IObjectTypeDescriptor<AssertionVerificationResult> descriptor)
    {
        // todo: documentation

        descriptor.BindFieldsExplicitly()
            .Name(WellKnownObjectTypes.AssertionVerificationResult);

        descriptor.Field(f => f.CredentialId)
            .Type<NonNullType<Base64Type>>();

        descriptor.Field(f => f.Counter)
            .Type<NonNullType<UnsignedIntType>>();
    }
}
