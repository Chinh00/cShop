using cShop.Core.Domain;
using cShop.Core.Repository;
using cShop.Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace cShop.Infrastructure.Data;

public class RepositoryBase<TDbContext, TEntity> : IRepository<TEntity>, IListRepository<TEntity>
    where TDbContext : DbContext
    where TEntity : EntityBase
{
    private readonly TDbContext _context;
    private readonly DbSet<TEntity> _dbSet;
    public RepositoryBase(TDbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    void GetQuery(IQueryable<TEntity> source, ISpecification<TEntity> specification)
    {
        source = source.Where(specification.Filter);
        specification.Includes.Aggregate(source, (queryable, current) => queryable.Include(current));
        specification.IncludeStrings.Aggregate(source, (queryable, current) => queryable.Include(current));
        
    }

    public async Task<TEntity> FindByIdAsync<TId>(TId id, CancellationToken cancellationToken)
    {
        return await _dbSet.FindAsync(id, cancellationToken);
    }

    public Task<List<TEntity>> FindAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<TEntity> FindOneAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
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

    public Task<TEntity> FindAsync(IListSpecification<TEntity> specification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}