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

    public async Task<TEntity> FindByIdAsync(Guid id)
    {
        return await DbSet.FindAsync(id);
    }

    public Task<List<TEntity>> FindAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await DbSet.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        DbSet.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public Task<bool> RemoveAsync(TEntity entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}