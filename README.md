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
    ⚠️ FIRST RELEASE AND DOCUMENTATION COMING SOON ⚠️
  </p>
</div>

<br />

[![Nuget](https://img.shields.io/nuget/v/HotChocolate.Extensions.Types.Fido2)](https://www.nuget.org/packages/HotChocolate.Extensions.Types.Fido2)
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

⚠️ Only available in preview version for the moment ⚠️

```shell
dotnet add package HotChocolate.Extensions.Types.Fido2
```

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- USAGE EXAMPLES -->
## Usage

Configure services to integrate all types defined by the `fido2-net-lib` library.

```csharp
builder.Services
    .AddGraphQLServer()
    .AddFido2();
```

You can then use all types defined in `fido2-net-lib` library with the difference that
error handling must be done within GraphQL mutations because it is not included in types
as it is the case in `fido2-net-lib` library.

<br />

⚠️ **NON-COMPLETE DOCUMENTATION** ⚠️

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- LICENSE -->
## License

Distributed under the MIT License. See `LICENSE.txt` for more information.

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- CREDITS -->
## Credits

* [Fingerprint icons created by Blaze150 - Flaticon](https://www.flaticon.com/free-icons/fingerprint)

<p align="right">(<a href="#readme-top">back to top</a>)</p>
