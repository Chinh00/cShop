using cShop.Core.Domain;

namespace cShop.Contracts.Services.Order;

public interface MakeOrderStockValidateIntegrationEvent : IIntegrationEvent
{
    public Guid OrderId { get; set; }
    public Guid UserId { get; set; }
    
    public List<CatalogConfirm> OrderItems { get; set; }
    
}

public record CatalogConfirm(Guid CatalogId, int Quantity);


