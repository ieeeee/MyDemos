using DM.Common.libs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Http.SelfHost;
using WF_WEB_API;

namespace WF_CONSOLE_HOST_V2
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //指定聆听的URL
                string StrHostURL = System.Configuration.ConfigurationManager.AppSettings["services_host"];

                HttpSelfHostConfiguration configuration = new HttpSelfHostConfiguration(StrHostURL);

                //注意: 在Vista, Win7/8，预设需以管理者权限执行才能系结到指定URL，否则要透过以下指令授权
                //开放授权 netsh http add urlacl url=http://+:32767/ user=machine\username
                //移除权限 netsh http delete urlacl url=http://+:32767/

                //设置WebApi支持跨域访问
                configuration.EnableCors();

                //设定路由
                configuration.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new { id = RouteParameter.Optional });

                //设定Self-Host Server，由于会使用到网络资源，用using确保会Dispose()加以释放
                using (HttpSelfHostServer httpServer = new HttpSelfHostServer(configuration))
                {
                    //Load WebApi Assemblies
                    httpServer.Configuration.Services.Replace(typeof(IAssembliesResolver), new ExtendedDefaultAssembliesResolver());

                    //OpenAsync()属异步呼叫，加上Wait()则等待开启完成才往下执行
                    httpServer.OpenAsync().Wait();

                    Lgr.Log.Info("Web API Host Started...");

                    //输入exit按Enter结束httpServer
                    string line = null;
                    do
                    {
                        line = Console.ReadLine();
                    } while (line != "exit");

                    //结束联机
                    httpServer.CloseAsync().Wait();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
