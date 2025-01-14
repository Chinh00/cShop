using Application.Abstraction;
using Application.UseCases.Specs;
using cShop.Core.Domain;
using cShop.Core.Repository;
using cShop.Infrastructure.IdentityServer;
using cShop.Infrastructure.Models;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Options;


namespace Application.UseCases.Commands;

public record CreatePaymentUrlCommand(Guid OrderId) : ICommand<IResult>
{
    public class Validator : AbstractValidator<CreatePaymentUrlCommand>
    {

    }
    internal class Handler(
        IRepository<OrderInfo> orderRepository,
        IClaimContextAccessor contextAccessor, IVnpayService paymentService,
        IHttpContextAccessor httpContextAccessor, IOptions<PaymentParam> options) : IRequestHandler<CreatePaymentUrlCommand, IResult>
    {
        
        public async Task<IResult> Handle(CreatePaymentUrlCommand request, CancellationToken cancellationToken)
        {
            var spec = new GetOrderInfoByOrderIdSpec(request.OrderId, contextAccessor.GetUserId());
            var orderInfo = await orderRepository.FindOneAsync(spec, cancellationToken);
            orderInfo.Status = PaymentStatus.Pending;

            var vnp_ReturnUrl = options.Value.vnp_ReturnUrl;
            var vnp_TmnCode = options.Value.vnp_TmnCode;
            var vnp_Version = options.Value.vnp_Version;
            var vnpay_url = options.Value.vnpay_url;
            var vnp_HashSecret = options.Value.vnp_HashSecret;

            var vnp_Command = "pay";
            var vnp_Amount = $"{(long)orderInfo.Amount * 100}";
            var vnp_CreateDate = TimeZoneInfo
                .ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"))
                .ToString("yyyyMMddHHmmss");
            var vnp_CurrCode = "VND";
            var vnp_IpAddr = "1.55.216.232";
            var vnp_Locale = "vn";
            var vnp_OrderInfo = $"Thanh toan don hang: {orderInfo.Id}";
            var vnp_OrderType = "other";
            var vnp_TxnRef = new Random().Next().ToString();
            
            paymentService.AddRequestData(nameof(vnp_Amount), vnp_Amount);
            paymentService.AddRequestData(nameof(vnp_Command), vnp_Command);
            paymentService.AddRequestData(nameof(vnp_CreateDate), vnp_CreateDate);
            paymentService.AddRequestData(nameof(vnp_CurrCode), vnp_CurrCode);
            paymentService.AddRequestData(nameof(vnp_IpAddr), vnp_IpAddr);
            paymentService.AddRequestData(nameof(vnp_Locale), vnp_Locale);
            paymentService.AddRequestData(nameof(vnp_OrderInfo), vnp_OrderInfo);
            paymentService.AddRequestData(nameof(vnp_OrderType), vnp_OrderType);
            paymentService.AddRequestData(nameof(vnp_ReturnUrl), vnp_ReturnUrl);
            paymentService.AddRequestData(nameof(vnp_TmnCode), vnp_TmnCode);
            paymentService.AddRequestData(nameof(vnp_TxnRef), vnp_TxnRef);
            paymentService.AddRequestData(nameof(vnp_Version), vnp_Version);
            orderInfo.TxnRef = int.Parse(vnp_TxnRef);
            await orderRepository.UpdateAsync(orderInfo, cancellationToken);

            return Results.Ok(ResultModel<string>.Create(paymentService.CreateRequestUrl(vnpay_url, vnp_HashSecret)));
        }
    }

}