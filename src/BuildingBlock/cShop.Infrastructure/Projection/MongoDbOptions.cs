namespace cShop.Infrastructure.Projection;

public class MongoDbOptions
{
    public const string MongoDb = "MongoDb";

    public string? Username { get; set; } = default!;
    
    public string Password { get; set; }
    
    public string DatabaseName { get; set; }
    
    public string CollectionName { get; set; }
    
    public string ServerName { get; set; } = "localhost";
    
    public int Port { get; set; } = 27017;

    public override string ToString()
    {
        return $"mongodb://{Username}:{Password}@{ServerName}:{Port}/{DatabaseName}?authSource=admin";
    }
}