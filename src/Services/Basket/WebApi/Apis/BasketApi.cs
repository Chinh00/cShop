using Application.UseCases.Commands;
using Application.UseCases.Queries;
using Asp.Versioning.Builder;
using cShop.Contracts.Services.Basket;
using MediatR;

namespace WebApi.Apis;

public static class BasketApi
{
    private const string BaseUrl = "/api/v{version:apiVersion}/basket";
    
    public static IVersionedEndpointRouteBuilder MapBasketApiV1(this IVersionedEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup(BaseUrl).HasApiVersion(1);
        group.MapGet("/{userId:guid}",
            async (ISender sender, Guid userId) => await sender.Send(new GetBasketQuery(userId)));
        group.MapPost(string.Empty, async (ISender sender, CreateBasketCommand command, CancellationToken cancellationToken) => await sender.Send(command, cancellationToken));
        group.MapPost("add-item", async (ISender sender, AddBasketItemCommand item, CancellationToken cancellation) => await sender.Send(item, cancellation));
        group.MapDelete("/{productId:guid}",
            async (ISender sender, Guid userId, Guid productId) =>
                await sender.Send(new RemoveBasketItemCommand(userId, productId)));
        
        
        return endpoints;
    }
}