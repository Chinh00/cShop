using Asp.Versioning.Builder;

namespace Identity.Api.Apis;

public static class UserApi
{
    
    private const string BaseUrl = "/api/v{version:apiVerison}/users";
    
    public static IVersionedEndpointRouteBuilder MapUserApi(this IVersionedEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup(BaseUrl).HasApiVersion(1);
        
        
        
        return endpoints;
    }
}