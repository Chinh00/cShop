using System.Linq.Expressions;
using cShop.Core.Domain;
using cShop.Core.Specifications;

namespace cShop.Core.Repository;

public interface IRepository<TEntity>
    where TEntity : EntityBase
{
    Task<TEntity> FindByIdAsync<TId>(TId id, CancellationToken cancellationToken);
    Task<List<TEntity>> FindAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken);
    Task<TEntity> FindOneAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken);
    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken);
    Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken);
    Task<bool> RemoveAsync(TEntity entity, CancellationToken cancellationToken);

} 

public interface IListRepository<TEntity>
    where TEntity : EntityBase
{
    Task<List<TEntity>> FindAsync(IListSpecification<TEntity> specification, CancellationToken cancellationToken);
    ValueTask<long> CountAsync(IListSpecification<TEntity> specification, CancellationToken cancellationToken);
}

public interface IMongoRepository<TEntity> where TEntity : MongoEntityBase{}

public interface IMongoCommandRepository<TEntity> : IMongoRepository<TEntity> where TEntity : MongoEntityBase
{
    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken);
    Task<TEntity> UpdateAsync(Expression<Func<TEntity, bool>> filter, TEntity entity, CancellationToken cancellationToken);
    Task<TEntity> RemoveAsync(Expression<Func<TEntity, bool>> condition, CancellationToken cancellationToken);
}

public interface IMongoQueryRepository<TEntity> : IMongoRepository<TEntity> where TEntity : MongoEntityBase
{
    Task<List<TEntity>> FindAsync(IMongoSpecification<TEntity> specification, CancellationToken cancellationToken);
    ValueTask<long> CountAsync(IMongoSpecification<TEntity> specification, CancellationToken cancellationToken);
}
