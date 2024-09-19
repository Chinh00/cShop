namespace cShop.Infrastructure;

public static class Extensions
{
    public static IConfiguration GetConfiguration(Type anchor)
    {

        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .AddJsonFile("appsettings.Development.json")
            .Build();
        
        return configuration;
    }
}