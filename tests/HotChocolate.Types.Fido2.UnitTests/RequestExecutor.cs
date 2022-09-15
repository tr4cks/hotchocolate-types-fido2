using HotChocolate.Execution;
using HotChocolate.Execution.Configuration;
using HotChocolate.Types.Fido2.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace HotChocolate.Types.Fido2.UnitTests;

internal static class RequestExecutor
{
    public static async Task<IRequestExecutor> GetAsync(
        Action<IServiceCollection>? setupServiceCollectionAction = null,
        Action<IRequestExecutorBuilder>? setupRequestExecutorBuilderAction = null)
    {
        var services = new ServiceCollection();
        setupServiceCollectionAction?.Invoke(services);
        var builder = services
            .AddGraphQL()
            .AddFido2();
        setupRequestExecutorBuilderAction?.Invoke(builder);
        return await builder.BuildRequestExecutorAsync();
    }
}
