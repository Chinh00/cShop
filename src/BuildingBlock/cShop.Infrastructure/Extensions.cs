using cShop.Core.Domain;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

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
        where TQuery : new()
    {

        TQuery queryObject = new ();
        if (!string.IsNullOrWhiteSpace(query) || query == "{}")
        {
            queryObject = JsonConvert.DeserializeObject<TQuery>(query);
        }
        context.Response.Headers.Append("x-query", JsonConvert.SerializeObject(queryObject,
            new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }));
        
        return queryObject;
        
    }
    
}