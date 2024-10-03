using Application.UseCases;
using Asp.Versioning.Builder;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Apis;

public static class PaymentApi
{
    private const string BaseUrl = "/api/v{version:apiVersion}/payment";
    
    public static IVersionedEndpointRouteBuilder MapPaymentApiV1(this IVersionedEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup(BaseUrl).HasApiVersion(1).RequireAuthorization();
        group.MapPost("/payment", async ([FromServices] ISender sender,[FromBody] Commands.PaymentOrder payment, CancellationToken cancellationToken) => await sender.Send(payment, cancellationToken));
        return endpoints;
    }
}