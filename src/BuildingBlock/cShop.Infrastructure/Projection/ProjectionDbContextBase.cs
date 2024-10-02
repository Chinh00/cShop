using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace cShop.Infrastructure.Projection;

public class ProjectionDbContextBase : IProjectionDbContext
{

    private readonly IMongoDatabase _mongoDatabase;

    public ProjectionDbContextBase(IOptions<MongoDbOptions> options)
    {
        var mongoClient = new MongoClient(options.Value.ToString());
        _mongoDatabase = mongoClient.GetDatabase(options.Value.DatabaseName);
    }

    public IMongoCollection<T> GetCollection<T>() => _mongoDatabase.GetCollection<T>(typeof(T).Name);
}