using cShop.Core.Domain;

namespace Domain.Entities;

public class CatalogBrand : EntityBase
{
    public string BrandName { get; set; }
    public string Description { get; set; }
}