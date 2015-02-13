using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Http.SelfHost;

namespace SelfHost
{
    class Program
    {
        static void Main(string[] args)
        {
            //Assembly wbAPI = Assembly.Load("WebApi, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");

            //Console.WriteLine(wbAPI.CodeBase);

            //HttpSelfHostConfiguration configuration = new HttpSelfHostConfiguration("http://localhost/selfhost");

            //using (HttpSelfHostServer httpServer = new HttpSelfHostServer(configuration))
            //{
            //    httpServer.Configuration.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional });

            //    httpServer.OpenAsync();
            //    Console.Read();
            //}

            HttpSelfHostConfiguration configuration = new HttpSelfHostConfiguration("http://localhost/selfhost");
            using (HttpSelfHostServer httpServer = new HttpSelfHostServer(configuration))
            {
                httpServer.Configuration.Services.Replace(typeof(IAssembliesResolver), new ExtendedDefaultAssembliesResolver());
                //其他操作
                httpServer.Configuration.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });
                httpServer.OpenAsync();
                Console.Read();
            }

            //Assembly.Load("WebApi, Version=1.0.0.0, Culture=neutral,  PublicKeyToken=null");
            //HttpSelfHostConfiguration configuration = new HttpSelfHostConfiguration("http://localhost/selfhost");
            //using (HttpSelfHostServer httpServer = new HttpSelfHostServer(configuration))
            //{
            //    httpServer.Configuration.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional });

            //    httpServer.OpenAsync();

            //    Console.Read();
            //}
        }
    }
}
