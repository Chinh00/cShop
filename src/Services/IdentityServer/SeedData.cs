using IdentityServer.Data.Domain;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer;

public class SeedData : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public SeedData(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var dbContextUserManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

        if (!dbContextUserManager.Users.Any())
        {
            
            for (var i = 0; i < 10; i++)
            {
                await dbContextUserManager.CreateAsync(new User()
                {
                    UserName = $"admin{i}",
                    Email = "admin@admin.com",
                }, "1231231234");
            }
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}