using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace DM.Common.libs
{
    /// <summary>
    /// 日志记录类，封闭log4net的ILog。主要是在此类进行自动初始化。
    /// </summary>
    public class Lgr
    {
        /// <summary>
        /// 获取在配置文件中的log4net配置对象
        /// </summary>
        public static ILog Log
        {
            get
            {
                return LogManager.GetLogger("DMer");
            }
        }
        /// <summary>
        /// 初始化log4net
        /// </summary>
        static Lgr()
        {
            log4net.Config.XmlConfigurator.Configure();
        }
    }
}
