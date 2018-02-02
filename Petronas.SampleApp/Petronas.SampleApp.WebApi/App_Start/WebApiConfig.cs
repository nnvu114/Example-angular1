using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;

namespace Petronas.SampleApp.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.EnableCors();
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                "WithActionApi",
                "api/{controller}/{action}/{id}",
                new { action = "WithActionApi", id = System.Web.Http.RouteParameter.Optional }
                );

            config.Routes.MapHttpRoute(
                "DefaultApi",
                "api/{controller}/{id}",
                new { action = "DefaultAction", id = System.Web.Http.RouteParameter.Optional }
                );

            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}
