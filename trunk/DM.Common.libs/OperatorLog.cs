using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.Common.libs
{
    public class OperatorLog
    {
        private static readonly object lockObj = new object();

        public static void WriteLog(string fileName, string content)
        {
            string path = ConfigurationManager.AppSettings["OperatorLog"].ToString();

            path = string.IsNullOrWhiteSpace(path) ? string.Format(@"{0}\logs\", Environment.CurrentDirectory) : path;

            string today = DateTime.Now.ToString("yyyyMMdd");

            lock (lockObj)
            {
                string logfilename = string.Format("{0}_{1}.log", fileName, today);

                using (StreamWriter sw = new StreamWriter(Path.Combine(path, logfilename), true, System.Text.Encoding.UTF8))
                {
                    sw.WriteLine("时间:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " 内容:" + content);
                }
            }
        }
        public static void WritePreviewLog(string content)
        {
            WriteLog("Preview", content);
        }
        public static void WriteDownloadLog(string content)
        {
            WriteLog("Download", content);
        }
        public static void WriteModifyAttLog(string content)
        {
            WriteLog("ModifyAtt", content);
        }
    }
}
