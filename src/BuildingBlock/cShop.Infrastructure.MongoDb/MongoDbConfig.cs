namespace cShop.Infrastructure.MongoDb;

public class MongoDbConfig
{
    public string ServerName { get; set; } = "localhost";
    public int Port { get; set; }
    public string? Username { get; set; }
    public string Password { get; set; }
    public string CollectionName { get; set; }

    public override string ToString()
    {
        if (Username is null)
        {
            return $"mongodb://{ServerName}:{Port}";
        }

        return "";
    }
}