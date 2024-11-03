namespace cShop.Infrastructure.Cdc;

public static class Extensions
{
    public static IServiceCollection AddKafkaConsumer(this IServiceCollection services,
        Action<ConsumerConfig> action)
    {

        services.AddOptions<ConsumerConfig>().BindConfiguration(ConsumerConfig.Name).Configure(action);
        services.AddHostedService<BackgroundConsumerService>();
        
        return services;
    }
}