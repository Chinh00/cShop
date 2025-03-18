using Core.IndexModels;

namespace Infrastructure.Catalog;

public class CatalogIndexModel : ElasticEntity<Guid>
{
    public string CatalogName { get; set; }
    public float Price { get; set; }
    public string Description { get; set; }
    public Guid CatalogTypeId { get; set; }
    public Guid CatalogBrandId { get; set; }
    public string CatalogTypeName { get; set; }
    public string CatalogBrandName { get; set; }
    public List<string> Pictures { get; set; }
}