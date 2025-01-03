using System.Linq.Expressions;
using cShop.Core.Domain;

namespace cShop.Core.Specifications;

public interface ISpecificationRoot<TEntity> where TEntity : EntityBase { }


public interface ISpecification<TEntity> : ISpecificationRoot<TEntity> where TEntity : EntityBase
{
    Expression<Func<TEntity, bool>> Filter { get; }
    List<Expression<Func<TEntity, object>>> Includes { get; } 
    List<string> IncludeStrings { get; }
    List<Expression<Func<TEntity, object>>> OrderBys { get;  }
    
    List<Expression<Func<TEntity, object>>> OrderDescBys { get;  }
    
    Expression<Func<TEntity, object>> GroupBy { get;  }
    
    int Skip { get;  }

    int Take { get;  }
}



public interface IListSpecification<TEntity> : ISpecificationRoot<TEntity> where TEntity : EntityBase
{
    List<Expression<Func<TEntity, bool>>> Filter { get; }
    List<Expression<Func<TEntity, object>>> Includes { get;  }
    List<string> IncludeStrings { get; }

    List<Expression<Func<TEntity, object>>> OrderBys { get;  }
    
    List<Expression<Func<TEntity, object>>> OrderDescBys { get;  }
    
    Expression<Func<TEntity, object>> GroupBy { get; }
    
    int Skip { get;  }

    int Take { get;  }
}



