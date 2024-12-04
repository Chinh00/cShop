using cShop.Core.Domain;

namespace IntegrationEvents;

public interface MakeOrderStockValidateIntegrationEvent : IIntegrationEvent
{
    public Guid OrderId { get; set; }
    
    public List<CatalogConfirm> OrderItems { get; set; }
    
}

public record CatalogConfirm(Guid CatalogId, int Quantity);


