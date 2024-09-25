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
        
        return new Guid(_httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty);
        
        
    }

    public Guid GetUserMail()
    {
        throw new NotImplementedException();
    }
}
