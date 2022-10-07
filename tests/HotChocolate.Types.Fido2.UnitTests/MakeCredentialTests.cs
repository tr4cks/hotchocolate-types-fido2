using System.Text;
using Fido2NetLib;
using Fido2NetLib.Objects;
using HotChocolate.Execution;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace HotChocolate.Types.Fido2.UnitTests;

public class MakeCredentialTests
{
    private const string MakeCredentialInputQuery = @"
        mutation MakeCredential($input: PublicKeyCredentialAttestationInput!) {
            makeCredentialInput(input: $input)
        }
    ";

    private const string MakeCredentialOutputQuery = @"
        mutation {
            makeCredentialOutput {
                credentialId
                counter
                publicKey
                user {
                    id
                    displayName
                    name
                }
                credType
                aaguid
            }
        }
    ";

    [Fact]
    public async Task MakeCredentialInputTest()
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
        var request = QueryRequestBuilder.New()
                .SetQuery(MakeCredentialInputQuery)
                .AddVariableValue("input", new Dictionary<string, object?>
                {
                    {"id", Base64UrlEncoder.Encode("Hello World!")},
                    {"type", "public-key"},
                    {"rawId", Base64UrlEncoder.Encode("Hello World!")},
                    {"response", new Dictionary<string, object?>
                    {
                        {"clientDataJSON", Base64UrlEncoder.Encode("Hello World!")},
                        {"attestationObject", Base64UrlEncoder.Encode("Hello World!")}
                    }},
                    {"extensions", new Dictionary<string, object?>
                    {
                        {"example.extension", "Hello World!"},
                        {"appid", true},
                        {"authnSel", true},
                        // ReSharper disable once StringLiteralTypo
                        {"exts", new List<object> {"Hello", "World!"}},
                        {"uvm", new List<List<object>> {new() {1, 2, 3}, new() {4, 5, 6}}},
                        {"ignoredProperty", true}
                    }}
                })
                .Create();
        var result = await executor.ExecuteAsync(request);

        Assert.Null(result.Errors);
    }

    [Fact]
    public async Task MakeCredentialOutputTest()
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
        var result = await executor.ExecuteAsync(MakeCredentialOutputQuery);

        Assert.Null(result.Errors);
    }

    // ReSharper disable once ClassNeverInstantiated.Global
    // ReSharper disable once MemberCanBePrivate.Global
    public class MutationType
    {
        [GraphQLType(typeof(AnyType))]
        public object? MakeCredentialInput(AuthenticatorAttestationRawResponse input) =>
            null;

        public AttestationVerificationSuccess MakeCredentialOutput()
        {
            Fido2User user = new()
            {
                Id = Encoding.UTF8.GetBytes("Bruce"),
                Name = "Bruce",
                DisplayName = "Bruce"
            };
            return new AttestationVerificationSuccess
            {
                CredentialId = Encoding.UTF8.GetBytes("Hello World!"),
                Counter = 42,
                PublicKey = Encoding.UTF8.GetBytes("Hello World!"),
                User = user,
                CredType = "public-key",
                Aaguid = Guid.NewGuid()
            };
        }
    }
}
