using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace cShop.Infrastructure.Swagger;

public class ConfigureSwaggerGenOption : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _provider;

    public ConfigureSwaggerGenOption(IApiVersionDescriptionProvider provider)
    {
        _provider = provider;
    }


    public void Configure(SwaggerGenOptions options)
    {
        foreach (var providerApiVersionDescription in _provider.ApiVersionDescriptions)
        {
            var openApiInfo = new OpenApiInfo()
            {
                Title = providerApiVersionDescription.ApiVersion.ToString(),
                Version = providerApiVersionDescription.ApiVersion.ToString(),
            };
            options.SwaggerDoc(providerApiVersionDescription.GroupName, openApiInfo);
        }
        options.CustomSchemaIds(type => type.ToString().Replace("+", "."));
    }


  
}