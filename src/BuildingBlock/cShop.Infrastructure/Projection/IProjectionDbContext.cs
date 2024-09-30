using MongoDB.Driver;

namespace cShop.Infrastructure.Projection;

public interface IProjectionDbContext
{
    public IMongoCollection<T> GetCollection<T>();
}