using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace WF_WEB_API_HOST
{
    public class WebAPIConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //设置WebApi支持跨域访问
            config.EnableCors();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });
        }
    }
}