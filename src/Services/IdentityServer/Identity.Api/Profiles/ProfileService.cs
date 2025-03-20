using System.Security.Claims;
using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;

namespace Identity.Api.Profiles;

public class ProfileService : IProfileService
{
    private readonly UserManager _userManager;

    public ProfileService(UserManager userManager)
    {
        _userManager = userManager;
    }

    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        var userId = context.Subject.GetSubjectId();
        var user = await _userManager.FindByIdAsync(userId);
        if (user is not null)
        {
            var claims = new List<Claim>
            {
                new("avatar", user?.AvatarUrl ?? ""),
                new ("username", user?.UserName ?? "")
            };
            context.IssuedClaims.AddRange(claims);
        }
        

    }

    public Task IsActiveAsync(IsActiveContext context)
    {
        context.IsActive = true;
        return Task.CompletedTask;
    }
}