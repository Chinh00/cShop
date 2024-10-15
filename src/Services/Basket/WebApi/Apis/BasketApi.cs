using Application.UseCases.Commands;
using Asp.Versioning.Builder;
using cShop.Contracts.Services.Basket;
using MediatR;

namespace WebApi.Apis;

public static class BasketApi
{
    private const string BaseUrl = "/api/v{version:apiVersion}/basket";
    
    public static IVersionedEndpointRouteBuilder MapBasketApiV1(this IVersionedEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup(BaseUrl).HasApiVersion(1).RequireAuthorization();

        group.MapPost(string.Empty, async (ISender sender, CancellationToken cancellationToken) => await sender.Send(new CreateBasketCommand(), cancellationToken));
        group.MapPost("add-item", async (ISender sender, AddBasketItemCommand item, CancellationToken cancellation) => await sender.Send(item, cancellation));
        
        
        
        return endpoints;
    }
}