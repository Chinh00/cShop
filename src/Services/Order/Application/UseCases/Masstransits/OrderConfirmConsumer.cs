using cShop.Contracts.Services.Order;
using MassTransit;

namespace Application.UseCases.Masstransits;

public class OrderConfirmConsumer : IConsumer<OrderConfirmed>
{
    public async Task Consume(ConsumeContext<OrderConfirmed> context)
    {
        
    }
}