using System.Web.Http;
using System.Web.Http.Cors;

namespace TaskMaster_Backend
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
           
            config.Formatters.Remove(config.Formatters.XmlFormatter);
        }
    }
}