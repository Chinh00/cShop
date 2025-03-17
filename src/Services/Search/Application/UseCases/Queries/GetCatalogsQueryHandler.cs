using cShop.Core.Domain;
using Infrastructure.Catalog;
using Infrastructure.Constants;
using MediatR;
using Nest;

namespace Application.UseCases.Queries;

public class GetCatalogsQueryHandler(IElasticClient elasticClient)
    : IRequestHandler<GetCatalogsQuery, ResultModel<ListResultModel<CatalogIndexModel>>>
{
    public async Task<ResultModel<ListResultModel<CatalogIndexModel>>> Handle(GetCatalogsQuery request, CancellationToken cancellationToken)
    {

        var searchResponse = await elasticClient.SearchAsync<CatalogIndexModel>(
            c => c.Query(q =>
                {
                    QueryContainer queryContainer = q.MatchAll(); // Mặc định lấy tất cả nếu không có điều kiện

                    if (!string.IsNullOrEmpty(request.q))
                    {
                        queryContainer &= q.MultiMatch(m => m
                            .Fields(f => f
                                .Field(x => x.Description, 4.0)
                                .Field(x => x.CatalogName, 4.0)
                                .Field(x => x.Description, 3.0)
                                .Field(x => x.CatalogBrandName, 1.0))
                            .Query(request.q)
                            .Type(TextQueryType.BestFields)
                            .Operator(Operator.Or));
                    }

                    return queryContainer;
                })
                .Index(IndexConstants.CatalogIndex)
                .Skip(request.Page * request.PageSize)
                .Take(request.PageSize),
            cancellationToken);
        return ResultModel<ListResultModel<CatalogIndexModel>>.Create(ListResultModel<CatalogIndexModel>.Create(searchResponse.Documents.ToList(), (long)searchResponse.Documents.ToList().Count, request.Page, request.PageSize));
    }
}