using System.Text.Json.Serialization;
using cShop.Core.Domain;

namespace Domain;

public class OrderDetail : EntityBase
{
    public Guid OrderId { get; set; }
    [JsonIgnore]
    public virtual Order Order { get; set; }
    public Guid ProductId { get; set; }
    
    public int Quantity { get; set; }
    
    public virtual ProductInfo ProductInfo { get; set; }
}