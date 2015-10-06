using System;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;
using OcsAuthServer.Models;
using OcsAuthServer.Providers;

namespace OcsAuthServer.Infrastructure
{
    public class AuthorizationServerConfigurator
    {
        public static OAuthAuthorizationServerOptions GetAuthorizationServerOptions()
        {
            return new OAuthAuthorizationServerOptions()
            {
                //For Dev enviroment only (on production should be AllowInsecureHttp = false)
                AllowInsecureHttp = true, //TODO need research.
                TokenEndpointPath = new PathString("/oauth/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new OmnicellOAuthProvider(new AudienceRepository()),
                AccessTokenFormat = new OmnicellJwtFormat()
            };
        }

        public static JwtBearerAuthenticationOptions JwtBearerAuthenticationOptions(string issuer, string audienceId, string audienceSecret)
        {
            return new JwtBearerAuthenticationOptions
            {
                AuthenticationMode = AuthenticationMode.Active,
                AllowedAudiences = new[] {audienceId},
                IssuerSecurityTokenProviders = new IIssuerSecurityTokenProvider[]
                {
                    new SymmetricKeyIssuerSecurityTokenProvider(issuer, TextEncodings.Base64Url.Decode(audienceSecret))
                }
            };
        }

    }
}