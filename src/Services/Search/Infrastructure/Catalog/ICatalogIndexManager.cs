namespace Infrastructure.Catalog;

public interface ICatalogIndexManager
{
    Task AddOrUpdateAsync(CatalogIndexModel model);
}