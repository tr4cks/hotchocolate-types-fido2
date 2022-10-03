using Fido2NetLib;
using Fido2NetLib.Objects;
using HotChocolate.Types.Fido2.Scalars;

namespace HotChocolate.Types.Fido2;

// ReSharper disable once ClassNeverInstantiated.Global
public class PublicKeyCredentialCreationOptionsType : ObjectType<CredentialCreateOptions>
{
    protected override void Configure(IObjectTypeDescriptor<CredentialCreateOptions> descriptor)
    {
        descriptor.BindFieldsExplicitly()
            .Name(WellKnownObjectTypes.PublicKeyCredentialCreationOptions)
            .Description(ObjectTypeResources.PublicKeyCredentialCreationOptionsType_Description);

        descriptor.Field(f => f.Rp)
            .Type<NonNullType<PublicKeyCredentialRpEntityType>>()
            .Description(ObjectTypeResources.PublicKeyCredentialCreationOptionsType_Rp_Description);

        descriptor.Field(f => f.User)
            .Type<NonNullType<PublicKeyCredentialUserEntityType>>()
            .Description(ObjectTypeResources.PublicKeyCredentialCreationOptionsType_User_Description);

        descriptor.Field(f => f.Challenge)
            .Type<NonNullType<Base64Type>>()
            .Description(ObjectTypeResources.PublicKeyCredentialCreationOptionsType_Challenge_Description);

        descriptor.Field(f => f.PubKeyCredParams)
            .Type<NonNullType<ListType<NonNullType<PublicKeyCredentialParametersType>>>>()
            .Description(ObjectTypeResources.PublicKeyCredentialCreationOptionsType_PubKeyCredParams_Description);

        descriptor.Field(f => f.Timeout)
            .Type<UnsignedLongType>()
            .Description(ObjectTypeResources.PublicKeyCredentialCreationOptionsType_Timeout_Description);

        descriptor.Field(f => f.ExcludeCredentials)
            .Type<ListType<NonNullType<PublicKeyCredentialDescriptorType>>>()
            .Description(ObjectTypeResources.PublicKeyCredentialCreationOptionsType_ExcludeCredentials_Description);

        descriptor.Field(f => f.AuthenticatorSelection)
            .Type<AuthenticatorSelectionCriteriaType>()
            .Description(ObjectTypeResources.PublicKeyCredentialCreationOptionsType_AuthenticatorSelection_Description);

        descriptor.Field(f => f.Attestation)
            .Type<EnumMemberType<AttestationConveyancePreference>>()
            .Description(ObjectTypeResources.PublicKeyCredentialCreationOptionsType_Attestation_Description);

        descriptor.Field(f => f.Extensions)
             .Type<AuthenticationExtensionsClientInputsType>()
             .Description(ObjectTypeResources.PublicKeyCredentialCreationOptionsType_Extensions_Description);
    }
}

// ReSharper disable once ClassNeverInstantiated.Global
public class PublicKeyCredentialRpEntityType : ObjectType<PublicKeyCredentialRpEntity>
{
    protected override void Configure(IObjectTypeDescriptor<PublicKeyCredentialRpEntity> descriptor)
    {
        descriptor.BindFieldsExplicitly()
            .Name(WellKnownObjectTypes.PublicKeyCredentialRpEntity)
            .Description(ObjectTypeResources.PublicKeyCredentialRpEntityType_Description);

        descriptor.Field(f => f.Id)
            .Type<StringType>()
            .Description(ObjectTypeResources.PublicKeyCredentialRpEntityType_Id_Description);

        descriptor.Field(f => f.Name)
            .Type<NonNullType<StringType>>()
            .Description(ObjectTypeResources.PublicKeyCredentialRpEntityType_Name_Description);
    }
}

// ReSharper disable once ClassNeverInstantiated.Global
public class PublicKeyCredentialParametersType : ObjectType<PubKeyCredParam>
{
    protected override void Configure(IObjectTypeDescriptor<PubKeyCredParam> descriptor)
    {
        descriptor.BindFieldsExplicitly()
            .Name(WellKnownObjectTypes.PublicKeyCredentialParameters)
            .Description(ObjectTypeResources.PublicKeyCredentialParametersType_Description);

        descriptor.Field(f => f.Type)
            .Type<NonNullType<EnumMemberType<PublicKeyCredentialType>>>()
            .Description(ObjectTypeResources.PublicKeyCredentialParametersType_Type_Description);

        descriptor.Field(f => f.Alg)
            .Type<NonNullType<LongType>>()
            .Resolve(context => (long) context.Parent<PubKeyCredParam>().Alg)
            .Description(ObjectTypeResources.PublicKeyCredentialParametersType_Alg_Description);
    }
}

// ReSharper disable once ClassNeverInstantiated.Global
public class AuthenticatorSelectionCriteriaType : ObjectType<AuthenticatorSelection>
{
    protected override void Configure(IObjectTypeDescriptor<AuthenticatorSelection> descriptor)
    {
        descriptor.BindFieldsExplicitly()
            .Name(WellKnownObjectTypes.AuthenticatorSelectionCriteria)
            .Description(ObjectTypeResources.AuthenticatorSelectionCriteriaType_Description);

        descriptor.Field(f => f.AuthenticatorAttachment)
            .Type<EnumMemberType<AuthenticatorAttachment>>()
            .Description(ObjectTypeResources.AuthenticatorSelectionCriteriaType_AuthenticatorAttachment_Description);

        // ResidentKey is missing

        descriptor.Field(f => f.RequireResidentKey)
            .Type<BooleanType>()
            .Description(ObjectTypeResources.AuthenticatorSelectionCriteriaType_RequireResidentKey_Description);

        descriptor.Field(f => f.UserVerification)
            .Type<EnumMemberType<UserVerificationRequirement>>()
            .Description(ObjectTypeResources.AuthenticatorSelectionCriteriaType_UserVerification_Description);
    }
}
