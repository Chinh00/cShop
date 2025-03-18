using System.Linq.Expressions;
using cShop.Core.Domain;
using cShop.Core.Specifications;

namespace cShop.Infrastructure.Mongodb;

public class MongoSpecification<TEntity> : IMongoSpecification<TEntity> where TEntity : MongoEntityBase
{
    public Expression<Func<TEntity, bool>> Filter { get; set; }
    public Expression<Func<TEntity, object>> Sorting { get; set; }
    public Expression<Func<TEntity, object>> SortingDesc { get; set; }
    public bool IsPagingEnabled { get; set; }
    public int Take { get; } = 1;
    public int Skip { get; } = 10;

    protected void ApplyFilter(FilterModel filterModel)
    {
        Filter = Core.Specifications.Extensions.BuildFilter<TEntity>(filterModel.Field, filterModel.Operator,
            filterModel.Value);
    }
    
    protected void ApplyFilters(List<FilterModel> filterModels)
    {
        if (filterModels.Count == 0) return;
        ApplyFilter(filterModels[0]);
        for (int i = 1; i < filterModels.Count(); i++)
        {
            Filter = Filter.And(Core.Specifications.Extensions.BuildFilter<TEntity>(filterModels[i].Field, filterModels[i].Operator, filterModels[i].Value));
        }
    }

    
}