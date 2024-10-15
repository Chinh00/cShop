using System.Text.Json;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace cShop.Infrastructure.Cache.Redis;

public class RedisService : IRedisService
{
    private RedisOptions _options;

    public RedisService(IOptions<RedisOptions> options)
    {
        _options = options.Value;
        _lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
            ConnectionMultiplexer.Connect(options.Value.GetConnectionString()));
    }
    private readonly Lazy<ConnectionMultiplexer> _lazyConnection;

    private readonly SemaphoreSlim _connectionLock = new SemaphoreSlim(1, 1);
    
    public ConnectionMultiplexer ConnectionMultiplexer => _lazyConnection.Value;

    public IDatabase Database
    {
        get
        {
            _connectionLock.Wait();

            try
            {
                return ConnectionMultiplexer.GetDatabase();
            }
            finally
            {
                _connectionLock.Release();
            }
        }
    }


    public async Task<T> HashGetOrSetAsync<T>(string key, string keyField, Func<Task<T>> value, CancellationToken cancellationToken)
    {
        var redisValue = await Database.HashGetAsync(key, keyField);
        if (redisValue.HasValue)
        {
            return JsonSerializer.Deserialize<T>(redisValue);
        }

        var val = await value();
        await Database.HashSetAsync(key, keyField, JsonSerializer.Serialize(val));
        return val;
    }

    public async Task<T> HashSetAsync<T>(string key, string keyField, Func<Task<T>> value, CancellationToken cancellationToken)
    {
        var val = await value();
        await Database.HashSetAsync(key, keyField, JsonSerializer.Serialize(val));
        return val;
    }

    public async Task<T> HashGetAsync<T>(string key, string keyField, CancellationToken cancellationToken)
    {
        return JsonSerializer.Deserialize<T>(await Database.HashGetAsync(key, keyField));
    }

    public async Task<bool> HashRemoveAsync(string key, string keyField, CancellationToken cancellationToken)
    {
        return await Database.HashDeleteAsync(key, keyField);
    }
}