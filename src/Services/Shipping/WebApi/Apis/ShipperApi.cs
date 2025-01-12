using Application.UseCases.Commands;
using Asp.Versioning.Builder;
using MediatR;

namespace WebApi.Apis;

public static class ShipperApi
{
    const string BaseUrl = "/api/v{version:apiVersion}/shippers";

    public static IVersionedEndpointRouteBuilder MapShipperApi(this IVersionedEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup(BaseUrl).HasApiVersion(1).RequireAuthorization();

        group.MapPost("/pick", async (ISender sender, PickShipment command) => await sender.Send(command));
        group.MapPost("/delivery", async (ISender sender, DeliveryShipment command) => await sender.Send(command));

        
        return endpoints;
    }
}