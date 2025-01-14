using Application.UseCases.Commands;
using Asp.Versioning.Builder;
using MediatR;

namespace WebApi.Apis;

public static class PaymentApi
{
    private const string BaseUrl = "/api/v{version:apiVersion}/payments";
    public static IVersionedEndpointRouteBuilder MapPaymentApiV1(this IVersionedEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup(BaseUrl).HasApiVersion(1).RequireAuthorization();

        group.MapPost("/payment-url",
            async (ISender sender, CreatePaymentUrlCommand command) => await sender.Send(command));
        
        group.MapGet("/callback-payment",
            async (ISender sender, HttpContext context) =>
                await sender.Send(new PaymentResultCommand(context.Request.Query))).AllowAnonymous();
        return endpoints;
    }
}