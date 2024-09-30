using Application.UseCases.Command;
using Asp.Versioning.Builder;
using MediatR;

namespace WebApi.Apis;

public static class BasketApi
{
    private const string BaseUrl = "/api/v{version:apiVersion}/basket";
    
    public static IVersionedEndpointRouteBuilder MapBasketApiV1(this IVersionedEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup(BaseUrl).HasApiVersion(1).RequireAuthorization();

        group.MapPost(string.Empty, async (ISender sender) =>
        {
            var result = await sender.Send(new Commands.CreateBasket());
            return Results.Ok(result);

        });
        
        
        return endpoints;
    }
}