namespace cShop.Infrastructure.Mongodb;

public static class Extensions
{
    public static IServiceCollection AddMongoDb(this IServiceCollection services, IConfiguration configuration,
        Action<IServiceCollection>? action = null)
    {
        services.Configure<MongoDbbOption>(configuration.GetSection(MongoDbbOption.Mongodb));
        action?.Invoke(services);
        return services;
    }
}