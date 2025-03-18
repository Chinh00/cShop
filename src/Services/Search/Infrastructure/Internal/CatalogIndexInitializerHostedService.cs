using Core;
using Infrastructure.Catalog;
using Infrastructure.Constants;

namespace Infrastructure.Internal;

public class CatalogIndexInitializerHostedService(IServiceScopeFactory scopeFactory) : IHostedService
{

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await using var scope = scopeFactory.CreateAsyncScope();
        var elasticManager = scope.ServiceProvider.GetRequiredService<IElasticManager>();
        await elasticManager.CreateIndexAsync<CatalogIndexModel, Guid>(IndexConstants.CatalogIndex);
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}