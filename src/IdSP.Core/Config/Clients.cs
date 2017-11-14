using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdSP.Core.Config
{
    public class Clients
    {
        public static IEnumerable<Client> Get()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "client",

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    // scopes that client has access to
                    AllowedScopes = { "api1" }
                },
                // resource owner password grant client
                new Client
                {
                    ClientId = "ro.client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = { "api1" }
                },
                new Client
                {
                    ClientId = "mvc.hybrid",
                    ClientName = "MVC Hybrid Client",

                    LogoUri = "https://raw.githubusercontent.com/manfredsteyer/angular-oauth2-oidc/master/oidc.png",

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.Hybrid,
                    AllowAccessTokensViaBrowser = false,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    // where to redirect to after login
                    RedirectUris =
                    {
                        "http://localhost:5002/signin-oidc"
                    },

                    // where to redirect to after logout
                    PostLogoutRedirectUris =
                    {
                        "http://localhost:5002/signout-callback-oidc"
                    },

                    AllowOfflineAccess = false,
                    AlwaysSendClientClaims = true,
                    // scopes that client has access to
                    AllowedScopes =
                    {
                        "api1",
                        IdentityServer4.IdentityServerConstants.StandardScopes.OpenId,
//                        IdentityServer4.IdentityServerConstants.StandardScopes.OfflineAccess,
                        IdentityServer4.IdentityServerConstants.StandardScopes.Profile,
                        IdentityServer4.IdentityServerConstants.StandardScopes.Phone,
                        IdentityServer4.IdentityServerConstants.StandardScopes.Email

                    }


                }
            };
        }
    }
}
