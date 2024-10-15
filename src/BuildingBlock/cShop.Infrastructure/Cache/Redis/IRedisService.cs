namespace cShop.Infrastructure.Cache.Redis;

public interface IRedisService
{
   Task<T> HashGetOrSetAsync<T>(string key, string keyField, Func<Task<T>> value, CancellationToken cancellationToken );
   Task<T> HashSetAsync<T>(string key, string keyField, Func<Task<T>> value, CancellationToken cancellationToken );
   Task<T> HashGetAsync<T>(string key, string keyField, CancellationToken cancellationToken );
   
   Task<bool> HashRemoveAsync(string key, string keyField, CancellationToken cancellationToken);
   
}