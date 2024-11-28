using System.Linq.Expressions;
using cShop.Core.Domain;

namespace cShop.Core.Specifications;

public class OrSpecification<TEntity>(ISpecification<TEntity> left, ISpecification<TEntity> right)
    : SpecificationBase<TEntity>
    where TEntity : EntityBase
{


    public override Expression<Func<TEntity, bool>> Filter
    {
        get
        {
            var param = Expression.Parameter(typeof(TEntity), "x");
            return Expression.Lambda<Func<TEntity, bool>>(Expression.Or(Expression.Invoke(left.Filter), Expression.Invoke(right.Filter)), param);
        }
    }
}