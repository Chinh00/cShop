using System.Reflection;
using cShop.Infrastructure.Validation;
using MediatR;

namespace cShop.Infrastructure.Mediator;

public static class Extensions
{
    public static IServiceCollection AddMediatorDefault(this IServiceCollection services, Type[] type,
        Action<IServiceCollection>? action = null)
    {

        services.AddMediatR(e => e.RegisterServicesFromAssemblies(type.Select(t => t.Assembly).ToArray()))
             .AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestValidateBehaviorPipeline<,>));
        // services.AddMediatR(e => e.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()))
        //     .AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestValidateBehaviorPipeline<,>));
        action?.Invoke(services);
        return services;
    }
}