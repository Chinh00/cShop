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

        var endsWith = sort.EndsWith("Desc");

        var propertyName = string.Concat(sort[..1].ToUpperInvariant(), endsWith ? sort[1..(sort.Length - 1 - "Desc".Length)] : sort[1..]);
        
        
        var property = specType?.GetGenericArguments()[0].GetRuntimeProperty(propertyName) ?? throw new NullReferenceException();
        
        var param = Expression.Parameter(typeof(TEntity), "x");
        
        
        var body = Expression.Convert(Expression.Property(param, property), typeof(object));
        
        var expression = Expression.Lambda<Func<TEntity, object>>(body, param);

        var sortAscMethod = specType?.GetMethod(sortAsc, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        var sortDescMethod = specType?.GetMethod(sortDesc, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

        if (endsWith)
        {
            sortDescMethod?.Invoke(specification, [expression]);
        }
        else
        {
            sortAscMethod?.Invoke(specification, [expression]);
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
        var subExpressionVisitor = new SubExpressionVisitor()
        {
            Subst = { [right.Parameters[0]] = param  }
        };
        
        
        return Expression.Lambda<Func<TEntity, bool>>(Expression.And(left.Body, subExpressionVisitor.Visit(right)), param);
    }
    private static Expression MakeString(Expression source)
    {
        return source.Type == typeof(string) ? source : Expression.Call(source, "ToString", Type.EmptyTypes);
    }
    static Expression BuildComparison(Expression left ,string comparision, string value)
    {
        return comparision switch
        {
            "==" => BuildBinary(ExpressionType.Equal, left, value),
            "!=" => BuildBinary(ExpressionType.NotEqual, left, value),
            ">=" => BuildBinary(ExpressionType.GreaterThanOrEqual, left, value),
            "<=" => BuildBinary(ExpressionType.LessThanOrEqual, left, value),
            ">" => BuildBinary(ExpressionType.GreaterThan, left, value),
            "<" => BuildBinary(ExpressionType.LessThan, left, value),
            "Contains" or "StartsWith" or "EndsWith" => Expression.Call(MakeString(left), comparision,
                Type.EmptyTypes, Expression.Constant(value, typeof(string))),
            _ => throw new Exception($"Invalid comparision: {comparision}")
        };
    }

    static Expression MakeExpressionContains(Expression left, string listValue)
    {
        var baseType = left.GetType().BaseType;
        var methodInfo = baseType?.GetMethod("Contains", [typeof(string)]);
        return Expression.Call(left, methodInfo, new Expression[] {Expression.Constant(listValue) });
    }
    
    static Expression BuildBinary(ExpressionType type, Expression left, string value)
    {
        object leftType = value;
        if (!(left.Type == typeof(string)))
        {
            if (string.IsNullOrEmpty(value))
            {
                leftType = null;
                if (Nullable.GetUnderlyingType(left.Type) == null) 
                    left = Expression.Convert(left, typeof(Nullable<>).MakeGenericType(left.Type));               
            }
            else
            {
                var valueType = Nullable.GetUnderlyingType(left.Type) ?? left.Type;
                leftType = valueType.IsEnum ? Enum.Parse(valueType, value) :
                    valueType == typeof(Guid) ? Guid.Parse(value) : Convert.ChangeType(value, valueType);
            }
        }

        var right = Expression.Constant(leftType, left.Type);
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