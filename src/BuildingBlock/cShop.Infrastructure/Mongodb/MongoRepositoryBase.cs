using System.Linq.Expressions;
using cShop.Core.Domain;
using cShop.Core.Repository;
using cShop.Core.Specifications;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace cShop.Infrastructure.Mongodb;

public class MongoRepositoryBase<TEntity> : IMongoCommandRepository<TEntity>, IMongoQueryRepository<TEntity> where TEntity : MongoEntityBase 
{
    private readonly IMongoCollection<TEntity> _mongoCollection;

    public MongoRepositoryBase(IOptions<MongoDbbOption> options)
    {
        _mongoCollection = new MongoClient(options.Value.ToString()).GetDatabase(options.Value.DatabaseName).GetCollection<TEntity>(typeof(TEntity).Name);
    }


    public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await _mongoCollection.InsertOneAsync(entity, cancellationToken: cancellationToken).ConfigureAwait(false);
        return entity;
    }

    public async Task<TEntity> UpdateAsync(Expression<Func<TEntity, bool>> filter, TEntity entity, CancellationToken cancellationToken)
    {
        await _mongoCollection.FindOneAndReplaceAsync(filter, entity,
            new FindOneAndReplaceOptions<TEntity>() { IsUpsert = false, ReturnDocument = ReturnDocument.After },
            cancellationToken: cancellationToken);
        return entity;
    }

    public async Task<TEntity> RemoveAsync(Expression<Func<TEntity, bool>> condition, CancellationToken cancellationToken)
    {
        var result = await _mongoCollection.FindOneAndDeleteAsync(condition, cancellationToken: cancellationToken);
        return result;
    }

    public Task<List<TEntity>> FindAsync(IMongoSpecification<TEntity> specification, CancellationToken cancellationToken)
    {
        return GetQueryableAggregate(specification).ToListAsync(cancellationToken);
    }

    IAggregateFluent<TEntity> GetQueryableAggregate(IMongoSpecification<TEntity> specification)
    {
        var aggregate = _mongoCollection.Aggregate();
        if (specification.Filter is not null) aggregate = aggregate.Match(specification.Filter);
        if (specification.Sorting is not null) aggregate = aggregate.Sort(specification.Sorting.ToString());
        if (specification.SortingDesc is not null) aggregate = aggregate.Sort(specification.SortingDesc.ToString());
        if (!specification.IsPagingEnabled) return aggregate;
        if (specification.Skip > 0) aggregate = aggregate.Skip(specification.Take);
        if (specification.Take > 0) aggregate = aggregate.Limit(specification.Take);
        return aggregate;
    }
    public async ValueTask<long> CountAsync(IMongoSpecification<TEntity> specification, CancellationToken cancellationToken)
    {
        return await _mongoCollection.CountDocumentsAsync(specification.Filter ?? (@base => true), cancellationToken: cancellationToken);
    }
}