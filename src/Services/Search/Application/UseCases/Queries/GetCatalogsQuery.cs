using cShop.Core.Domain;
using FluentValidation;
using Infrastructure.Catalog;
using MediatR;

namespace Application.UseCases.Queries;

public class GetCatalogsQuery : IRequest<ResultModel<ListResultModel<CatalogIndexModel>>>
{
    public string q { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public int? Order { get; set; }
}

public class Validator : AbstractValidator<GetCatalogsQuery>
{
    public Validator()
    {
        
    }
}