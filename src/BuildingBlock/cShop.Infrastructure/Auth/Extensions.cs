using cShop.Infrastructure.IdentityServer;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace cShop.Infrastructure.Auth;

public static class Extensions
{
    private const string Cors = "Cros";
    
    public static IServiceCollection AddAuthenticationDefault(this IServiceCollection services, IConfiguration configuration,
        Action<IServiceCollection>? action = null)
    {

        services.AddHttpContextAccessor();
        services.AddScoped<IClaimContextAccessor, ClaimContextAccessor>();
        services.AddCors(options =>
        {
            options.AddPolicy(Cors, builder =>
            {
                builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
            });
        });
        
        services.AddAuthentication().AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
        {
            options.Authority = configuration.GetValue<string>("IdentityServer:Url");
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters.ValidateIssuer = false;
            options.TokenValidationParameters.ValidateAudience = false;
        });
        services.AddAuthorization();
        
        action?.Invoke(services);
        return services;
    }


    public static WebApplication UseAuthenticationDefault(this WebApplication app, IConfiguration configuration, Action<WebApplication>? action = null)
    {

        app.UseCors(Cors);
        
        app.UseAuthentication();
        app.UseAuthorization();
        
        action?.Invoke(app);
        
        return app;
    }
}