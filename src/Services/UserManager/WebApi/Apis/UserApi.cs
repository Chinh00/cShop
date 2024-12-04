using Application.UseCases.Commands;
using Asp.Versioning.Builder;
using MediatR;

namespace WebApi.Apis;

public static class UserApi
{
    const string BaseUrl = "/api/v{version:apiVersion}/users";
    
    
    public static IVersionedEndpointRouteBuilder MapUserApi(this IVersionedEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup(BaseUrl).HasApiVersion(1);
        group.MapPost("/shippers", async (ISender sender, CreateShipperCommand command) => await sender.Send(command));
        group.MapPost("/customers", async (ISender sender, CreateCustomerCommand command) => await sender.Send(command));
        
        return endpoints;
    }
}