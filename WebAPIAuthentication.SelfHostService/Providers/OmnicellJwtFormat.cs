using System;
using System.Configuration;
using System.IdentityModel.Tokens;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using OcsAuthServer.Infrastructure;

namespace OcsAuthServer.Providers
{
    public class OmnicellJwtFormat : ISecureDataFormat<AuthenticationTicket>
    {
        private const string issuer = "Omnicell";
        
        public string Protect(AuthenticationTicket data)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }

            string audienceId = data.Properties.Dictionary["AudienceId"];
            string symmetricKeyAsBase64 = data.Properties.Dictionary["AudienceSecret"];
        
            var keyByteArray = TextEncodings.Base64Url.Decode(symmetricKeyAsBase64);
            var signingKey = new HmacSigningCredentials(keyByteArray);
            //var signingKey = new SigningCredentials(
            //                       new InMemorySymmetricSecurityKey(keyByteArray),
            //                       signatureAlgorithm: "http://www.w3.org/2001/04/xmldsig-more#hmac-sha256",
            //                       digestAlgorithm: "http://www.w3.org/2001/04/xmlenc#sha256");

            var issued = data.Properties.IssuedUtc;

            var expires = data.Properties.ExpiresUtc;

            var token = new JwtSecurityToken(issuer, audienceId, data.Identity.Claims, issued.Value.UtcDateTime, expires.Value.UtcDateTime, signingKey);

            var handler = new JwtSecurityTokenHandler();

            var jwt = handler.WriteToken(token);

            return jwt;
        }

        public AuthenticationTicket Unprotect(string protectedText)
        {
            throw new NotImplementedException();
        }
    }
}
