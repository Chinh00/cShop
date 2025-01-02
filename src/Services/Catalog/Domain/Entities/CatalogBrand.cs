using cShop.Core.Domain;
using Domain.Aggregate;

namespace Domain.Entities;

public class CatalogBrand : EntityBase
{
    public string BrandName { get; set; }
    public string Description { get; set; }
    public virtual ICollection<CatalogItem> CatalogItems { get; set; }
}