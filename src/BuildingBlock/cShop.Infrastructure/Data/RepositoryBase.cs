using cShop.Core.Domain;
using cShop.Core.Repository;
using Microsoft.EntityFrameworkCore;

namespace cShop.Infrastructure.Data;

public class RepositoryBase<TDbContext, TEntity> : IRepository<TEntity>
    where TDbContext : AppBaseContext
    where TEntity : EntityBase
{
    
    public readonly DbSet<TEntity> DbSet;
    private readonly TDbContext _context;

    public RepositoryBase(TDbContext context)
    {
        _context = context;
        DbSet = context.Set<TEntity>();
    }

    public Task<TEntity> FindByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<List<TEntity>> FindAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await _context.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> RemoveAsync(TEntity entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}