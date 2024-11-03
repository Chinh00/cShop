using cShop.Core.Domain;

namespace Domain.Entities;

public class Category : EntityBase
{
    public string Name { get; set; }
    public string Description { get; set; }
}