namespace cShop.Infrastructure.Mediator;

public static class Extensions
{
    public static IServiceCollection AddMediatorDefault(this IServiceCollection services, Type[] type,
        Action<IServiceCollection>? action = null)
    {

        services.AddMediatR(e => e.RegisterServicesFromAssemblies(type.Select(t => t.Assembly).ToArray()));
        action?.Invoke(services);
        return services;
    }
}