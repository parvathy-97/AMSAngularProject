using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace AMSAngularProject
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.EnableCors(new EnableCorsAttribute("http://localhost:4200", "*", "*"));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );


            /*config.Formatters.JsonFormatter.SupportedMediaTypes
                .Add(new System.Net.Http.Headers
                   .MediaTypeHeaderValue("text/html"));

               config.Formatters.JsonFormatter
                   .SerializerSettings
                   .DateTimeZoneHandling = DateTimeZoneHandling.Utc;*/

            var json = config.Formatters.JsonFormatter;
           json.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
           config.Formatters.Remove(config.Formatters.XmlFormatter);
           
        }
    }
}
