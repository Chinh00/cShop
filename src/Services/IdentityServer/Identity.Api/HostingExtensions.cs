using Duende.IdentityServer.Configuration;

namespace Identity.Api;

internal static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        
        builder.Services.AddSchemaRegistry(builder.Configuration);
        builder.Services.AddKafkaConsumer<CustomerConsumerConfig>((config) =>
        {
            config.Topic = "customer_cdc_events";
            config.GroupId = "customer_cdc_events_identity_group";
            config.HandlePayload = async (ISchemaRegistryClient schemaRegistry,string eventName, byte[] payload) =>
            {
                return eventName switch
                {
                    nameof(CustomerCreatedIntegrationEvent) => await payload.AsRecord<CustomerCreatedIntegrationEvent>(
                        schemaRegistry),
                    _ => null
                };
            };
        }); 
        builder.Services.AddRazorPages();

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("Db")));
        builder.Services.AddTransient<IRepository<UserOutbox>, RepositoryBase<ApplicationDbContext, UserOutbox>>();
        

        builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddSignInManager<SigninManager>()
            .AddUserManager<UserManager>()
            .AddDefaultTokenProviders();

        builder.Services
            .AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
                
                // see https://docs.duendesoftware.com/identityserver/v6/fundamentals/resources/
                options.EmitStaticAudienceClaim = true;
            })
            .AddInMemoryIdentityResources(Config.IdentityResources)
            .AddInMemoryApiScopes(Config.ApiScopes)
            .AddInMemoryClients(Config.Clients(builder.Configuration))
            .AddTestUsers(Config.TestUsers)
            .AddAspNetIdentity<ApplicationUser>()
            .AddDeveloperSigningCredential();
        builder.Services.ConfigureApplicationCookie(options => { options.Cookie.SameSite = SameSiteMode.Lax; });
        builder.Services.AddAuthentication()
            .AddGoogle(options =>
            {
                options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

                // register your IdentityServer with Google at https://console.developers.google.com
                // enable the Google+ API
                // set the redirect URI to https://localhost:5001/signin-google
                options.ClientId = "";
                options.ClientSecret = "";
            });
        
        
        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.UseCors("Cors");
        app.UseSerilogRequestLogging();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseStaticFiles();
        app.UseRouting();
        app.UseIdentityServer();
        app.UseAuthorization();

        app.MapRazorPages()
            .RequireAuthorization();
        app.Use(async (context, next) =>
        {
            context.Response.Headers["Content-Security-Policy"] =
                "default-src 'self'; connect-src *;";
            await next();
        });

        return app;
    }
}