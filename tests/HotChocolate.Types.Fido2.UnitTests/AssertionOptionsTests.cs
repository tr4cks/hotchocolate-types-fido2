using System.Text;
using Fido2NetLib;
using Fido2NetLib.Objects;
using HotChocolate.Execution;
using Microsoft.Extensions.DependencyInjection;

namespace HotChocolate.Types.Fido2.UnitTests;

public class AssertionOptionsTests
{
    private const string AssertionOptionsOutputQuery = @"
        mutation {
            assertionOptionsOutput {
                challenge
                timeout
                rpId
                allowCredentials {
                    type
                    id
                    transports
                }
                userVerification
                extensions
            }
        }
    ";

    [Fact]
    public async Task AssertionOptionsOutputTest()
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
        var result = await executor.ExecuteAsync(AssertionOptionsOutputQuery);

        Assert.Null(result.Errors);
    }

    // ReSharper disable once ClassNeverInstantiated.Global
    // ReSharper disable once MemberCanBePrivate.Global
    public class MutationType
    {
        public AssertionOptions AssertionOptionsOutput()
        {
            Fido2Configuration configuration = new()
            {
                ServerDomain = "localhost",
                ServerName = "FIDO2 Test"
            };
            List<PublicKeyCredentialDescriptor> allowedCredentials = new()
            {
                new()
                {
                    Type = PublicKeyCredentialType.PublicKey,
                    Id = Encoding.UTF8.GetBytes("Hello World!"),
                    Transports = new []
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
                Example = null,
                AppID = "cedf4be0-6340-416c-8a9e-1dc2f8d0357c",
                AuthenticatorSelection = new[]
                {
                    new byte[] { 1, 2, 3, 4, 5 },
                    new byte[] { 6, 7, 8, 9, 10 }
                },
                Extensions = true
            };
            return AssertionOptions.Create(
                configuration, Encoding.UTF8.GetBytes("Hello World!"),
                allowedCredentials,
                UserVerificationRequirement.Preferred,
                extensions);
        }
    }
}
