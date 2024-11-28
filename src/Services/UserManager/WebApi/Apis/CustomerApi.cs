using Application.UseCases.Commands;
using Asp.Versioning.Builder;
using MediatR;

namespace WebApi.Apis;

public static class CustomerApi
{
    public const string BaseUrl = "/api/v{version:apiVersion}/customers";

    public static IVersionedEndpointRouteBuilder MapCustomersApi(this IVersionedEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup(BaseUrl).HasApiVersion(1);



        group.MapPost(string.Empty, async (ISender sender, CreateCustomerCommand command) => await sender.Send(command));
        
        
        
        return endpoints;
    }
    
    
}