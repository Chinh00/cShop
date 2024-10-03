using cShop.Core.Domain;

namespace cShop.Contracts.Services.Order;

public class IntegrationEvents
{
    public record PaymentProcess(Guid OrderId) : Message, IIntegrationEvent;
    public record PaymentProcessSuccess(Guid OrderId) : Message, IIntegrationEvent;
    public record PaymentProcessFail(Guid OrderId) : Message, IIntegrationEvent;
}