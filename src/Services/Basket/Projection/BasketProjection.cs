using cShop.Core.Domain;

namespace Projection;

public class BasketProjection : ProjectionBase
{
    public Guid UserId { get; set; }
    public ICollection<BasketItem> Items { get; set; } = [];
}


public record BasketItem(Guid ProductId, int Quantity, double Price);