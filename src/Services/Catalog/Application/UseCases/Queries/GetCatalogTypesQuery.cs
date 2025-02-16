using Application.UseCases.Dtos;
using Application.UseCases.Queries.Specs;
using AutoMapper;
using cShop.Core.Domain;
using cShop.Core.Repository;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.UseCases.Queries;

public record GetCatalogTypesQuery : IQuery<ListResultModel<CatalogTypeDto>>
{
    public List<FilterModel> Filters { get; set; } = [];
    public List<string> Includes { get; set; } = [];
    public List<string> Sorts { get; set; } = [];
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    
    public class Validator : AbstractValidator<GetCatalogTypesQuery>
    {
        public Validator()
        {
            
        }
    }
    internal class Handler(IListRepository<CatalogType> repository, IMapper mapper)
        : IRequestHandler<GetCatalogTypesQuery, ResultModel<ListResultModel<CatalogTypeDto>>>
    {
        public async Task<ResultModel<ListResultModel<CatalogTypeDto>>> Handle(GetCatalogTypesQuery request, CancellationToken cancellationToken)
        {
            var specs = new GetCatalogTypesSpec(request);
            
            var catalogItems = await repository.FindAsync(specs, cancellationToken);
            var catalogCount = await repository.CountAsync(specs, cancellationToken);
            var listResult = ListResultModel<CatalogTypeDto>.Create(mapper.Map<List<CatalogTypeDto>>(catalogItems),
                catalogCount, request.Page, request.PageSize);
            return ResultModel<ListResultModel<CatalogTypeDto>>.Create(listResult);
        }
    }
    
}