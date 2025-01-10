using Asp.Versioning.Builder;

namespace WebApi.Apis;

public static class PaymentApi
{
    private const string BaseUrl = "/api/v{version:apiVersion}/payments";
    public static IVersionedEndpointRouteBuilder MapPaymentApiV1(this IVersionedEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup(BaseUrl).HasApiVersion(1);

        
        
        return endpoints;
    }
}