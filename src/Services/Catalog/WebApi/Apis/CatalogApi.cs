using Application.UseCases.Command;
using Asp.Versioning.Builder;

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
        
        
        return endpoints;
    }
}