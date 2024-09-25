namespace cShop.Infrastructure.IdentityServer;

public static class Extensions
{
    public static IServiceCollection AddIdentityServerCustom(this IServiceCollection services, IConfiguration configuration,
        Action<IServiceCollection>? action = null)
    {

        var identityUrl = configuration.GetValue<string>("IdentityServer");

        services.AddAuthentication().AddJwtBearer(options =>
        {
            options.Authority = identityUrl;
            options.TokenValidationParameters.ValidateAudience = false;
        });

        services.AddHttpContextAccessor();
        services.AddScoped<IClaimContextAccessor, ClaimContextAccessor>();
        
        action?.Invoke(services);
        return services;
    }
}