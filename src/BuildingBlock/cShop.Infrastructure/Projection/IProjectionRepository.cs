using System.Linq.Expressions;
using cShop.Core.Domain;

namespace cShop.Infrastructure.Projection;



public interface IProjectionRepository<T>
    where T : ProjectionBase
{
    public Task ReplaceOrInsertEventAsync(T replace, Expression<Func<T, bool>> filter, CancellationToken cancellationToken);

    public Task UpdateFieldAsync<TField, TId>(TId id, long version, Expression<Func<T, TField>> field, TField value, CancellationToken cancellationToken);
    
    
    public Task<T> FindByIdAsync<TId>(TId id, CancellationToken cancellationToken);
    
}