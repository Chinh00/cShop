using Application;
using cShop.Infrastructure.Auth;
using cShop.Infrastructure.Logging;
using cShop.Infrastructure.Mediator;
using cShop.Infrastructure.SchemaRegistry;
using cShop.Infrastructure.Swagger;
using cShop.Infrastructure.Validation;
using Infrastructure;
using WebApi.Apis;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddLoggingCustom(builder.Configuration, "search-service")
    .AddAuthenticationDefault(builder.Configuration)
    .AddMediatorDefault([typeof(Anchor)])
    .AddValidation(typeof(Anchor))
    .AddSwaggerCustom()
    .AddSchemaRegistry(builder.Configuration)
    .AddCdcConsumer()
    .AddElasticSearchService(builder.Configuration);
var app = builder.Build();

app.NewVersionedApi("Search").MapSearchApiV1();
app.UseAuthenticationDefault().ConfigureSwagger(builder.Configuration);
app.Run();

