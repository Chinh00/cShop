using cShop.Core.Domain;

namespace cShop.Core.Repository;

public interface IRepository<TEntity>
    where TEntity : EntityBase
{
    Task<TEntity> FindByIdAsync(Guid id);
    Task<List<TEntity>> FindAsync();
    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken);
    Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken);
    Task<bool> RemoveAsync(TEntity entity, CancellationToken cancellationToken);

} 

