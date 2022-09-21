using System.Text;
using Fido2NetLib;
using HotChocolate.Execution;
using Microsoft.Extensions.DependencyInjection;

namespace HotChocolate.Types.Fido2.UnitTests;

public class MakeCredentialTests
{
    private const string MakeCredentialInputQuery = @"
        mutation MakeCredential($input: PublicKeyCredentialInput!) {
            makeCredentialInput(input: $input)
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
                    {"id", Convert.ToBase64String(Encoding.UTF8.GetBytes("Hello World!"))},
                    {"type", "public-key"},
                    {"rawId", Convert.ToBase64String(Encoding.UTF8.GetBytes("Hello World!"))},
                    {"response", new Dictionary<string, object?>
                    {
                        {"clientDataJSON", Convert.ToBase64String(Encoding.UTF8.GetBytes("Hello World!"))},
                        {"attestationObject", Convert.ToBase64String(Encoding.UTF8.GetBytes("Hello World!"))}
                    }}
                })
                .Create();
        var result = await executor.ExecuteAsync(request);

        Assert.Null(result.Errors);
    }

    // ReSharper disable once ClassNeverInstantiated.Global
    // ReSharper disable once MemberCanBePrivate.Global
    public class MutationType
    {
        [GraphQLType(typeof(AnyType))]
        public object? MakeCredentialInput(AuthenticatorAttestationRawResponse input) =>
            null;
    }
}
