using Application.UseCases.Dtos;
using Application.UseCases.Specs;
using AutoMapper;
using cShop.Core.Domain;
using cShop.Core.Repository;
using cShop.Infrastructure.IdentityServer;
using Domain;
using FluentValidation;
using MediatR;

namespace Application.UseCases.Queries;

public record GetMyOrdersQuery : IQuery<ListResultModel<OrderDto>>
{
    public List<FilterModel> Filters { get; set; } = [];
    public List<string> Includes { get; set; } = [];
    public List<string> Sorts { get; set; } = [];
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 15;
    public class Validator : AbstractValidator<GetMyOrdersQuery>
    {
        public Validator()
        {
            
        }
    }
    
    internal class Handler(IListRepository<Order> repository, IMapper mapper, IClaimContextAccessor contextAccessor)
        : IRequestHandler<GetMyOrdersQuery, ResultModel<ListResultModel<OrderDto>>>
    {
        public async Task<ResultModel<ListResultModel<OrderDto>>> Handle(GetMyOrdersQuery request, CancellationToken cancellationToken)
        {
            var spec = new GetOrdersByUserIdSpec<OrderDto>(request, contextAccessor.GetUserId());
            var orders = await repository.FindAsync(spec, cancellationToken);
            var result = mapper.Map<List<OrderDto>>(orders);
            var listResultModel = ListResultModel<OrderDto>.Create(result, orders.Count, request.Page, request.PageSize);
            return ResultModel<ListResultModel<OrderDto>>.Create(listResultModel);
        }
    }
}