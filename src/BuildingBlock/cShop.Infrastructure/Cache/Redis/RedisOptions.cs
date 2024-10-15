namespace cShop.Infrastructure.Cache.Redis;

public record RedisOptions
{
    public int Port { get; set; } = 6379;
    public string Server { get; set; } = "localhost";

    public string GetConnectionString()
    {
        return $"{Server}:{Port}";
    }
}