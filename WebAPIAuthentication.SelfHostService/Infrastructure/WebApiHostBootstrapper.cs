using System.Configuration;
using System.Web.Http;
using OcsAuthServer.Models;
using Owin;

namespace OcsAuthServer.Infrastructure
{
    public class WebApiHostBootstrapper
    {
        // This method is required by Katana:
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext<ISecurityProcessor>(() => new SecurityProcessor());
            var webApiConfiguration = ConfigureWebApi();

            app.UseOAuthAuthorizationServer(AuthorizationServerConfigurator.GetAuthorizationServerOptions());
            app.UseJwtBearerAuthentication(AuthorizationServerConfigurator.JwtBearerAuthenticationOptions("Omnicell",
                ConfigurationManager.AppSettings["AudienceId"],
                ConfigurationManager.AppSettings["AudienceSecret"]));

            app.UseWebApi(webApiConfiguration);
        }

        private HttpConfiguration ConfigureWebApi()
        {
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                "DefaultApi",
                "api/{controller}/{name}",
                new {name = RouteParameter.Optional});
            return config;
        }
    }
}