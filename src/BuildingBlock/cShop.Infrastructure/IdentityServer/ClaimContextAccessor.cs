using System.Security.Claims;

namespace cShop.Infrastructure.IdentityServer;

public class ClaimContextAccessor : IClaimContextAccessor
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ClaimContextAccessor(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid GetUserId()
    {
        
        return Guid.Parse(_httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier));
        
        
    }

    public Guid GetUserMail()
    {
        throw new NotImplementedException();
    }
}
