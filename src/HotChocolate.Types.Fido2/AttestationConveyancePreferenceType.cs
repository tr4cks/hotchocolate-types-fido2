using Fido2NetLib.Objects;

namespace HotChocolate.Types.Fido2;

// ReSharper disable once ClassNeverInstantiated.Global
public class AttestationConveyancePreferenceType : EnumType<AttestationConveyancePreference>
{
    protected override void Configure(IEnumTypeDescriptor<AttestationConveyancePreference> descriptor)
    {
        descriptor.BindValuesExplicitly()
            .Name(WellKnownObjectTypes.AttestationConveyancePreference)
            .Description(ObjectTypeResources.AttestationConveyancePreferenceType_Description);

        descriptor.Value(AttestationConveyancePreference.None)
            .Description(ObjectTypeResources.AttestationConveyancePreferenceType_None_Description);

        descriptor.Value(AttestationConveyancePreference.Indirect)
            .Description(ObjectTypeResources.AttestationConveyancePreferenceType_Indirect_Description);

        descriptor.Value(AttestationConveyancePreference.Direct)
            .Description(ObjectTypeResources.AttestationConveyancePreferenceType_Direct_Description);
    }
}
