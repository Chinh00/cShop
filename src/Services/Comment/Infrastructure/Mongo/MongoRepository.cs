using cShop.Core.Domain;
using cShop.Infrastructure.Mongodb;
using Microsoft.Extensions.Options;

namespace Infrastructure.Mongo;

public class MongoRepository<TEntity>(IOptions<MongoDbbOption> options) : MongoRepositoryBase<TEntity>(options)
    where TEntity : MongoEntityBase;