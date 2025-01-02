using cShop.Core.Domain;
using Domain.Aggregate;

namespace Domain.Entities;

public class CatalogType : EntityBase
{
    public string Name { get; set; }
    public string Description { get; set; }
    public virtual ICollection<CatalogItem> CatalogItems { get; set; }
}