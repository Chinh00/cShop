using Application.UseCases.Dtos;
using Application.UseCases.Specs;
using AutoMapper;
using cShop.Core.Domain;
using cShop.Core.Repository;
using Domain;
using FluentValidation;
using MediatR;

namespace Application.UseCases.Queries;

public record GetShippersQuery : IQuery<ListResultModel<ShipperDto>>
{
    public List<FilterModel> Filters { get; set; } = [];
    public List<string> Includes { get; set; } = [];
    public List<string> Sorts { get; set; } = [];
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 20;
    
    
    public class Validator : AbstractValidator<GetShippersQuery>
    {
        
    }
    internal class Handler(IMapper mapper, IListRepository<Shipper> repository)
        : IRequestHandler<GetShippersQuery, ResultModel<ListResultModel<ShipperDto>>>
    {
        public async Task<ResultModel<ListResultModel<ShipperDto>>> Handle(GetShippersQuery request,
            CancellationToken cancellationToken)
        {
            var specs = new GetShippersSpec(request);
            var customers = await repository.FindAsync(specs, cancellationToken);
            var listResultModel = ListResultModel<ShipperDto>.Create(mapper.Map<List<ShipperDto>>(customers),
                customers.Count, request.Page, request.PageSize);
            return ResultModel<ListResultModel<ShipperDto>>.Create(listResultModel);
        }
    }
}