using cShop.Contracts.Services.IdentityServer;
using Domain;
using Infrastructure.Data;
using MassTransit;

namespace Bus.Consumer;

public class CustomerCreatedIntegrationConsumer(OrderContext context)
    : IConsumer<IntegrationEvent.CustomerCreatedIntegration>
{
    private readonly OrderContext _context = context;

    public async Task Consume(ConsumeContext<IntegrationEvent.CustomerCreatedIntegration> context)
    {
        await _context.Set<CustomerInfo>().AddAsync(new CustomerInfo()
        {
            Id = context.Message.CustomerId,
            
        });
        await _context.SaveChangesAsync();
    }
}