using System.Text;
using Fido2NetLib;
using Fido2NetLib.Objects;
using HotChocolate.Execution;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace HotChocolate.Types.Fido2.UnitTests;

public class MakeAssertionTests
{
    private const string MakeAssertionInputQuery = @"
        mutation MakeAssertion($input: PublicKeyCredentialAssertionInput!) {
            makeAssertionInput(input: $input)
        }
    ";

    private const string MakeAssertionOutputQuery = @"
        mutation {
            makeAssertionOutput {
                credentialId
                counter
            }
        }
    ";

    [Fact]
    public async Task MakeAssertionInputTest()
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
            .SetQuery(MakeAssertionInputQuery)
            .AddVariableValue("input", new Dictionary<string, object?>
            {
                {"id", Base64UrlEncoder.Encode("Hello World!")},
                {"type", "public-key"},
                {"rawId", Base64UrlEncoder.Encode("Hello World!")},
                {"response", new Dictionary<string, object?>
                {
                    {"clientDataJSON", Base64UrlEncoder.Encode("Hello World!")},
                    {"authenticatorData", Base64UrlEncoder.Encode("Hello World!")},
                    {"signature", Base64UrlEncoder.Encode("Hello World!")},
                    {"userHandle", Base64UrlEncoder.Encode("Hello World!")}
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
    public async Task MakeAssertionOutputTest()
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
        var result = await executor.ExecuteAsync(MakeAssertionOutputQuery);

        Assert.Null(result.Errors);
    }

    // ReSharper disable once ClassNeverInstantiated.Global
    // ReSharper disable once MemberCanBePrivate.Global
    public class MutationType
    {
        [GraphQLType(typeof(AnyType))]
        public object? MakeAssertionInput(AuthenticatorAssertionRawResponse input) =>
            null;

        public AssertionVerificationResult MakeAssertionOutput()
        {
            return new AssertionVerificationResult
            {
                CredentialId = Encoding.UTF8.GetBytes("Hello World!"),
                Counter = 42
            };
        }
    }
}
