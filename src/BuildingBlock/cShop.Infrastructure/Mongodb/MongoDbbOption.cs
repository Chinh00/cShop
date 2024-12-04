namespace cShop.Infrastructure.Mongodb;

public class MongoDbbOption
{
    public const string Mongodb = "MongoDb";
    public string Username { get; set; } = "root";
    public string Password { get; set; } = "%40P%40ssw0rd02";
    public string DatabaseName { get; set; } = "DbName";
    public string ServerName { get; set; } = "localhost";
    public int Port { get; set; } = 27017;

    public override string ToString()
    {
        return $"mongodb://{Username}:{Password}@{ServerName}:{Port}/?authSource=admin";
    }
}