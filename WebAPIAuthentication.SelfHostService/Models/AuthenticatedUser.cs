using System.Collections.Generic;
using System.Security.Claims;

namespace OcsAuthServer.Models
{
    public class AuthenticatedUser
    {
        
        public string UserId { get; set; }

      
        public string UserName { get; set; }

     
        public IList<Claim> Claims { get; set; }
        
    }
}