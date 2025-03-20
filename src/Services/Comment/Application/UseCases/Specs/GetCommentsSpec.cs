using cShop.Core.Domain;
using cShop.Infrastructure.Mongodb;
using Domain;
using Infrastructure.Dtos;

namespace Application.UseCases.Specs;

public class GetCommentsSpec : MongoSpecification<CommentLine>
{
    public GetCommentsSpec(IQuery<ListResultModel<CommentLineDto>> request)
    {
        ApplyFilters(request.Filters);
        ApplySort(request.Sorts?.FirstOrDefault());
    }
}