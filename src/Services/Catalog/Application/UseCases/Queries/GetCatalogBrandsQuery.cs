using Application.UseCases.Dtos;
using Application.UseCases.Queries.Specs;
using AutoMapper;
using cShop.Core.Domain;
using cShop.Core.Repository;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.UseCases.Queries;

public record GetCatalogBrandsQuery : IQuery<ListResultModel<CatalogBrandDto>>
{
    public List<FilterModel> Filters { get; set; } = [];
    public List<string> Includes { get; set; } = [];
    public List<string> Sorts { get; set; } = [];
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    
    public class Validator : AbstractValidator<GetCatalogBrandsQuery>
    {
        public Validator()
        {
            
        }
    }
    internal class Handler(IListRepository<CatalogBrand> repository, IMapper mapper)
        : IRequestHandler<GetCatalogBrandsQuery, ResultModel<ListResultModel<CatalogBrandDto>>>
    {
        public async Task<ResultModel<ListResultModel<CatalogBrandDto>>> Handle(GetCatalogBrandsQuery request, CancellationToken cancellationToken)
        {
            var specs = new GetCatalogBrandsSpec(request);
            
            var catalogItems = await repository.FindAsync(specs, cancellationToken);
            var catalogCount = await repository.CountAsync(specs, cancellationToken);
            var listResult = ListResultModel<CatalogBrandDto>.Create(mapper.Map<List<CatalogBrandDto>>(catalogItems),
                catalogCount, request.Page, request.PageSize);
            return ResultModel<ListResultModel<CatalogBrandDto>>.Create(listResult);
        }
    }
}