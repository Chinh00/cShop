using Application.UseCases.Queries;
using Asp.Versioning.Builder;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Apis;

public static class SearchApi
{
    private const string BaseUrl = "/api/v{version:apiVersion}/search";
    public static IVersionedEndpointRouteBuilder MapSearchApiV1(this IVersionedEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup(BaseUrl).HasApiVersion(1);

        group.MapGet(string.Empty,
            async ([FromServices] ISender sender,[AsParameters] GetCatalogsQuery command) => await sender.Send(command));
        return endpoints;
    }
}