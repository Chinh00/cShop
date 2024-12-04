using Asp.Versioning.Builder;

namespace WebApi.Apis;

public static class ShipperApi
{
    const string BaseUrl = "/api/v{version:apiVersion}/shippers";
    public static IVersionedEndpointRouteBuilder MapShipperApi(this IVersionedEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup(BaseUrl).HasApiVersion(1);
            
        
        
        
        return endpoints;
    }
}