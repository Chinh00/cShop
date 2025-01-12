using Application.UseCases.Commands;
using Asp.Versioning.Builder;
using MediatR;

namespace WebApi.Apis;

public static class PaymentApi
{
    private const string BaseUrl = "/api/v{version:apiVersion}/payments";
    public static IVersionedEndpointRouteBuilder MapPaymentApiV1(this IVersionedEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup(BaseUrl).HasApiVersion(1);

        group.MapPost("/payment-url",
            async (ISender sender, CreatePaymentUrlCommand command) => await sender.Send(command));
        
        group.MapPost("/callback-payment",
            async (ISender sender, PaymentResultCommand command) => await sender.Send(command));
        return endpoints;
    }
}