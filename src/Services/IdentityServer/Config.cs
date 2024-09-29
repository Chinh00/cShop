using Duende.IdentityServer.Models;
using Duende.IdentityServer.Test;

namespace IdentityServer;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        { 
            new IdentityResources.OpenId()
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

                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },

                AllowedScopes = { "api" },
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