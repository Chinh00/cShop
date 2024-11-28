using System.Linq.Expressions;
using System.Reflection;
using cShop.Core.Domain;

namespace cShop.Core.Specifications;

public static class Extensions
{

    public static void ApplySorting<TEntity>(this ISpecificationRoot<TEntity> specification, string sort, string sortAsc, string sortDesc)
        where TEntity : EntityBase
    { 
        var specType = specification.GetType().BaseType;     
        //Expression<Func<TEntity, object>> = x => x.Property

        var endsWith = sort.EndsWith("Desc");

        var propertyName = string.Concat(sort[..1].ToUpperInvariant(), endsWith ? sort[1..(sort.Length - 1 - "Desc".Length)] : sort[1..]);
        
        
        var property = specType?.GetGenericArguments()[0].GetRuntimeProperty(propertyName) ?? throw new NullReferenceException();
        
        var param = Expression.Parameter(typeof(TEntity), "x");
        
        
        var body = Expression.Convert(Expression.Property(param, property), typeof(object));
        
        var expression = Expression.Lambda<Func<TEntity, object>>(body, param);

        var sortAscMethod = specType?.GetMethod(sortAsc, BindingFlags.Public);
        var sortDescMethod = specType?.GetMethod(sortDesc, BindingFlags.Public);

        if (endsWith)
        {
            sortDescMethod?.Invoke(specification, [new {expression}]);
        }
        else
        {
            sortAscMethod?.Invoke(specification, [new {expression}]);
        }

    }
    public static Expression<Func<TEntity, bool>> BuildFilter<TEntity>(string field, string comparision, string value)
        where TEntity : EntityBase
    {
        var parameter = Expression.Parameter(typeof(TEntity), "x");
        var property = field.Split(".").Aggregate((Expression)parameter, Expression.Property);

        return Expression.Lambda<Func<TEntity, bool>>(BuildComparison(property, comparision, value), parameter);
    }


    public static Expression<Func<TEntity, bool>> And<TEntity>(this Expression<Func<TEntity, bool>> left, Expression<Func<TEntity, bool>> right)
        where TEntity : EntityBase
    {
        var param = left.Parameters[0];
        var typeOfLeft = param.Type;
        var subExpressionVisitor = new SubExpressionVisitor()
        {
            Subst = { [right.Parameters[0]] = param  }
        };
        
        
        return Expression.Lambda<Func<TEntity, bool>>(Expression.And(left.Body, subExpressionVisitor.Visit(right)), param);
    }
    
    static Expression BuildComparison(Expression left ,string comparision, string value)
    {
        return comparision switch
        {
            "==" => BuildBinary(ExpressionType.Equal, left, value),
            _ => throw new Exception($"Invalid comparision: {comparision}")
        };
    }

    static Expression BuildBinary(ExpressionType type, Expression left, string value)
    {
        var right = Expression.Constant(value, left.Type);
        return Expression.MakeBinary(type, left, right);
    }
    
    public class SubExpressionVisitor : ExpressionVisitor 
    {
        public readonly Dictionary<Expression, Expression> Subst = new();

        protected override Expression VisitParameter(ParameterExpression node)
        {
            return Subst.TryGetValue(node, out var newValue) ? newValue : node;
        }
    }
}