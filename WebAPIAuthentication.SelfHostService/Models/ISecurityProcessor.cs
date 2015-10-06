using System;
using System.Collections.Generic;
using System.Security.Claims;


namespace OcsAuthServer.Models
{
    public interface ISecurityProcessor : IDisposable
    {
        /// <summary>
        /// Authenticates user
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <param name="encryptedPassword">Encrypted password</param>
        /// <returns>Returns a response of type <see cref="AuthenticatedUser"/> with user and claims info</returns>
        AuthenticatedUser AuthenticateUser(string userId, string encryptedPassword);
    }

    public class SecurityProcessor : ISecurityProcessor
    {
        public AuthenticatedUser AuthenticateUser(string userId, string encryptedPassword)
        {
            return new AuthenticatedUser()
            {
                UserId = "10",
                UserName = "OmniTech",
                Claims = new List<Claim>() {new Claim("InstallId", "CPC01")}
            };
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}