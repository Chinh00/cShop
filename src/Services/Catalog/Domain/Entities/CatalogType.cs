using cShop.Core.Domain;

namespace Domain.Entities;

public class CatalogType : EntityBase
{
    public string Name { get; set; }
    public string Description { get; set; }
}