using System.Text;
using Fido2NetLib;
using Fido2NetLib.Objects;
using HotChocolate.Execution;
using Microsoft.Extensions.DependencyInjection;

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
                {"id", Convert.ToBase64String(Encoding.UTF8.GetBytes("Hello World!"))},
                {"type", "public-key"},
                {"rawId", Convert.ToBase64String(Encoding.UTF8.GetBytes("Hello World!"))},
                {"response", new Dictionary<string, object?>
                {
                    {"clientDataJSON", Convert.ToBase64String(Encoding.UTF8.GetBytes("Hello World!"))},
                    {"authenticatorData", Convert.ToBase64String(Encoding.UTF8.GetBytes("Hello World!"))},
                    {"signature", Convert.ToBase64String(Encoding.UTF8.GetBytes("Hello World!"))},
                    {"userHandle", Convert.ToBase64String(Encoding.UTF8.GetBytes("Hello World!"))}
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
