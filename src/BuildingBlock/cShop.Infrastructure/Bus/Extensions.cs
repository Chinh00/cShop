using cShop.Infrastructure.Bus.Kafka;

namespace cShop.Infrastructure.Bus;

public static class Extensions
{
    
    

    public static IServiceCollection AddKafkaConsumer(this IServiceCollection services, Action<KafkaConsumerConfig> action)
    {
       
        
        
        return services;
    }
}