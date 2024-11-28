using System.Linq.Expressions;
using cShop.Core.Domain;

namespace cShop.Core.Specifications;

public abstract class SpecificationBase<TEntity> : ISpecification<TEntity> where TEntity : EntityBase
{
    public abstract Expression<Func<TEntity, bool>> Filter { get; } 
    public List<Expression<Func<TEntity, object>>> Includes { get; set; }
    public List<string> IncludeStrings { get; } = [];
    public List<Expression<Func<TEntity, object>>> OrderBys { get; set; }
    public List<Expression<Func<TEntity, object>>> OrderDescBys { get; set; }
    public Expression<Func<TEntity, object>> GroupBy { get; set; }
    public int Skip { get; set; }
    public int Take { get; set; }


    public void ApplyFilter(Expression<Func<TEntity, bool>> filter)
    {
    }

    public void ApplyFilterList(ICollection<FilterModel> filterModels)
    {
        foreach (var filterModel in filterModels)
        {
            var expression = Extensions.BuildFilter<TEntity>(filterModel.Field, filterModel.Operator, filterModel.Value);
        }

    }
}