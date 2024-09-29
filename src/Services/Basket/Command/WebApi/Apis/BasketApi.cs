using Asp.Versioning.Builder;
using MediatR;

namespace WebApi.Apis;

public static class BasketApi
{
    private const string BaseUrl = "/api/v{version:apiVersion}/basket";
    public static IVersionedEndpointRouteBuilder MapBasketApiV1(this IVersionedEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup(BaseUrl).HasApiVersion(1);

        group.MapPost(string.Empty, async (ISender sender, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(new Commands.CreateBasket(), cancellationToken);
            return Results.Ok(result);
        });
        
        
        
        return endpoints;
    }
}