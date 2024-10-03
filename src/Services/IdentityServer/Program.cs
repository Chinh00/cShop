using cShop.Infrastructure.Ole;
using IdentityServer;
using IdentityServer.Apis;
using IdentityServer.Data;
using IdentityServer.Data.Domain;
using IdentityServer.Infrastructure;
using Microsoft.AspNetCore.Identity;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("Starting up");




try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((ctx, lc) => lc
        .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
        .Enrich.FromLogContext()
        .ReadFrom.Configuration(ctx.Configuration));

    

    builder.Services.AddDbContext<UserDbContext>((provider, optionsBuilder) =>
    {
        optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("db"));    
    });

    builder.Services
        .AddSwaggerCustom(builder.Configuration)
        .AddMediatorDefault([typeof(Program)])
        .AddMessageBus(builder.Configuration)
        .AddMasstransitCustom(builder.Configuration)
        .AddHostedService<SeedData>()
        ;
    
    builder.Services
        .AddOpenTelemetryCustom("IdentityService")
        .AddIdentity<User, IdentityRole<Guid>>()
        .AddEntityFrameworkStores<UserDbContext>()
        .AddUserManager<UserManager<User>>()
        .AddSignInManager<SignInManager<User>>()
        .AddDefaultTokenProviders();


    
    var app = builder
        .ConfigureServices()
        .ConfigurePipeline()
        .MapIdentityServerApiV1()
        .ConfigureSwagger(builder.Configuration);
    
    app.Run();


}
catch (Exception ex)
{
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}