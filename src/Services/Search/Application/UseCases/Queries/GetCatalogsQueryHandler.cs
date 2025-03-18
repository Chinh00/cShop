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
                    QueryContainer queryContainer = q.MatchAll();

                    if (!string.IsNullOrEmpty(request.q))
                    {
                        queryContainer &= q.MultiMatch(m => m
                            .Fields(f => f
                                .Field(x => x.CatalogName, 4.0)
                                .Field(x => x.Description, 3.0)
                                .Field(x => x.CatalogBrandName, 2.0)
                                .Field(x => x.CatalogTypeName, 2.0))
                            .Query(request.q)
                            .Type(TextQueryType.BestFields)
                            .Operator(Operator.Or));
                    }

                    if (request.CatalogTypeId is not null)
                    {
                        queryContainer &= q.Term(x => x.Field(ff => ff.CatalogTypeId.Suffix("keyword")).Value(request.CatalogTypeId));
                    }
                    if (request.CatalogBrandId is not null)
                    {
                        queryContainer &= q.Term(x => x.Field(ff => ff.CatalogBrandId.Suffix("keyword")).Value(request.CatalogBrandId));
                    }
                    
                    return queryContainer;
                })
                .Index(IndexConstants.CatalogIndex)
                .Skip(request.Page * request.PageSize)
                .Take(request.PageSize),
            cancellationToken);
        return ResultModel<ListResultModel<CatalogIndexModel>>.Create(ListResultModel<CatalogIndexModel>.Create(searchResponse.Documents.ToList(), (long)searchResponse.HitsMetadata.Total.Value, request.Page ?? 1, request.PageSize ?? 10));
        
    }
}