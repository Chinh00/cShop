using Application;
using cShop.Infrastructure.Auth;
using cShop.Infrastructure.Logging;
using cShop.Infrastructure.Mediator;
using cShop.Infrastructure.Mongodb;
using cShop.Infrastructure.Ole;
using cShop.Infrastructure.SchemaRegistry;
using cShop.Infrastructure.Swagger;
using cShop.Infrastructure.Validation;
using Infrastructure.Dtos;
using Infrastructure.Mongo;
using WebApi.Apis;
using WebApi.Hub;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddLoggingCustom(builder.Configuration, "comment-service")
    .AddAuthenticationDefault(builder.Configuration)
    .AddMongoDb(builder.Configuration, typeof(MongoRepository<>))
    .AddAutoMapper(typeof(MapperConfig))
    .AddSwaggerCustom()
    .AddMediatorDefault([typeof(Anchor)])
    .AddValidation(typeof(Anchor))
    .AddSchemaRegistry(builder.Configuration)
    .AddOpenTelemetryCustom(builder.Configuration, "comment-service")
    .AddSignalR();
    

var app = builder.Build();
app.NewVersionedApi("Comments").MapCommentApiV1();
app.UseRouting();
app.MapHub<CommentHub>("/hubs/comments");
app.UseAuthenticationDefault().ConfigureSwagger(builder.Configuration);

app.Run();

