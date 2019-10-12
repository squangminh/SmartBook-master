using IdentityServer4;
using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdwardSoft.API.Authentication.Configurations.IdentityServer
{
    public class Config
    {
        // scopes define the resources in your system
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("API.Core", "AdwardSoft API Core")
            };
        }
        public static IEnumerable<Client> GetClients(IConfiguration configuration)
        {
            return new List<Client>
    {
        new Client
        {
            ClientId = "Inside",
            ClientName = "AdwardSoft Inside",
            AllowedGrantTypes = GrantTypes.ClientCredentials,

           // RequireConsent = false,

            ClientSecrets =
            {
                new Secret(configuration["Client:Inside:Secret"].Sha256())
            },
            //RedirectUris = {configuration["Client:Inside:RedirectUris"].ToString()},
            //PostLogoutRedirectUris = {configuration["Client:Inside:PostLogoutRedirectUris"].ToString() },

            AllowedScopes =
            {
                //IdentityServerConstants.StandardScopes.OpenId,
               //  IdentityServerConstants.StandardScopes.Profile,
                "API.Core"
            },
            AllowOfflineAccess = true
        }
    };
        }
    }
}
