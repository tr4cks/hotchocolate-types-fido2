using System.Text;
using Fido2NetLib;
using Fido2NetLib.Objects;
using HotChocolate.Execution;
using Microsoft.Extensions.DependencyInjection;

namespace HotChocolate.Types.Fido2.UnitTests;

public class MakeCredentialOptionsTests
{
    private const string MakeCredentialOptionsOutputQuery = @"
        mutation {
            makeCredentialOptionsOutput {
                rp {
                    id
                    name
                }
                user {
                    id
                    displayName
                    name
                }
                challenge
                pubKeyCredParams {
                    type
                    alg
                }
                timeout
                excludeCredentials {
                    type
                    id
                    transports
                }
                authenticatorSelection {
                    authenticatorAttachment
                    requireResidentKey
                    userVerification
                }
                attestation
                extensions
            }
        }
    ";

    [Fact]
    public async Task MakeCredentialOptionsOutputTest()
    {
        var executor = await RequestExecutor.GetAsync(
            setupRequestExecutorBuilderAction: builder =>
            {
                builder.ModifyOptions(options =>
                {
                    options.StrictValidation = false;
                });
                builder.AddMutationType<MutationType>();
            });
        var result = await executor.ExecuteAsync(MakeCredentialOptionsOutputQuery);

        Assert.Null(result.Errors);
    }

    // ReSharper disable once ClassNeverInstantiated.Global
    // ReSharper disable once MemberCanBePrivate.Global
    public class MutationType
    {
        public CredentialCreateOptions MakeCredentialOptionsOutput()
        {
            Fido2Configuration configuration = new()
            {
                ServerDomain = "localhost",
                ServerName = "FIDO2 Test"
            };
            Fido2User user = new()
            {
                Id = Encoding.UTF8.GetBytes("Bruce"),
                Name = "Bruce",
                DisplayName = "Bruce"
            };
            AuthenticatorSelection authenticatorSelection = new()
            {
                AuthenticatorAttachment = AuthenticatorAttachment.Platform,
                RequireResidentKey = false,
                UserVerification = UserVerificationRequirement.Preferred
            };
            List<PublicKeyCredentialDescriptor> excludeCredentials = new()
            {
                new()
                {
                    Type = PublicKeyCredentialType.PublicKey,
                    Id = Encoding.UTF8.GetBytes("Hello World!"),
                    Transports = new[]
                    {
                        AuthenticatorTransport.Ble,
                        AuthenticatorTransport.Internal,
                        AuthenticatorTransport.Nfc,
                        AuthenticatorTransport.Usb
                    }
                }
            };
            AuthenticationExtensionsClientInputs extensions = new()
            {
                Example = "Hello World!",
                AppID = "cedf4be0-6340-416c-8a9e-1dc2f8d0357c",
                AuthenticatorSelection = new[]
                {
                    new byte[] { 1, 2, 3, 4, 5 },
                    new byte[] { 6, 7, 8, 9, 10 }
                },
                Extensions = true,
                UserVerificationMethod = true
            };
            return CredentialCreateOptions.Create(
                configuration,
                Encoding.UTF8.GetBytes("Hello World!"),
                user,
                authenticatorSelection,
                AttestationConveyancePreference.Direct,
                excludeCredentials,
                extensions
                );
        }
    }
}
