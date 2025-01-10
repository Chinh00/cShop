using Asp.Versioning;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace cShop.Infrastructure.Swagger;

public static class Extensions
{
    public static IServiceCollection AddSwaggerCustom(this IServiceCollection services,
        Action<IServiceCollection>? action = null)
    {

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddApiVersioning(options =>
        {
            options.ReportApiVersions = true;
            options.DefaultApiVersion = new ApiVersion(1);
        })
        .AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerGenOption>();
        
        
        
        
        action?.Invoke(services);
        return services;
    }

    public static WebApplication ConfigureSwagger(this WebApplication app, IConfiguration configuration, Action<WebApplication>? action = null)
    {

        app.UseSwagger();
        app.UseSwaggerUI(option =>
        {
            foreach (var apiVersionDescription in app.DescribeApiVersions())
            {
                var url = $"/swagger/{apiVersionDescription.GroupName}/swagger.json";
                var name = $"v{apiVersionDescription.ApiVersion}";
                
                option.SwaggerEndpoint(url, name);
            }
        });
        
        app.MapFallback("/", () => Results.Redirect("/swagger"));
        
        
        action?.Invoke(app);
        return app;
    }
}