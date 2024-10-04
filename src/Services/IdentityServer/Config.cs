using Duende.IdentityServer.Models;
using Duende.IdentityServer.Test;

namespace IdentityServer;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        { 
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope(name: "api", displayName: "cShop api") 
            
        };
    

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            new Client
            {
                ClientId = "client",

                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                
                RequireClientSecret = false,

                AllowedScopes = { "openid", "api", "offline_access" },
                AllowOfflineAccess = true
            }
        };

    public static IEnumerable<TestUser> Users => new[]
    {
        new TestUser()
        {
            SubjectId = Guid.NewGuid().ToString(),
            Username = "admin", 
            Password = "password"
        },
    };
}