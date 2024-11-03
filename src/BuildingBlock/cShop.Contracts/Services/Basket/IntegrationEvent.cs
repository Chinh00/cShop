using cShop.Core.Domain;

namespace cShop.Contracts.Services.Basket;

public static class IntegrationEvent
{
    
    public interface BasketCheckoutSuccess : IIntegrationEvent
    {
        public Guid OrderId { get; set; }
    }
    
    public interface BasketCheckoutFail : IIntegrationEvent
    {
        public Guid OrderId { get; set; }
    }
    
    

    
} 