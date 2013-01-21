using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace UserApi
{
    public static class WebApiConfig
    {
        const string API_BASE_PATH = "api1/v5/";

        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: API_BASE_PATH + "{controller}/{action}"
            );
        }
    }
}
