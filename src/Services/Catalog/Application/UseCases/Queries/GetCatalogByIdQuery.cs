using Application.UseCases.Dtos;
using Application.UseCases.Queries.Specs;
using AutoMapper;
using cShop.Core.Domain;
using cShop.Core.Repository;
using Domain.Aggregate;
using FluentValidation;
using MediatR;

namespace Application.UseCases.Queries;

public record GetCatalogByIdQuery(Guid Id) : IRequest<ResultModel<CatalogItemDto>>
{
    public class Validator : AbstractValidator<GetCatalogByIdQuery>
    {
        public Validator()
        {
            
        }
    }
    internal class Handler(IMapper mapper, IRepository<CatalogItem> repository)
        : IRequestHandler<GetCatalogByIdQuery, ResultModel<CatalogItemDto>>
    {

        public async Task<ResultModel<CatalogItemDto>> Handle(GetCatalogByIdQuery request, CancellationToken cancellationToken)
        {
            var spec = new GetCatalogByIdSpec(request.Id);
            var catalog = await repository.FindOneAsync(spec, cancellationToken);
            return ResultModel<CatalogItemDto>.Create(catalog is null ? null : mapper.Map<CatalogItemDto>(catalog));
        }
    }
}