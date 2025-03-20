namespace cShop.Infrastructure.IdentityServer;

public interface IClaimContextAccessor
{
    Guid GetUserId();

    Guid GetUserMail();
    string GetAvatar();
    string GetUsername();
}