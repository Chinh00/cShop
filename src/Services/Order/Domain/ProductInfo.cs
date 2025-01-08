using cShop.Core.Domain;

namespace Domain;

public class ProductInfo : EntityBase
{
    public string ProductName { get; set; }
    public decimal ProductPrice { get; set; }
    
    
}