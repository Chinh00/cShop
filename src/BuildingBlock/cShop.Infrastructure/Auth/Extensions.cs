using cShop.Infrastructure.IdentityServer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.JsonWebTokens;

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
            options.TokenValidationParameters.SignatureValidator = (token, parameters) => new JsonWebToken(token);
            options.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    var accessToken = context.Request.Query["access_token"];

                    if (!string.IsNullOrEmpty(accessToken))
                        context.Token = accessToken;
                    return Task.CompletedTask;
                }
            };
        });
        services.AddAuthorization();
        
        action?.Invoke(services);
        return services;
    }


    public static WebApplication UseAuthenticationDefault(this WebApplication app, Action<WebApplication>? action = null)
    {

        app.UseCors(Cors);
        
        app.UseAuthentication();
        app.UseAuthorization();
        
        action?.Invoke(app);
        
        return app;
    }
}