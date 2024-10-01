using cShop.Core.Domain;

namespace Projection;

public class BasketProjection : ProjectionBase
{
    public Guid UserId { get; set; }
}