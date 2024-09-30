using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace cShop.Infrastructure.Projection;

public static class Extensions
{
    public static IServiceCollection AddProjections(this IServiceCollection services, IConfiguration configuration, Action<IServiceCollection>? action = null)
    {

        services.Configure<MongoDbOptions>(configuration.GetSection(MongoDbOptions.MongoDb));
        services.AddScoped<IProjectionDbContext, ProjectionDbContext>();
        services.AddScoped(typeof(IProjectionRepository<>), typeof(ProjectionRepository<>));
        BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
        action?.Invoke(services);
        return services;
    }
    
    
    
}