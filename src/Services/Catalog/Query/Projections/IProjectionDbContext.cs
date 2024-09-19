using MongoDB.Driver;

namespace Projections;

public interface IProjectionDbContext
{
    public IMongoCollection<T> GetCollection<T>();
}