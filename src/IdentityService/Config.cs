using Duende.IdentityServer.Models;

namespace IdentityService
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            [
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            ];

        public static IEnumerable<ApiScope> ApiScopes =>
            [
                new ApiScope("auctionApp", "Auction app full access")
            ];

        public static IEnumerable<Client> Clients =>
            [
                // interactive client using code flow + pkce
                new Client
                {
                    ClientId = "postman",
                    ClientName = "postman",
                    ClientSecrets = { new Secret("NotASecret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    RedirectUris = { "https://getpostman.com/oauth2/callback" },
                    AllowedScopes = { "openid", "profile", "auctionApp" }
                },
                new Client
                {
                    ClientId = "nextApp",
                    ClientName = "nextApp",
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
                    RequirePkce = false,
                    RedirectUris = { "http://localhost:3000/api/callback/id-server" },
                    AllowOfflineAccess = true,
                    AllowedScopes = { "openid", "profile", "auctionApp" },
                    AccessTokenLifetime = 3600 * 24 * 30
                }
            ];
    }
}
