using Fido2NetLib.Objects;

namespace HotChocolate.Types.Fido2;

// ReSharper disable once ClassNeverInstantiated.Global
public class UserVerificationRequirementType : EnumType<UserVerificationRequirement>
{
    protected override void Configure(IEnumTypeDescriptor<UserVerificationRequirement> descriptor)
    {
        descriptor.BindValuesExplicitly()
            .Name(WellKnownObjectTypes.UserVerificationRequirement)
            .Description(ObjectTypeResources.UserVerificationRequirementType_Description);

        descriptor.Value(UserVerificationRequirement.Required)
            .Description(ObjectTypeResources.UserVerificationRequirementType_Required_Description);

        descriptor.Value(UserVerificationRequirement.Preferred)
            .Description(ObjectTypeResources.UserVerificationRequirementType_Preferred_Description);

        descriptor.Value(UserVerificationRequirement.Discouraged)
            .Description(ObjectTypeResources.UserVerificationRequirementType_Discouraged_Description);
    }
}
