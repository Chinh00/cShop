using Application.UseCases;
using Asp.Versioning.Builder;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Apis;

public static class OrderApi
{
    private const string BaseUrl = "/api/v{version:apiVersion}/order";
    public static IVersionedEndpointRouteBuilder MapOrderV1Api(this IVersionedEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup(BaseUrl).HasApiVersion(1).RequireAuthorization();

        group.MapPost(string.Empty, async ([FromServices] ISender sender,[FromBody] Commands.CreateOrder createOrder, CancellationToken cancellationToken) => await sender.Send(createOrder, cancellationToken));
        
        
        return endpoints;
    }
}