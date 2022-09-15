using System.Text.Json;
using Fido2NetLib;
using Fido2NetLib.Objects;
using HotChocolate.Types.Fido2.Extensions;
using HotChocolate.Types.Fido2.Scalars;

namespace HotChocolate.Types.Fido2;

/// <see href="https://w3c.github.io/webauthn/#dictdef-publickeycredentialcreationoptions"/>
public class PublicKeyCredentialCreationOptionsType : ObjectType<CredentialCreateOptions>
{
    protected override void Configure(IObjectTypeDescriptor<CredentialCreateOptions> descriptor)
    {
        descriptor.BindFieldsExplicitly()
            .Name(nameof(PublicKeyCredentialCreationOptionsType)[..^"Type".Length])
            .Description("[Documentation](https://w3c.github.io/webauthn/#dictdef-publickeycredentialcreationoptions)");

        descriptor.Field(f => f.Rp)
            .Type<NonNullType<PublicKeyCredentialRpEntityType>>()
            .Description("[Documentation](https://w3c.github.io/webauthn/#dom-publickeycredentialcreationoptions-rp)");

        descriptor.Field(f => f.User)
            .Type<NonNullType<PublicKeyCredentialUserEntityType>>()
            .Description("[Documentation](https://w3c.github.io/webauthn/#dom-publickeycredentialcreationoptions-user)");

        descriptor.Field(f => f.Challenge)
            .Type<NonNullType<Base64Type>>()
            .Description("[Documentation](https://w3c.github.io/webauthn/#dom-publickeycredentialcreationoptions-challenge)");

        descriptor.Field(f => f.PubKeyCredParams)
            .Type<NonNullType<ListType<NonNullType<PublicKeyCredentialParametersType>>>>()
            .Description("[Documentation](https://w3c.github.io/webauthn/#dom-publickeycredentialcreationoptions-pubkeycredparams)");

        descriptor.Field(f => f.Timeout)
            .Type<UnsignedLongType>()
            .Description("[Documentation](https://w3c.github.io/webauthn/#dom-publickeycredentialcreationoptions-timeout)");

        descriptor.Field(f => f.ExcludeCredentials)
            .Type<ListType<NonNullType<PublicKeyCredentialDescriptorType>>>()
            .Description("[Documentation](https://w3c.github.io/webauthn/#dom-publickeycredentialcreationoptions-excludecredentials)");

        descriptor.Field(f => f.AuthenticatorSelection)
            .Type<AuthenticatorSelectionCriteriaType>()
            .Description("[Documentation](https://w3c.github.io/webauthn/#dom-publickeycredentialcreationoptions-authenticatorselection)");

        descriptor.Field(f => f.Attestation)
            .Type<EnumMemberType<AttestationConveyancePreference>>()
            .Description("[Documentation](https://w3c.github.io/webauthn/#dom-publickeycredentialcreationoptions-attestation)");

        // todo
        descriptor.Field(f => f.Extensions)
            .Type<StringType>()
            .Resolve(context => JsonSerializer.Serialize(context.Parent<CredentialCreateOptions>().Extensions))
            .Description("[Documentation](https://w3c.github.io/webauthn/#dom-publickeycredentialcreationoptions-extensions)");

        // descriptor.Field(f => f.Extensions)
        //     .Type<AuthenticationExtensionsClientInputsType>()
        //     .Description("[Documentation](https://w3c.github.io/webauthn/#dom-publickeycredentialcreationoptions-extensions)");
    }
}

public class PublicKeyCredentialRpEntityType : ObjectType<PublicKeyCredentialRpEntity>
{
    protected override void Configure(IObjectTypeDescriptor<PublicKeyCredentialRpEntity> descriptor)
    {
        descriptor.BindFieldsExplicitly()
            .Name(nameof(PublicKeyCredentialRpEntityType)[..^"Type".Length])
            .Description("[Documentation](https://w3c.github.io/webauthn/#dictdef-publickeycredentialrpentity)");

        descriptor.Field(f => f.Id)
            .Type<StringType>()
            .Description("[Documentation](https://w3c.github.io/webauthn/#dom-publickeycredentialrpentity-id)");

        descriptor.Field(f => f.Name)
            .Type<NonNullType<StringType>>()
            .Description("[Documentation](https://w3c.github.io/webauthn/#dom-publickeycredentialentity-name)");
    }
}

public class PublicKeyCredentialUserEntityType : ObjectType<Fido2User>
{
    protected override void Configure(IObjectTypeDescriptor<Fido2User> descriptor)
    {
        descriptor.BindFieldsExplicitly()
            .Name(nameof(PublicKeyCredentialUserEntityType)[..^"Type".Length])
            .Description("[Documentation](https://w3c.github.io/webauthn/#dictdef-publickeycredentialuserentity)");

        descriptor.Field(f => f.Id)
            .Type<NonNullType<Base64Type>>()
            .Description("[Documentation](https://w3c.github.io/webauthn/#dom-publickeycredentialuserentity-id)");

        descriptor.Field(f => f.DisplayName)
            .Type<NonNullType<StringType>>()
            .Description("[Documentation](https://w3c.github.io/webauthn/#dom-publickeycredentialuserentity-displayname)");

        descriptor.Field(f => f.Name)
            .Type<NonNullType<StringType>>()
            .Description("[Documentation](https://w3c.github.io/webauthn/#dom-publickeycredentialentity-name)");
    }
}

public class PublicKeyCredentialParametersType : ObjectType<PubKeyCredParam>
{
    protected override void Configure(IObjectTypeDescriptor<PubKeyCredParam> descriptor)
    {
        descriptor.BindFieldsExplicitly()
            .Name(nameof(PublicKeyCredentialParametersType)[..^"Type".Length])
            .Description("[Documentation](https://w3c.github.io/webauthn/#dictdef-publickeycredentialparameters)");

        descriptor.Field(f => f.Type)
            .Type<NonNullType<EnumMemberType<PublicKeyCredentialType>>>()
            .Description("[Documentation](https://w3c.github.io/webauthn/#dom-publickeycredentialparameters-type)");

        descriptor.Field(f => f.Alg)
            .Type<NonNullType<LongType>>()
            .Resolve(context => (long) context.Parent<PubKeyCredParam>().Alg)
            .Description("[Documentation](https://w3c.github.io/webauthn/#dom-publickeycredentialparameters-alg)");
    }
}

public class PublicKeyCredentialDescriptorType : ObjectType<PublicKeyCredentialDescriptor>
{
    protected override void Configure(IObjectTypeDescriptor<PublicKeyCredentialDescriptor> descriptor)
    {
        descriptor.BindFieldsExplicitly()
            .Name(nameof(PublicKeyCredentialDescriptorType)[..^"Type".Length])
            .Description("[Documentation](https://w3c.github.io/webauthn/#dictdef-publickeycredentialdescriptor)");

        // is not consistent with the specification
        descriptor.Field(f => f.Type)
            .Type<EnumMemberType<PublicKeyCredentialType>>()
            .Description("[Documentation](https://w3c.github.io/webauthn/#dom-publickeycredentialdescriptor-type)");

        descriptor.Field(f => f.Id)
            .Type<NonNullType<Base64Type>>()
            .Description("[Documentation](https://w3c.github.io/webauthn/#dom-publickeycredentialdescriptor-id)");

        descriptor.Field(f => f.Transports)
            .Type<ListType<NonNullType<EnumMemberType<AuthenticatorTransport>>>>()
            .Description("[Documentation](https://w3c.github.io/webauthn/#dom-publickeycredentialdescriptor-transports)");
    }
}

public class AuthenticatorSelectionCriteriaType : ObjectType<AuthenticatorSelection>
{
    protected override void Configure(IObjectTypeDescriptor<AuthenticatorSelection> descriptor)
    {
        descriptor.BindFieldsExplicitly()
            .Name(nameof(AuthenticatorSelectionCriteriaType)[..^"Type".Length])
            .Description("[Documentation](https://w3c.github.io/webauthn/#dictdef-authenticatorselectioncriteria)");

        descriptor.Field(f => f.AuthenticatorAttachment)
            .Type<EnumMemberType<AuthenticatorAttachment>>()
            .Description("[Documentation](https://w3c.github.io/webauthn/#dom-authenticatorselectioncriteria-authenticatorattachment)");

        // ResidentKey is missing

        descriptor.Field(f => f.RequireResidentKey)
            .Type<BooleanType>()
            .Description("[Documentation](https://w3c.github.io/webauthn/#dom-authenticatorselectioncriteria-requireresidentkey)");

        descriptor.Field(f => f.UserVerification)
            .Type<EnumMemberType<UserVerificationRequirement>>()
            .Description("[Documentation](https://w3c.github.io/webauthn/#dom-authenticatorselectioncriteria-userverification)");
    }
}

public class AuthenticationExtensionsClientInputsType : ObjectType<AuthenticationExtensionsClientInputs>
{
    protected override void Configure(IObjectTypeDescriptor<AuthenticationExtensionsClientInputs> descriptor)
    {
        descriptor.BindFieldsExplicitly()
            .Name(nameof(AuthenticationExtensionsClientInputsType)[..^"Type".Length])
            .Description("[Documentation](https://w3c.github.io/webauthn/#dictdef-authenticationextensionsclientinputs)");

        // must be renamed on the client side to "example.extension"
        descriptor.Field(f => f.Example)
            .Name("ExampleExtension")
            .Type<AnyType>()
            .Description("This extension allows for passing of conformance tests");

        descriptor.Field(f => f.AppID)
            .Name("appid")
            .Type<StringType>()
            .Description(@"
                This extension allows WebAuthn Relying Parties that have previously registered a credential using the legacy FIDO JavaScript APIs to request an assertion.
                https://www.w3.org/TR/webauthn/#sctn-appid-extension
            ".Dedent().TrimNewLines());

        // todo: convert?
        descriptor.Field(f => f.AuthenticatorSelection)
            .Name("authnSel")
            .Type<ListType<NonNullType<ListType<NonNullType<ByteType>>>>>()
            .Description(@"
                This extension allows a WebAuthn Relying Party to guide the selection of the authenticator that will be leveraged when creating the credential.
                It is intended primarily for Relying Parties that wish to tightly control the experience around credential creation.
                https://www.w3.org/TR/webauthn/#sctn-authenticator-selection-extension
            ".Dedent().TrimNewLines());

        descriptor.Field(f => f.Extensions)
            .Name("exts")
            .Type<BooleanType>()
            .Description(@"
                This extension enables the WebAuthn Relying Party to determine which extensions the authenticator supports.
                https://www.w3.org/TR/webauthn/#sctn-supported-extensions-extension
            ".Dedent().TrimNewLines());

        descriptor.Field(f => f.UserVerificationMethod)
            .Name("uvm")
            .Type<BooleanType>()
            .Description(@"
                This extension enables use of a user verification method.
                https://www.w3.org/TR/webauthn/#sctn-uvm-extension
            ".Dedent().TrimNewLines());
    }
}
