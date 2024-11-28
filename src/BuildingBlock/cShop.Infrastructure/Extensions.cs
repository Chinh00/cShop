using System.Text.Json;
using cShop.Core.Domain;
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

    public static TQuery GetQuery<TQuery>(this HttpContext context, string query)
        where TQuery : IQuery<IResult>, new()
    {

        TQuery queryObject = new ();
        if (!string.IsNullOrWhiteSpace(query) || query == "{}")
        {
            queryObject = JsonSerializer.Deserialize<TQuery>(query);
        }
        else
        {
            context.Response.Headers.Add("x-query", JsonSerializer.Serialize("{}", new JsonSerializerOptions()
            {
                
                PropertyNameCaseInsensitive = true
            }));

        }
        
        return queryObject;
        
    }
    
}