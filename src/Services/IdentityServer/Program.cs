using IdentityServer;
using IdentityServer.Data;
using IdentityServer.Data.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

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
        .AddIdentity<User, IdentityRole<Guid>>()
        .AddEntityFrameworkStores<UserDbContext>()
        .AddUserManager<UserManager<User>>()
        .AddSignInManager<SignInManager<User>>()
        .AddDefaultTokenProviders();

    builder.Services.AddHostedService<SeedData>();
    
    
    var app = builder
        .ConfigureServices()
        .ConfigurePipeline();
    
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