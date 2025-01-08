using Application.UseCases.Commands;
using Application.UseCases.Queries;
using Asp.Versioning.Builder;
using MediatR;

namespace WebApi.Apis;

public static class BasketApi
{
    private const string BaseUrl = "/api/v{version:apiVersion}/baskets";
    
    public static IVersionedEndpointRouteBuilder MapBasketApiV1(this IVersionedEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup(BaseUrl).HasApiVersion(1).RequireAuthorization();
        group.MapGet(string.Empty,
            async (ISender sender) => await sender.Send(new GetBasketByUserQuery()));
        group.MapPost("/create",
            async (ISender sender, CancellationToken cancellationToken) =>
                await sender.Send(new CreateBasketCommand(), cancellationToken));
        group.MapPost("/add", async (ISender sender, AddBasketItemCommand item, CancellationToken cancellation) => await sender.Send(item, cancellation));
        group.MapDelete("/{productId:guid}",
            async (ISender sender, Guid productId) =>
                await sender.Send(new RemoveBasketItemCommand(productId)));
        
        
        return endpoints;
    }
}