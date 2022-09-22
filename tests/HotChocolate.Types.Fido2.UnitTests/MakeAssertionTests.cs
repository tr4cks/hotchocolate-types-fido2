using System.Text;
using Fido2NetLib.Objects;
using HotChocolate.Execution;
using Microsoft.Extensions.DependencyInjection;

namespace HotChocolate.Types.Fido2.UnitTests;

public class MakeAssertionTests
{
    private const string MakeAssertionOutputQuery = @"
        mutation {
            makeAssertionOutput {
                credentialId
                counter
            }
        }
    ";

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
