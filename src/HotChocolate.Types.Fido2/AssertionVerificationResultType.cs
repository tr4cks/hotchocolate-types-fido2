using Fido2NetLib.Objects;
using HotChocolate.Types.Fido2.Scalars;

namespace HotChocolate.Types.Fido2;

// ReSharper disable once ClassNeverInstantiated.Global
public class AssertionVerificationResultType : ObjectType<AssertionVerificationResult>
{
    protected override void Configure(
        IObjectTypeDescriptor<AssertionVerificationResult> descriptor)
    {
        descriptor.BindFieldsExplicitly()
            .Name(WellKnownObjectTypes.AssertionVerificationResult)
            .Description(ObjectTypeResources.AssertionVerificationResultType_Description);

        descriptor.Field(f => f.CredentialId)
            .Type<NonNullType<Base64UrlType>>();

        descriptor.Field(f => f.Counter)
            .Type<NonNullType<UnsignedIntType>>();
    }
}
