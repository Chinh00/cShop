using cShop.Core.Domain;

namespace cShop.Contracts.Services.Order;

public class IntegrationEvents
{
    public record PaymentProcess() : Message, IIntegrationEvent;
}