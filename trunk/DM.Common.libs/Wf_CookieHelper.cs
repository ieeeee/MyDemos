using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace DM.Common.libs
{
    public class Wf_CookieHelper
    {
        #region  判断所查询的cookie是否存在,如果不存在则创建它
        /// <summary>
        /// 判断所传的cookie是否存在,如果不存在则创建它
        /// </summary>
        /// <param name="name">要检测的cookie名称</param>
        /// <param name="content">cookie的内容</param>
        /// <param name="dtime">cookie的有效时间</param>
        /// <returns>是否有此Cookie</returns>
        public static bool CheckCookie(string name, string content, DateTime dtime)
        {
            bool falg = false;
            HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies[name];
            if (cookie != null && !cookie.Value.ToString().Equals(""))
                falg = true;
            else
                falg = createCookie(name, content);
            return falg;
        }
        /// <summary>
        /// 判断所传的cookie是否存在,如果不存在则创建它并设它的过期时间为浏览器关闭
        /// </summary>
        /// <param name="name">要检测的cookie名称</param>
        /// <param name="content">cookie的内容</param>
        /// <returns>是否有此Cookie</returns>
        public static bool CheckCookie(string name, string content)
        {
            bool falg = false;
            HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies[name];
            if (cookie != null && !cookie.Value.ToString().Equals(""))
                falg = true;
            else
                falg = createCookie(name, content);
            return falg;
        }
        /// <summary>
        /// 判断所传的cookie是否存在
        /// </summary>
        /// <param name="name">要检测的cookie名称</param>
        /// <returns>是否有此Cookie</returns>
        public static bool CheckCookie(string name)
        {
            bool falg = false;
            HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies[name];
            if (cookie != null && !cookie.Value.ToString().Equals(""))
                falg = true;
            return falg;
        }
        #endregion

        #region 创建cookie
        /// <summary>
        /// 创建cookie
        /// </summary>
        /// <param name="name">cookie名称</param>
        /// <param name="content">cookie的内容</param>
        /// <param name="dtime">cookie的过期时间</param>
        /// <returns>是否创建成功</returns>
        public static bool createCookie(string name, string content, DateTime dtime)
        {
            bool falg = false;
            if (!name.Equals("") && !content.Equals("") && !dtime.Equals(""))
            {
                HttpContext.Current.Response.Cookies.Add(new HttpCookie(name, HttpUtility.UrlEncode(content)));
                HttpContext.Current.Response.Cookies[name].Expires = dtime;
                falg = true;
            }
            return falg;
        }
        /// <summary>
        /// 创建cookie
        /// </summary>
        /// <param name="name">cookie名称</param>
        /// <param name="content">cookie的内容</param>
        /// <returns>是否创建成功</returns>
        public static bool createCookie(string name, string content)
        {
            bool falg = false;
            if (!name.Equals("") && !content.Equals(""))
            {
                HttpContext.Current.Response.Cookies.Add(new HttpCookie(name, HttpUtility.UrlEncode(content)));
                falg = true;
            }
            return falg;
        }
        #endregion

        #region 修改cookie
        /// <summary>
        /// 修改cookie
        /// </summary>
        /// <param name="name">要修改的cookie名</param>
        /// <param name="content">cookie的内容</param>
        /// <param name="dtime">cookie的有效时间</param>
        /// <returns>是否修改成功</returns>
        public static bool updateCookie(string name, string content, DateTime dtime)
        {
            bool falg = false;
            if (!name.Equals("") && !content.Equals("") && !dtime.Equals(""))
            {
                try
                {
                    HttpCookie cookie = HttpContext.Current.Request.Cookies[name];
                    if (cookie != null)
                        falg = createCookie(name, content, dtime);
                }
                catch (Exception e)
                { throw e; }
            }
            return falg;
        }
        /// <summary>
        /// 修改cookie
        /// </summary>
        /// <param name="name">要修改的cookie名</param>
        /// <param name="content">cookie的内容</param>
        /// <returns>是否修改成功</returns>
        public static bool updateCookie(string name, string content)
        {
            bool falg = false;
            if (!name.Equals("") && !content.Equals(""))
            {
                try
                {
                    HttpCookie cookie = HttpContext.Current.Request.Cookies[name];
                    if (cookie != null)
                        falg = createCookie(name, content);
                }
                catch (Exception e)
                { throw e; }
            }
            return falg;
        }
        #endregion

        #region  根据cookie名获得cookie中内容
        /// <summary>
        /// 根据cookie名获得cookie中内容
        /// </summary>
        /// <param name="cookieName">要获得的cookie名称</param>
        /// <returns>指定cookie的值</returns>
        public static string getCookie(string cookieName)
        {
            string value = "";
            if (!string.IsNullOrEmpty(cookieName))
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies[cookieName];
                if (cookie != null)
                    value = HttpUtility.UrlDecode(cookie.Value);
            }
            else
                value = "cookie为空值";
            return value;
        }
        #endregion

        /// <summary>
        /// 设置cookie
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetCookie(string key, string value)
        {
            HttpContext.Current.Response.Cookies.Remove(key);
            HttpContext.Current.Response.Cookies.Add(new HttpCookie(key, value));
        }

        /// <summary>
        /// 设置cookie
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetCookie(string key, string value,DateTime dt)
        {
            HttpContext.Current.Response.Cookies.Remove(key);
            var cookie = new HttpCookie(key, value);
            cookie.Expires = dt;
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        /// <summary>
        /// 获取cookie值
        /// </summary>
        /// <param name="key"></param>
        public static string GetCookie(string key)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[key];
            if (cookie != null)
            {
                return cookie.Value;
            }
            return null;
        }
        
        /// <summary>
        /// 删除cookie
        /// </summary>
        /// <param name="key"></param>
        public static void DelCookie(string key)
        {
            HttpContext.Current.Response.Cookies.Remove(key);
        }
    }
}
