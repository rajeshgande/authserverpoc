using System.Collections.Generic;
using System.Configuration;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using OcsAuthServer.Infrastructure;
using OcsAuthServer.Models;

namespace OcsAuthServer.Providers
{
    public class OmnicellOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly IAudienceRepository _audienceRepository;

        public OmnicellOAuthProvider(IAudienceRepository _audienceRepository)
        {
            this._audienceRepository = _audienceRepository;
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            return Task.FromResult<object>(null);
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            string audienceId = context.ClientId;
            if (string.IsNullOrEmpty(audienceId)) audienceId = ConfigurationManager.AppSettings["AudienceId"];
            string audienceSecret = _audienceRepository.GetAudienceSecret(audienceId);
            var allowedOrigin = "*";

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });

            var securityProcessor = context.OwinContext.Get<ISecurityProcessor>();
            
            var authenticatedUser =  securityProcessor.AuthenticateUser(context.UserName, context.Password);

            if (authenticatedUser == null)
            {
                context.SetError("invalid_grant", "The ApplicationUser name or password is incorrect.");
                return;
            }
            
            ClaimsIdentity oAuthIdentity = new ClaimsIdentity(authenticatedUser.Claims); 
            oAuthIdentity.AddClaim(new Claim("InstallId", "CPC01"));
            var ticket =  new AuthenticationTicket(oAuthIdentity, new AuthenticationProperties(new Dictionary<string, string>()));
            ticket.Properties.Dictionary.Add("AudienceId", audienceId);
            ticket.Properties.Dictionary.Add("AudienceSecret", audienceSecret);
            context.Validated(ticket);

        }

       
    }
}
