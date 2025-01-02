using Application.UseCases.Commands;
using Application.UseCases.Queries;
using Asp.Versioning.Builder;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Apis;

public static class OrderApi
{
    private const string BaseUrl = "/api/v{version:apiVersion}/orders";
    public static IVersionedEndpointRouteBuilder MapOrderV1Api(this IVersionedEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup(BaseUrl).HasApiVersion(1);

        group.MapPost(string.Empty, async ([FromServices] ISender sender,[FromBody] CreateOrderCommand createOrder, CancellationToken cancellationToken) => await sender.Send(createOrder, cancellationToken));
        group.MapGet("/{id:guid}",
            async (ISender sender, Guid id) => await sender.Send(new GetOrderStateMachineQuery(id)));

        return endpoints;
    }
}