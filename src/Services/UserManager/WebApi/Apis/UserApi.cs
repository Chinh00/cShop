using Application.UseCases.Commands;
using Application.UseCases.Queries;
using Asp.Versioning.Builder;
using cShop.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Apis;

public static class UserApi
{
    const string BaseUrl = "/api/v{version:apiVersion}/users";
    
    
    public static IVersionedEndpointRouteBuilder MapUserApi(this IVersionedEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup(BaseUrl).HasApiVersion(1);
        group.MapPost("/shippers", async (ISender sender, CreateShipperCommand command) => await sender.Send(command));
        group.MapPost("/customers", async (ISender sender, CreateCustomerCommand command) => await sender.Send(command));
        group.MapGet("/customers", async (ISender sender, HttpContext context,[FromHeader(Name = "x-query")] string stringQuery) =>
        {
            var query = context.GetQuery<GetCustomersQuery>(stringQuery);
            var result = await sender.Send(query);
            return result;
        });
        group.MapGet("/shippers", async (ISender sender, HttpContext context,[FromHeader(Name = "x-query")] string stringQuery) =>
        {
            var query = context.GetQuery<GetShippersQuery>(stringQuery);
            var result = await sender.Send(query);
            return result;
        });
        return endpoints;
    }
}