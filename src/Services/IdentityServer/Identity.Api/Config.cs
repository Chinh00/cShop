﻿using Duende.IdentityServer.Models;
using Duende.IdentityServer.Test;

namespace Identity.Api;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
    [
        new IdentityResources.OpenId(),
            new IdentityResources.Profile()
    ];

    public static IEnumerable<ApiScope> ApiScopes =>
    [
        new ApiScope("api"),
    ];

    public static IEnumerable<Client> Clients(IConfiguration config) =>
    [
        new Client
            {
                ClientId = "nextjs",
                ClientSecrets = { new Secret("secret".Sha256()) },

                AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
                AllowedCorsOrigins= { config.GetValue<string>("Nextjs:Domain") ?? "http://localhost:3000"},
                RedirectUris = { $"{config.GetValue<string>("Nextjs:Domain")}/api/auth/callback/oidc" },
                RequirePkce = false,
                AllowOfflineAccess = true,
                AllowedScopes = { "openid", "profile", "api" },
                AlwaysIncludeUserClaimsInIdToken = true
            }
    ];
    public static List<TestUser> TestUsers =>
    [
        new TestUser()
        {
            SubjectId = "1casc",
            Username = "Administrator",
            Password = "Pass123@@@",
        }
    ];
}