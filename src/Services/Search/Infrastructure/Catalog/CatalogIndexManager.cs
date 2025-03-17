using Core;
using Infrastructure.Constants;

namespace Infrastructure.Catalog;

public class CatalogIndexManager(IElasticManager elasticManager) : ICatalogIndexManager
{
    public async Task AddOrUpdateAsync(CatalogIndexModel model)
    {
        await elasticManager.AddOrUpdateAsync<CatalogIndexModel, Guid>(IndexConstants.CatalogIndex, model);
    }
}