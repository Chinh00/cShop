using Application.UseCases;
using Asp.Versioning.Builder;

namespace WebApi.Apis;

public static class OrderApi
{
    private const string BaseUrl = "/api/v{version:apiVersion}/order";
    public static IVersionedEndpointRouteBuilder MapOrderV1Api(this IVersionedEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup(BaseUrl).HasApiVersion(1);

        group.MapPost(string.Empty, async (ISender sender, Commands.CreateOrder createOrder) => await sender.Send(createOrder));
        
        
        
        return endpoints;
    }
}