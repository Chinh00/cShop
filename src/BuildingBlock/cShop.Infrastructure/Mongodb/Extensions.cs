using cShop.Core.Repository;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace cShop.Infrastructure.Mongodb;

public static class Extensions
{
    public static IServiceCollection AddMongoDb(this IServiceCollection services, IConfiguration configuration, Type? serviceType = null,
        Action<IServiceCollection>? action = null)
    {
        BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
        services.Configure<MongoDbbOption>(configuration.GetSection(MongoDbbOption.Mongodb));
        
        if (serviceType != null) services.Scan(c =>
            c.FromAssembliesOf(serviceType).AddClasses(e => e.AssignableTo(typeof(IMongoRepository<>)))
                .AsImplementedInterfaces().WithTransientLifetime());
        action?.Invoke(services);
        return services;
    }
    
}