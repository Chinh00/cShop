using cShop.Core.Domain;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Projections;

public class ProjectionDbContext : IProjectionDbContext
{

    private readonly IMongoDatabase _mongoDatabase;

    public ProjectionDbContext(IOptions<MongoDbOptions> options)
    {
        var mongoClient = new MongoClient(options.Value.ToString());
        _mongoDatabase = mongoClient.GetDatabase(options.Value.DatabaseName);
    }

    public IMongoCollection<T> GetCollection<T>() => _mongoDatabase.GetCollection<T>(typeof(T).Name);
}