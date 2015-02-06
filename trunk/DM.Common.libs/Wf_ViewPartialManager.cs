using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.IO;
using System.Web;

namespace DM.Common.libs
{
    /// <summary>
    /// 异步加载用户控件管理
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Wf_ViewPartialManager<T> where T : UserControl
    {
        private Page m_pageHolder;

        public T LoadViewControl(string path)
        {
            this.m_pageHolder = new Page();
            return (T)this.m_pageHolder.LoadControl(path);
        }

        public string RenderView(T control)
        {
            StringWriter output = new StringWriter();
            this.m_pageHolder.Controls.Add(control);
            HttpContext.Current.Server.Execute(this.m_pageHolder, output, false);
            return output.ToString();
        }
    }
}
