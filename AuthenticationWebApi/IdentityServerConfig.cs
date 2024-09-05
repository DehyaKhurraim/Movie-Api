using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Security.Claims;

namespace AuthenticationWebApi
{
    public static class IdentityServerConfig
    {

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "movieapi_client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = { "movieapi" }
                }
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("movieapi", "Movie API")
            };

    }
}
