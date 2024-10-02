using System.Linq.Expressions;
using cShop.Core.Domain;
using MongoDB.Driver;

namespace cShop.Infrastructure.Projection;

public class ProjectionRepositoryBase<TProjection> : IProjectionRepository<TProjection>
    where TProjection : ProjectionBase
{
    private readonly ILogger<TProjection> _logger;
    private readonly IProjectionDbContext _projectionDbContext;
    public ProjectionRepositoryBase(ILogger<TProjection> logger, IProjectionDbContext projectionDbContext)
    {
        _logger = logger;
        _projectionDbContext = projectionDbContext;
    }

    
    public async Task ReplaceOrInsertEventAsync(TProjection replace, Expression<Func<TProjection, bool>> filter, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Persist {CollectionName} in database.", typeof(TProjection).Name);
        await _projectionDbContext.GetCollection<TProjection>().ReplaceOneAsync(filter, replace, new ReplaceOptions() { IsUpsert = true }, cancellationToken: cancellationToken);
        
    }

    public async Task UpdateFieldAsync<TField, TId>(TId id, long version, Expression<Func<TProjection, TField>> field, TField value,
        CancellationToken cancellationToken)
    {
        await _projectionDbContext.GetCollection<TProjection>().UpdateOneAsync(
            filter: e => e.Id == Guid.Parse(id.ToString()) && e.Version < version,
            update: new ObjectUpdateDefinition<TProjection>(new object()).Set(field, value),
            cancellationToken: cancellationToken
        );
    }

    public async Task<TProjection> FindByIdAsync<TId>(TId id, CancellationToken cancellationToken)
    {
        var entity = await _projectionDbContext.GetCollection<TProjection>().FindAsync(e => e.Id == Guid.Parse(id.ToString()), cancellationToken: cancellationToken);
        return await entity.FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }
}