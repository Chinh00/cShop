using Application.UseCases.Commands;
using Application.UseCases.Queries;
using Asp.Versioning.Builder;
using cShop.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Apis;

public static class CommentApi
{
    private const string BaseUrl = "/api/v{version:apiVersion}/comments";
    
    public static IVersionedEndpointRouteBuilder MapCommentApiV1(this IVersionedEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup(BaseUrl).HasApiVersion(1);

        group.MapGet(string.Empty,
            async (ISender sender, HttpContext context, [FromHeader(Name = "x-query")] string? stringQuery) =>
            {
                var query = context.GetQuery<GetCommentsQuery>(stringQuery);
                return await sender.Send(query);
            });
        group.MapPost(string.Empty,
            async (ISender sender, CreateCommentCommand command) =>
                await sender.Send(command)).RequireAuthorization();
        
        return endpoints;
    }
}