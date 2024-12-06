using Application.UseCases.Command;
using Application.UseCases.Queries;
using Asp.Versioning.Builder;
using cShop.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Apis;

public static class CatalogApi
{
    private const string BaseUrl = "/api/v{version:apiVersion}/catalog";
    
    public static IVersionedEndpointRouteBuilder MapCatalogApiV1(this IVersionedEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup(BaseUrl).HasApiVersion(1);

        group.MapPost(string.Empty, async (ISender sender, CreateCatalogCommand command) => await sender.Send(command));
        group.MapPut("/{id:guid}/active", async (ISender sender, Guid id) => await sender.Send(new ActiveCatalogCommand(id)));
        group.MapPut("/{id:guid}/inactive", async (ISender sender, Guid id) => await sender.Send(new InactiveCatalogCommand(id)));

        group.MapDelete("/{id:guid}",
            async (ISender sender, Guid id) => await sender.Send(new RemoveCatalogItemCommand()));
        
        
        group.MapGet(string.Empty, async (ISender sender, HttpContext context,[FromHeader(Name = "x-query")] string stringQuery) =>
        {
            var query = context.GetQuery<GetCatalogsQuery>(stringQuery);
            var result = await sender.Send(query);
            return Results.Ok(result);
        });
        
        return endpoints;
    }
}