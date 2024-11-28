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
    Task<TEntity> FindAsync(IListSpecification<TEntity> specification, CancellationToken cancellationToken);
    

} 

