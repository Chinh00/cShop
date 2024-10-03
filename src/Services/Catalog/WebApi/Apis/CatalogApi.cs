using Application.UseCases.Command;
using Asp.Versioning.Builder;
using MediatR;

namespace WebApi.Apis;

public static class CatalogApi
{
    private const string BaseUrl = "/api/v{version:apiVersion}/catalog";
    
    public static IVersionedEndpointRouteBuilder MapCatalogApiV1(this IVersionedEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup(BaseUrl).HasApiVersion(1);

        group.MapPost(string.Empty, async (ISender sender, Commands.CreateCatalog command) => await sender.Send(command));
        group.MapPut("/{id:guid}/active", async (ISender sender, Guid id) => await sender.Send(new Commands.ActiveCatalog(id)));
        group.MapPut("/{id:guid}/inactive", async (ISender sender, Guid id) => await sender.Send(new Commands.InActiveCatalog(id)));
        
        
        return endpoints;
    }
}