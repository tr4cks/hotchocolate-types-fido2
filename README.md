<!-- Improved compatibility of back to top link: See: https://github.com/othneildrew/Best-README-Template/pull/73 -->
<a name="readme-top"></a>



<!-- PROJECT LOGO -->
<br />
<div align="center">
  <a href="https://github.com/tr4cks/hotchocolate-types-fido2">
    <img src="assets/logo.png" alt="Logo" width="80" height="80">
  </a>

  <h3 align="center">hotchocolate-types-fido2</h3>

  <p align="center">
    Simple integration of <strong>fido2-net-lib</strong> types into <strong>HotChocolate</strong> 👌
  </p>
</div>

<br />

[![Nuget](https://img.shields.io/nuget/v/HotChocolate.Extensions.Types.Fido2)](https://www.nuget.org/packages/HotChocolate.Extensions.Types.Fido2)
![Tests](https://github.com/tr4cks/hotchocolate-types-fido2/workflows/Tests/badge.svg)
[![codecov](https://codecov.io/gh/tr4cks/hotchocolate-types-fido2/branch/main/graph/badge.svg?token=WHT2JAQM1N)](https://codecov.io/gh/tr4cks/hotchocolate-types-fido2)
[![GitHub](https://img.shields.io/github/license/tr4cks/hotchocolate-types-fido2)](https://github.com/tr4cks/hotchocolate-types-fido2/blob/main/LICENSE)

---



<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li><a href="#about-the-project">About The Project</a></li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
    <li><a href="#usage">Usage</a></li>
    <li>
      <a href="#examples">Examples</a>
      <ul>
        <li><a href="#create-attestation-options">Create Attestation Options</a></li>
        <li><a href="#register-credentials">Register Credentials</a></li>
        <li><a href="#create-assertion-options">Create Assertion Options</a></li>
        <li><a href="#verify-the-assertion-response">Verify The Assertion Response</a></li>
      </ul>
    </li>
    <li><a href="#type-mapping-table">Type Mapping Table</a></li>
    <li><a href="#license">License</a></li>
    <li><a href="#credits">Credits</a></li>
  </ol>
</details>



<!-- ABOUT THE PROJECT -->
## About The Project

Make available the [fido2-net-lib](https://github.com/passwordless-lib/fido2-net-lib)
(WebAuthn) types within the [HotChocolate](https://github.com/ChilliCream/hotchocolate)
(GraphQL) library.



<!-- GETTING STARTED -->
## Getting Started

### Prerequisites

Since version 3 of `fido2-net-lib` library is implemented only for the `.net6` target,
this library also works only with this one.

### Installation

```shell
dotnet add package HotChocolate.Extensions.Types.Fido2
```

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- USAGE EXAMPLES -->
## Usage

Configure services to integrate all types defined by the `fido2-net-lib` library.

```csharp
builder.Services.AddFido2(options =>
{
    // See the configuration example at https://github.com/passwordless-lib/fido2-net-lib/blob/v3.0.0/Demo/Startup.cs#L47
});

builder.Services
    .AddGraphQLServer()
    .AddFido2();
```

You can then use all types defined in `fido2-net-lib` library with the difference that
error handling must be done within GraphQL mutations because it is not included in types
as it is the case in `fido2-net-lib` library.

You can consult the schema via `Banana Cake Pop` to directly see which type you can use
and deduce the relationships using the [table below](#type-mapping-table).

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- EXAMPLES -->
## Examples

Here is the list of the different prototypes needed to implement the protocol.
You can refer to the [examples](https://github.com/passwordless-lib/fido2-net-lib/tree/v3.0.0#examples)
provided in the `fido2-net-lib` library documentation to understand what
each of the following prototypes correspond to. Refer also to their implementation in
the [example](https://github.com/tr4cks/hotchocolate-types-fido2/blob/1.0.0/samples/Fido2Api/Mutation.cs)
provided in the project.

For more details concerning the implementation of controllers allowing the implementation
of [FIDO2](https://fidoalliance.org/fido2/) / [WebAuthn](https://www.w3.org/TR/webauthn/)
refer to the [example](https://github.com/passwordless-lib/fido2-net-lib/blob/v3.0.0/Demo/Controller.cs)
provided by the `fido2-net-lib` library.

### [Create Attestation Options](https://github.com/passwordless-lib/fido2-net-lib/tree/v3.0.0#create-attestation-options)

```csharp
public CredentialCreateOptions MakeCredentialOptions(
    IFido2 fido2,
    IHttpContextAccessor httpContextAccessor,
    string? username,
    string displayName,
    AttestationConveyancePreference attType,
    AuthenticatorAttachment? authType,
    bool requireResidentKey,
    UserVerificationRequirement userVerification)
{ }
```

### [Register Credentials](https://github.com/passwordless-lib/fido2-net-lib/tree/v3.0.0#register-credentials)

```csharp
public async Task<AttestationVerificationSuccess> MakeCredential(
    IFido2 fido2,
    IHttpContextAccessor httpContextAccessor,
    AuthenticatorAttestationRawResponse attestationResponse,
    CancellationToken cancellationToken)
{ }
```

### [Create Assertion Options](https://github.com/passwordless-lib/fido2-net-lib/tree/v3.0.0#create-assertion-options)

```csharp
public AssertionOptions MakeAssertionOptions(
    IResolverContext context,
    IFido2 fido2,
    IHttpContextAccessor httpContextAccessor,
    string? username,
    UserVerificationRequirement userVerification = UserVerificationRequirement.Discouraged)
{ }
```

### [Verify The Assertion Response](https://github.com/passwordless-lib/fido2-net-lib/tree/v3.0.0#verify-the-assertion-response)

```csharp
public async Task<AssertionVerificationResult> MakeAssertion(
    IFido2 fido2,
    IHttpContextAccessor httpContextAccessor,
    AuthenticatorAssertionRawResponse clientResponse,
    CancellationToken cancellationToken)
{ }
```

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- TYPE MAPPING TABLE -->
## Type Mapping Table

| FIDO2 types                                           | HotChocolate types                                | GraphQL types                               | Binding behavior[^binding] |
|-------------------------------------------------------|---------------------------------------------------|---------------------------------------------|----------------------------|
| `AssertionOptions`                                    | `PublicKeyCredentialRequestOptionsType`           | `PublicKeyCredentialRequestOptions`         | Implicit[^implicit]        |
| `AssertionVerificationResult`                         | `AssertionVerificationResultType`                 | `AssertionVerificationResult`               | Implicit[^implicit]        |
| `AttestationConveyancePreference`                     | `AttestationConveyancePreferenceType`             | `AttestationConveyancePreference`           | Implicit[^implicit]        |
| `AttestationConveyancePreference`                     | `EnumMemberType<AttestationConveyancePreference>` | `AttestationConveyancePreferenceStringEnum` | Explicit[^explicit]        |
| `AttestationVerificationSuccess`                      | `AttestationVerificationSuccessType`              | `AttestationVerificationSuccess`            | Implicit[^implicit]        |
| `AuthenticationExtensionsClientInputs`                | `AuthenticationExtensionsClientInputsType`        | `AuthenticationExtensionsClientInputs`      | Explicit[^explicit]        |
| `AuthenticationExtensionsClientOutputs`               | `AuthenticationExtensionsClientOutputsType`       | `AuthenticationExtensionsClientOutputs`     | Explicit[^explicit]        |
| `AuthenticatorAssertionRawResponse`                   | `PublicKeyCredentialAssertionInputType`           | `PublicKeyCredentialAssertionInput`         | Implicit[^implicit]        |
| `AuthenticatorAssertionRawResponse.AssertionResponse` | `AuthenticatorAssertionResponseInputType`         | `AuthenticatorAssertionResponseInput`       | Explicit[^explicit]        |
| `AuthenticatorAttachment`                             | `AuthenticatorAttachmentType`                     | `AuthenticatorAttachment`                   | Implicit[^implicit]        |
| `AuthenticatorAttachment`                             | `EnumMemberType<AuthenticatorAttachment>`         | `AuthenticatorAttachmentStringEnum`         | Explicit[^explicit]        |
| `AuthenticatorAttestationRawResponse`                 | `PublicKeyCredentialAttestationInputType`         | `PublicKeyCredentialAttestationInput`       | Implicit[^implicit]        |
| `AuthenticatorAttestationRawResponse.ResponseData`    | `AuthenticatorAttestationResponseInputType`       | `AuthenticatorAttestationResponseInput`     | Explicit[^explicit]        |
| `AuthenticatorSelection`                              | `AuthenticatorSelectionCriteriaType`              | `AuthenticatorSelectionCriteria`            | Explicit[^explicit]        |
| `AuthenticatorTransport`                              | `AuthenticatorTransportType`                      | `AuthenticatorTransport`                    | Implicit[^implicit]        |
| `AuthenticatorTransport`                              | `EnumMemberType<AuthenticatorTransport>`          | `AuthenticatorTransportStringEnum`          | Explicit[^explicit]        |
| `CredentialCreateOptions`                             | `PublicKeyCredentialCreationOptionsType`          | `PublicKeyCredentialCreationOptions`        | Implicit[^implicit]        |
| `Fido2User`                                           | `PublicKeyCredentialUserEntityType`               | `PublicKeyCredentialUserEntity`             | Explicit[^explicit]        |
| `PubKeyCredParam`                                     | `PublicKeyCredentialParametersType`               | `PublicKeyCredentialParameters`             | Explicit[^explicit]        |
| `PublicKeyCredentialDescriptor`                       | `PublicKeyCredentialDescriptorType`               | `PublicKeyCredentialDescriptor`             | Explicit[^explicit]        |
| `PublicKeyCredentialRpEntity`                         | `PublicKeyCredentialRpEntityType`                 | `PublicKeyCredentialRpEntity`               | Explicit[^explicit]        |
| `PublicKeyCredentialType`                             | `PublicKeyCredentialTypeType`                     | `PublicKeyCredentialType`                   | Implicit[^implicit]        |
| `PublicKeyCredentialType`                             | `EnumMemberType<PublicKeyCredentialType>`         | `PublicKeyCredentialTypeStringEnum`         | Explicit[^explicit]        |
| `UserVerificationRequirement`                         | `UserVerificationRequirementType`                 | `UserVerificationRequirement`               | Implicit[^implicit]        |
| `UserVerificationRequirement`                         | `EnumMemberType<UserVerificationRequirement>`     | `UserVerificationRequirementStringEnum`     | Explicit[^explicit]        |

[^binding]: Defines the type system binding behavior of `HotChocolate`.
[^implicit]: Implicitly bind type system members.
[^explicit]: Type system members need to be explicitly bound.



<!-- LICENSE -->
## License

Distributed under the MIT License. See `LICENSE.txt` for more information.

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- CREDITS -->
## Credits

* [Fingerprint icons created by Blaze150 - Flaticon](https://www.flaticon.com/free-icons/fingerprint)

<p align="right">(<a href="#readme-top">back to top</a>)</p>
