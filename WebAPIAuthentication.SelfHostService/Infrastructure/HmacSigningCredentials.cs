using System;
using System.IdentityModel.Tokens;

namespace OcsAuthServer.Infrastructure
{
    public class HmacSigningCredentials : SigningCredentials
    {
        private const string HmacSha256Signature = "http://www.w3.org/2001/04/xmldsig-more#hmac-sha256";
        private const string HmacSha384Signature = "http://www.w3.org/2001/04/xmldsig-more#hmac-sha384";
        private const string HmacSha512Signature = "http://www.w3.org/2001/04/xmldsig-more#hmac-sha512";

        private const string Sha256Digest = "http://www.w3.org/2001/04/xmlenc#sha256";
        private const string Sha384Digest = "http://www.w3.org/2001/04/xmlenc#sha384";
        private const string Sha512Digest = "http://www.w3.org/2001/04/xmlenc#sha512";

        public HmacSigningCredentials(string base64EncodedKey)
            : this(Convert.FromBase64String(base64EncodedKey))
        { }

        public HmacSigningCredentials(byte[] key)
            : base(new InMemorySymmetricSecurityKey(key),
                CreateSignatureAlgorithm(key),
                CreateDigestAlgorithm(key))
        { }

        protected static string CreateSignatureAlgorithm(byte[] key)
        {
            switch (key.Length)
            {
                case 32:
                    return HmacSha256Signature;
                case 48:
                    return HmacSha384Signature;
                case 64:
                    return HmacSha512Signature;
                default:
                    throw new InvalidOperationException("Unsupported key lenght");
            }
        }

        protected static string CreateDigestAlgorithm(byte[] key)
        {
            switch (key.Length)
            {
                case 32:
                    return Sha256Digest;
                case 48:
                    return Sha384Digest;
                case 64:
                    return Sha512Digest;
                default:
                    throw new InvalidOperationException("Unsupported key length");
            }
        }
    }
}