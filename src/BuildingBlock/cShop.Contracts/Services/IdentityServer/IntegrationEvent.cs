using cShop.Core.Domain;
using MediatR;

namespace cShop.Contracts.Services.IdentityServer;

public class IntegrationEvent
{
    public record CustomerCreatedIntegration(Guid CustomerId) : IIntegrationEvent;
}