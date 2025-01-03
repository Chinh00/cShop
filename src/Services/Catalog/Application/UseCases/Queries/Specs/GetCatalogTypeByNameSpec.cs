using System.Linq.Expressions;
using cShop.Core.Specifications;
using Domain.Entities;

namespace Application.UseCases.Queries.Specs;

public sealed class GetCatalogTypeByNameSpec : SpecificationBase<CatalogType>
{
    public GetCatalogTypeByNameSpec(string name)
    {
        ApplyFilter(e => e.Name.Contains(name));
    }
}