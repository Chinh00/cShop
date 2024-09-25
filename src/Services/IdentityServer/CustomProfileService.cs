using System.Security.Claims;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using IdentityServer.Data.Domain;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer;

public class CustomProfileService : IProfileService
{

    private readonly UserManager<User> _userManager;

    public CustomProfileService(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {

        var user = await _userManager.GetUserAsync(context.Subject);
        
        if (user is null) throw new Exception($"Unable to load user with ID '{context.Subject}'.");
        
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        };
        context.IssuedClaims.AddRange(claims);
    }

    public Task IsActiveAsync(IsActiveContext context) => Task.CompletedTask;
}