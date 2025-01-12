using System.Linq.Expressions;
using cShop.Core.Domain;

namespace cShop.Core.Specifications;

public class ListSpecification<TEntity> : IListSpecification<TEntity> where TEntity : EntityBase
{
    public virtual List<Expression<Func<TEntity, bool>>> Filter { get; } = [];
    public List<Expression<Func<TEntity, object>>> Includes { get; set; } = [];
    public List<string> IncludeStrings { get; } = [];
    public List<Expression<Func<TEntity, object>>> OrderBys { get; set; } = [];
    public List<Expression<Func<TEntity, object>>> OrderDescBys { get; set; } = [];
    public Expression<Func<TEntity, object>> GroupBy { get; set; }
    public int Skip { get; set; } = 1;
    public int Take { get; set; } = 15;



    
    public void ApplyInclude(Expression<Func<TEntity, object>> include) => Includes.Add(include);

    public void ApplyIncludeList(ICollection<string> includes) => IncludeStrings.AddRange(includes);
    
    public void ApplyFilter(Expression<Func<TEntity, bool>> filter) => Filter.Add(filter);
    
    public void ApplyFilterList(ICollection<FilterModel> filterModels)
    {
        foreach (var filterModel in filterModels)
        {
            Filter.Add(Extensions.BuildFilter<TEntity>(filterModel.Field, filterModel.Operator, filterModel.Value));
        }
    }

    public void ApplyOrderAsc(Expression<Func<TEntity, object>> orderBy) => OrderBys.Add(orderBy);
    public void ApplyOrderDesc(Expression<Func<TEntity, object>> orderBy) => OrderDescBys.Add(orderBy);

    public void ApplyOrderList(List<string> orderBys)
    {
        orderBys.ForEach(e => this.ApplySorting(e, nameof(ApplyOrderAsc), nameof(ApplyOrderDesc)));
    }

    public void ApplyPagination(int page, int pageSize)
    {
        Skip = page;
        Take = pageSize;
    }
    
}