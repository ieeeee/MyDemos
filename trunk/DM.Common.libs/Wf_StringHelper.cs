using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data;
using System.Drawing;

namespace DM.Common.libs
{
    public static class Wf_StringHelper
    {
        public static string NotImage = "/Global/NotImage.jpg";

        private static string _WebTitleText;
        public static string WebTitleText
        {
            get { return Wf_StringHelper._WebTitleText; }
            set { Wf_StringHelper._WebTitleText = value; }
        }


        /// <summary>
        /// 将字符串分割,返回int[]数组
        /// </summary>
        /// <param name="input">字符串</param>
        /// <returns>int[]</returns>
        public static int[] SplitRetInt(string input)
        {
            string[] strs = Split(input);
            int[] ids = new int[strs.Length];
            for (int i = 0; i < ids.Length; i++)
            {
                ids[i] = Wf_ConvertHelper.ToInt32(strs[i]);
            }
            return ids;
        }

        /// <summary>
        /// 将字符串分割,返回string[]数组-分隔符为(,， ;、/\\)
        /// </summary>
        /// <param name="input">字符串</param>
        /// <returns>string[]</returns>
        public static string[] Split(string input)
        {
            if (IsNullOrEmptyByTrim(input))
                return null;

            return Wf_RegexHelper.Split(input, @"[,， ;、/\\]");
        }

        public static string[] Split(string input, string splitChar)
        {
            if (IsNullOrEmptyByTrim(input))
                return null;

            return Wf_RegexHelper.Split(input, @"[" + splitChar + "]");
        }

        /// <summary>
        /// 判断字符串是否为null或""(清除首尾空格)
        /// </summary>
        /// <param name="input">字符串</param>
        /// <returns>为空或""返回true，否则flase</returns>
        public static bool IsNullOrEmptyByTrim(string input)
        {
            if (string.IsNullOrEmpty(input))
                return true;

            if (input.Trim().Length == 0)
                return true;

            return false;
        }

        /// <summary>
        /// 判断字符串是否为null或""(清楚首尾空格)
        /// </summary>
        /// <param name="input">object</param>
        /// <returns>为空或""返回true，否则flase</returns>
        public static bool IsNullOrEmptyByTrim(object input)
        {
            return IsNullOrEmptyByTrim(Wf_ConvertHelper.ToString(input));
        }

        /// <summary>
        /// 返回一个空字符串
        /// </summary>
        public static string EmptyString
        {
            get
            {
                return String.Empty;
            }
        }

        /// <summary>
        /// 获得字符串的字节长度
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>字节长度</returns>
        public static int GetLength(string str)
        {
            if (IsNullOrEmptyByTrim(str))
                return 0;

            byte[] by = System.Text.Encoding.Default.GetBytes(str);

            return by.Length;
        }

        /// <summary>
        /// 字符串截取(按byte长度)
        /// </summary>
        /// <param name="str">要截取的字符串</param>
        /// <param name="maxlen">最大的byte长度</param>
        /// <param name="appendStr">截取后追加的字符(长度不够不追加)</param>
        /// <returns></returns>
        public static string SubString(string str, int maxlen, string appendStr)
        {
            if (IsNullOrEmptyByTrim(str))
                return "";

            byte[] by = System.Text.Encoding.Default.GetBytes(str);
            byte[] cutby = new byte[maxlen];
            string temp = "";
            if (by.Length > maxlen)
            {
                Array.Copy(by, 0, cutby, 0, maxlen);
                temp = System.Text.Encoding.Default.GetString(cutby);
                return temp.Substring(0, temp.Length - 1) + (IsNullOrEmptyByTrim(appendStr) ? "" : appendStr);
            }
            return str;
        }

        /// <summary>
        /// 为跳转连接中的URL添加http://开头 返回Html代码
        /// </summary>
        /// <param name="text">需要添加的连接</param>
        /// <returns><a href="http://www.microsoft.com">www.microsoft.com</a> </returns>
        public static string RuleUrlHttpLink(string text)
        {
            if (IsNullOrEmptyByTrim(text))
                return null;

            string strTemp = text.Trim();

            string[] urls = System.Text.RegularExpressions.Regex.Split(strTemp, @"\s+", System.Text.RegularExpressions.RegexOptions.Compiled | System.Text.RegularExpressions.RegexOptions.Singleline);

            string str = null;
            foreach (string url in urls)
            {
                if (url.ToLower().StartsWith("http://"))
                {
                    str += "<a href=\"" + url + "\" target=\"_blank\">" + url.Replace("http://", "") + "</a>  ";
                }
                else
                {
                    string tempUrl = "http://" + url;
                    str += "<a href=\"" + tempUrl + "\" target=\"_blank\">" + tempUrl.Replace("http://", "") + "</a>  ";
                }
            }

            return str;
        }

        public static string RuleUrlHttpLink(string text, bool isHtml)
        {
            if (IsNullOrEmptyByTrim(text))
                return null;

            string strTemp = text.Trim();

            string[] urls = System.Text.RegularExpressions.Regex.Split(strTemp, @"\s+", System.Text.RegularExpressions.RegexOptions.Compiled | System.Text.RegularExpressions.RegexOptions.Singleline);

            string str = null;
            foreach (string url in urls)
            {
                if (url.ToLower().StartsWith("http://"))
                    return url;
                else
                    return "http://" + url;
            }

            return str;
        }

        /// <summary>
        /// 自定义返回System.Exception异常报错字符串
        /// </summary>
        /// <param name="ex">System.Exception</param>
        /// <returns>自定义异常字符串</returns>
        public static string BuildExceptionMessage(System.Exception ex)
        {
            StringBuilder exceptionMsgs = new StringBuilder();
            if (null != ex)
            {
                exceptionMsgs.AppendFormat("\n{0}\n{1}\n{2} Soure:{3}", ex.GetType().Name, ex.Message, ex.StackTrace, ex.Source);
                ex = ex.InnerException;
            }
            return exceptionMsgs.ToString();
        }

        /// <summary>
        /// 将指定月转换为中文
        /// 例：1转为一月
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        public static string MonthToChinese(int month)
        {
            switch (month)
            {
                case 1: return "一月";
                case 2: return "二月";
                case 3: return "三月";
                case 4: return "四月";
                case 5: return "五月";
                case 6: return "六月";
                case 7: return "七月";
                case 8: return "八月";
                case 9: return "九月";
                case 10: return "十月";
                case 11: return "十一月";
                case 12: return "十二月";
            }
            throw new Exception("月份错误");
        }

        /// <summary>
        /// 将字符串切割并且判断其键值是否包含某个字符串
        /// </summary>
        /// <param name="check">需要判断的字符串</param>
        /// <param name="valueString">需要检测的待分割字符串</param>
        /// <returns></returns>
        public static bool CheckCharInStringToArray(string check, string valueString)
        {
            if (string.IsNullOrEmpty(check) || string.IsNullOrEmpty(valueString))
                return false;

            string[] arr = Split(valueString);
            foreach (string str in arr)
            {
                if (check.Trim().Equals(str.Trim()))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 清除字符串首尾空格
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string Trim(string input)
        {
            if (IsNullOrEmptyByTrim(input))
                return "";

            return input.Trim();
        }

        #region 获取上传图片 文件路径


        public static string GetUpLoadFilesPath(string filePath)
        {
            string sitePath = string.Format("{0}{1}", System.Web.HttpContext.Current.Server.MapPath("/UploadFiles/main/Files/"), filePath);

            if (!System.IO.File.Exists(sitePath))
                return "";

            return "/UploadFiles/main/Files/" + filePath;
        }

        public static string GetUpLoadImagePath(object imagesPath)
        {
            return GetUpLoadImagePath(imagesPath, false);
        }

        public static string GetUpLoadImagePath(string imagesPath)
        {
            return GetUpLoadImagePath(imagesPath, false);
        }

        public static string GetUpLoadImagePath(object imagesPath, bool findSmallImg)
        {
            return GetUpLoadImagePath(Wf_ConvertHelper.ToString(imagesPath), findSmallImg);
        }

        public static string GetUpLoadImagePath(string imagesPath, bool findSmallImg)
        {
            if (Wf_StringHelper.IsNullOrEmptyByTrim(imagesPath)) return Wf_StringHelper.NotImage;

            //增加直接从images文件夹找图片
            string rootpath = imagesPath.StartsWith("/") ? "/" : "/UploadFiles/main/Images/";

            string sitePath = string.Format("{0}{1}", System.Web.HttpContext.Current.Server.MapPath(rootpath), imagesPath);

            if (findSmallImg)
            {
                string smallpath = sitePath + ".gif";
                if (System.IO.File.Exists(smallpath))
                    return rootpath + (imagesPath.StartsWith("/") ? imagesPath.Remove(0, 1) : imagesPath) + ".gif";
            }

            if (System.IO.File.Exists(sitePath))
                return rootpath + (imagesPath.StartsWith("/") ? imagesPath.Remove(0, 1) : imagesPath);
            else
                return Wf_StringHelper.NotImage;
        }


        #endregion

        /// <summary>
        /// 按照固定的某宽/某高等比例缩放图片
        /// </summary>
        /// <param name="Path"></param>
        /// <param name="Limitwidth">最大宽度</param>
        /// <param name="Limitheight">最大高度</param>
        /// <param name="OutWidth">输出宽度</param>
        /// <param name="OutHeight">输出高度</param>
        public static void GetImageThumbnailSize(string Path, int Limitwidth, int Limitheight, out int OutWidth, out int OutHeight)
        {
            if (!Wf_StringHelper.IsNullOrEmptyByTrim(Path))
            {
                //获取图片
                string filepath = System.Web.HttpContext.Current.Server.MapPath("/") + (Path.IndexOf('/') == 0 ? Path.Remove(0, 1) : Path);
                Image image = Image.FromFile(filepath);
                if (image != null)
                {

                    if (Limitwidth > 0 && image.Width >= Limitwidth)			//指定高度进行缩放
                    {
                        OutWidth = Limitwidth;
                        OutHeight = Wf_ConvertHelper.ToInt32(Limitwidth * image.Height / image.Width);
                    }
                    else if (Limitheight > 0 && image.Height >= Limitheight)	//指定宽度进行缩放
                    {
                        OutHeight = Limitheight;
                        OutWidth = Wf_ConvertHelper.ToInt32(Limitheight * image.Width / image.Height);
                    }
                    else
                    {
                        OutWidth = image.Width;
                        OutHeight = image.Height;
                    }
                    return;
                }
            }
            OutWidth = 200;
            OutHeight = 200;
        }

        #region 返回分享按钮html字符串
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetShareStr(object url, object title)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<script type=\"text/javascript\"> var jiathis_config = {url: \"" + url.ToString().Trim() + "\", title: \"" + title.ToString() + "\" }</script>");
            builder.Append("<div id=\"ckepop\" style=\"margin-top:8px;\" ><a href=\"http://www.jiathis.com/share/\" class=\"jiathis jiathis_txt\" target=\"_blank\"><img src=\"/skins/images/jiathis1.gif\" border=\"0\" /></a></div><div class=\"clear\"> </div>");
            builder.Append("<script type=\"text/javascript\" src=\"http://v2.jiathis.com/code_mini/jia.js\" charset=\"utf-8\"></script><br /><p></p>");
            return builder.ToString();
        }
        #endregion

        #region 获取相册图片（数组）
        /// <summary>
        /// 获取相册图片（数组）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string[] GetAlbum(string input, char splitstr)
        {
            if (!string.IsNullOrEmpty(input))
            {
                //详细页面图片
                StringBuilder sbPics = new StringBuilder();

                if (input.Contains("$$$"))
                {
                    string pics = input.Replace("$$$", ";");
                    string[] picArray = pics.Split(';');
                    return picArray;
                }
                else
                {
                    return input.Split(splitstr);
                }
            }
            else
            {
                return null;
            }
        }
        #endregion

        /// <summary>
        /// 处理出错
        /// </summary>
        public static void NoDataException()
        {
            //异常处理
            System.Web.HttpContext.Current.Response.Clear();
            System.Web.HttpContext.Current.Response.Write("系统正忙...请稍后再试.");
            System.Web.HttpContext.Current.Response.End();
        }

        public static string CheckStr(object input, string format)
        {
            string result = Wf_RegexHelper.ParseHtmlBQ(input);
            if (!Wf_StringHelper.IsNullOrEmptyByTrim(result))
                return string.Format(format, result);
            return "";
        }

        /// <summary>
        /// 按指定格式放回日期字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="formatstr"></param>
        /// <returns></returns>
        public static string FormatDate(object obj, string formatstr)
        {
            try
            {
                if (obj == null)
                {
                    return DateTime.Now.ToString(formatstr);
                }
                else
                {
                    DateTime dt = Convert.ToDateTime(obj);
                    if (dt != null)
                    {
                        return dt.ToString(formatstr);
                    }
                    else
                    {
                        return DateTime.Now.ToString(formatstr);
                    }
                }
            }
            catch (Exception)
            {
                return DateTime.Now.ToString(formatstr);
            }
        }
    }


}