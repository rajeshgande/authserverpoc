using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using OcsAuthServer.Models;

namespace OcsAuthServer.Controllers
{
   // [Authorize]
    public class TestController : ApiController
    {
        public string Get()
        {
            return "Hello Autheticated User";
        }
    }
}