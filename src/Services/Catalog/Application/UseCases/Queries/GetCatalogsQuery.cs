using Application.UseCases.Dtos;
using Application.UseCases.Queries.Specs;
using AutoMapper;
using cShop.Core.Domain;
using cShop.Core.Repository;
using Domain.Aggregate;
using FluentValidation;
using MediatR;

namespace Application.UseCases.Queries;

public record GetCatalogsQuery : IQuery<ListResultModel<CatalogItemDto>>
{
    public List<FilterModel> Filters { get; set; } = [];
    public List<string> Includes { get; set; } = [];
    public List<string> Sorts { get; set; } = [];
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    
    public class Validator : AbstractValidator<GetCatalogsQuery>
    {
        public Validator()
        {
            
        }
    }
    
    
    internal class Handler(IListRepository<CatalogItem> catalogItemRepository, IMapper mapper)
        : IRequestHandler<GetCatalogsQuery, ResultModel<ListResultModel<CatalogItemDto>>>
    {
        public async Task<ResultModel<ListResultModel<CatalogItemDto>>> Handle(GetCatalogsQuery request, CancellationToken cancellationToken)
        {
            var specs = new GetCatalogsSpec(request);
            
            var catalogItems = await catalogItemRepository.FindAsync(specs, cancellationToken);
            var listResult = ListResultModel<CatalogItemDto>.Create(mapper.Map<List<CatalogItemDto>>(catalogItems),
                catalogItems.Count, 1, 10);
            return ResultModel<ListResultModel<CatalogItemDto>>.Create(listResult);
        }
    }
}