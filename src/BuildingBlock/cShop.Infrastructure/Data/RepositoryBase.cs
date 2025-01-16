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

    IQueryable<TEntity> GetQuery(IQueryable<TEntity> source, ISpecification<TEntity> specification)
    {
        if (specification.Filter is not null) source = source.Where(specification.Filter);
        specification.Includes.ForEach(e => source = source.Include(e));
        specification.IncludeStrings.ForEach(e => source = source.Include(e));
        // source = source.Skip(specification.Skip).Take(specification.Take);
        return source;
    }
    IQueryable<TEntity> GetQuery(IQueryable<TEntity> source, IListSpecification<TEntity> specification)
    {
        specification.Filter.ForEach(e => source = source.Where(e));
        specification.Includes.ForEach(e => source = source.Include(e));
        specification.IncludeStrings.ForEach(e => source = source.Include(e));
        specification.OrderBys.ForEach(e => source = source.OrderBy(e));
        specification.OrderDescBys.ForEach(e => source = source.OrderByDescending(e));
        if (specification.IsPagingEnabled) source = source.Skip(specification.Skip - 1).Take(specification.Take);
        return source;
    }

    public async Task<TEntity> FindByIdAsync<TId>(TId id, CancellationToken cancellationToken)
    {
        return await _dbSet.FindAsync(id, cancellationToken);
    }

    public async Task<List<TEntity>> FindAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken)
    {
        var query = GetQuery(_dbSet, specification);
        return await query.ToListAsync(cancellationToken);
    }

    public async Task<TEntity> FindOneAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken)
    { 
        var query = GetQuery(_dbSet, specification);
        return await query.FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public Task<bool> RemoveAsync(TEntity entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<List<TEntity>> FindAsync(IListSpecification<TEntity> specification, CancellationToken cancellationToken)
    {
        var query = GetQuery(_dbSet, specification);
        return await query.ToListAsync(cancellationToken);
    }

    public async ValueTask<long> CountAsync(IListSpecification<TEntity> specification, CancellationToken cancellationToken)
    {
        specification.IsPagingEnabled = false;
        var query = GetQuery(_dbSet, specification);
        return await query.LongCountAsync(cancellationToken: cancellationToken);
    }
}