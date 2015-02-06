using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace DM.Common.libs
{
    public class Wf_RegexHelper
    {
  


        #region 常用正则表达式
        /// <summary>
        /// html标记
        /// </summary>
        public const string htmlTag = @"((<[a-z!].*?>)|(</.+?>))";

        /// <summary>
        /// 正整数
        /// </summary>
        public const string positiveInteger = @"^\+?[1-9]\d*$";

        /// <summary>
        /// 负整数
        /// </summary>
        public const string negativeInteger = @"^\-?[1-9]\d*$";

        /// <summary>
        /// 整数
        /// </summary>
        public const string integer = @"^[\+\-]?[1-9]\d*$";

        /// <summary>
        /// 正浮点数
        /// </summary>
        public const string positiveFloat = @"^(([0-9]+\.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*\.[0-9]+)|([0-9]*[1-9][0-9]*))$";

        /// <summary>
        /// 负浮点数
        /// </summary>
        public const string negativeFloat = @"^(-(([0-9]+\.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*\.[0-9]+)|([0-9]*[1-9][0-9]*)))$";

        /// <summary>
        /// 浮点数
        /// </summary>
        public const string @float = @"^(-?\d+)(\.\d+)?$";

        /// <summary>
        /// 不区分大小写英文字符
        /// </summary>
        public const string englishWordNoUpperAndLower = @"^[a-zA-Z]+$";

        /// <summary>
        /// 大写英文字符
        /// </summary>
        public const string englishWordUpper = @"^[A-Z]+$";

        /// <summary>
        /// 小写英文字符
        /// </summary>
        public const string englishWordLower = @"^[a-z]+$";

        /// <summary>
        /// 英文字符和数字的组合
        /// </summary>
        public const string englishCharAndDigit = @"^[a-zA-Z0-9]+$";

        /// <summary>
        /// 数字字母下划线的组合
        /// </summary>
        public const string englishCharDigitAndUnderline = @"^\w+$";

        /// <summary>
        /// 电子邮件
        /// </summary>
        public const string email = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

        /// <summary>
        /// 网址
        /// </summary>
        public const string href = @"[a-zA-z]+://(\w+(-\w+)*)(\.(\w+(-\w+)*))*(\?\S*)?";

        /// <summary>
        /// 中国电话
        /// </summary>
        public const string chineseTel = @"((\d{3,4})|\d{3,4}-)?\d{7,8}(-\d{3})*";

        /// <summary>
        /// 密码格式(6-20)
        /// </summary>
        public const string passwordFormat = @"^[a-zA-Z]\w{4,19}$";

        /// <summary>
        /// QQ号码
        /// </summary>
        public const string qqFormat = @"^[1-9]\d{3,8}$";

        /// <summary>
        /// IP地址
        /// </summary>
        public const string ipAddress = @"^(d{1,2}|1dd|2[0-4]d|25[0-5]).(d{1,2}|1dd|2[0-4]d|25[0-5]).(d{1,2}|1dd|2[0-4]d|25[0-5]).(d{1,2}|1dd|2[0-4]d|25[0-5])$";

        /// <summary>
        /// 首尾空格
        /// </summary>
        public const string beginSpaceOrEndSpace = @"(^\s*)|(\s*$)";

        /// <summary>
        /// 空行
        /// </summary>
        public const string blankLine = @"\n[\s| ]*\r";

        /// <summary>
        /// 中国邮政编码
        /// </summary>
        public const string chinesePost = @"^[1-9][0-9]{5}$";

        /// <summary>
        /// 中国手机
        /// </summary>
        public const string chineseMobile = @"(86)?0?1\d{10}";

        /// <summary>
        /// 中国电话，包括手机
        /// </summary>
        public const string chineseMobileTel = @"(\(\d{3,4}\)|\d{3,4}-|\s)?\d{7,14}";

        /// <summary>
        /// 数字
        /// </summary>
        public const string digit = @"(-?\d*)(\.\d+)?";

        /// <summary>
        /// 双字节字符
        /// </summary>
        public const string doubleByteChar = @"^[^\x00-\xff]*$";

        /// <summary>
        /// 非安全字符
        /// </summary>
        public const string noSafeChar = @"^(([A-Z]*|[a-z]*|\d*|[-_\~!@#\$%\^&\*\.\(\)\[\]\{\}<>\?\\\/\'\""]*)|.{0,5})$|\s";

        /// <summary>
        /// 日期
        /// </summary>
        public const string date = @"((((19){1}|(20){1})d{2})|d{2})[01]{1}d{1}[0-3]{1}d{1}";

        /// <summary>
        /// 切割字符
        /// </summary>
        public const string specialChar = "[ ;,\"'()]+";

        /// <summary>
        /// 中文
        /// </summary>
        public const string chinese = "^[\u4e00-\u9fa5]+$";

        /// <summary>
        /// 英文
        /// </summary>
        public const string chineseEnglish = @"^[\u4e00-\u9fa5\w]+$";

        /// <summary>
        /// SQL关键字
        /// </summary>
        public const string UnsafeStringPattern = @"(exec)|(insert)|(select)|(delete)|(update)|(count)|(\*)|(chr)|(mid)|(master)|(truncate)|(char)|(declare)|(execute)|(substring)|(create)|(table)|(index)|(cursor)|(procedure)|(transaction)|(-{2,})";

        /// <summary>
        /// 批量操作时的id条件:1,2,6,1
        /// </summary>
        public const string IDS = @"^[1-9]\d*(,[1-9]\d*)*$";

        /// <summary>
        /// 批量操作时的id条件:1$2$6$1
        /// </summary>
        public const string IDSM = @"^[1-9]\d*(\$[1-9]\d*)*$";

        /// <summary>
        /// 批量操作时的id条件:1|2|6|1
        /// </summary>
        public const string IDSS = @"^[1-9]\d*(|[1-9]\d*)*$";

        /// <summary>
        /// 批量操作时的GUID条件:1|2|6;1
        /// </summary>
        /// string ptn = @"^([\w\d\u4e00-\u9fa5],?)+$";
        public const string GUIDS = @"^([A-Z0-9]{8}-[A-Z0-9]{4}-[A-Z0-9]{4}-[A-Z0-9]{4}-[A-Z0-9]{12},?)+$";

        /// <summary>
        /// 安科卡号格式
        /// </summary>
        public const string AnkeCardFormat = @"\d{10}";

        #endregion

        #region	常用函数
        /// <summary>
        /// 判断是否GUID格式
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsGUID(string strSrc)
        {
            Guid g = Guid.Empty;
            return Guid.TryParse(strSrc, out g);
        }

        /// <summary>
        /// 判断字符是否为中文
        /// </summary>
        /// <param name="ch">字符</param>
        /// <returns>是中文则返回true，否则返回false</returns>
        public static bool IsChinese(string text)
        {
            return System.Text.RegularExpressions.Regex.Match(text, chinese, System.Text.RegularExpressions.RegexOptions.Compiled).Success;
        }

        public static bool IsChinese(char ch)
        {
            return IsChinese(Convert.ToString(ch));
        }

        /// <summary>
        /// 判断字符是否为英文
        /// </summary>
        /// <param name="ch">字符</param>
        /// <returns>是英文则返回true，否则返回false</returns>
        public static bool IsEnglish(string text)
        {
            return System.Text.RegularExpressions.Regex.Match(text, englishWordNoUpperAndLower, System.Text.RegularExpressions.RegexOptions.Compiled).Success;
        }

        /// <summary>
        /// 判断字符是否为英文或数字
        /// </summary>
        /// <param name="ch">字符</param>
        /// <returns>是英文或数组则返回true，否则返回false</returns>
        public static bool IsEnglishOrInteger(char ch)
        {
            return System.Text.RegularExpressions.Regex.Match(Wf_ConvertHelper.ToString(ch), englishCharAndDigit, System.Text.RegularExpressions.RegexOptions.Compiled).Success;
        }

        /// <summary>
        /// 判断字符是否为中英文组合
        /// </summary>
        /// <param name="ch">字符</param>
        /// <returns>是中英文组合则返回true，否则返回false</returns>
        public static bool IsChineseEnglish(string text)
        {
            return System.Text.RegularExpressions.Regex.Match(text, chineseEnglish, System.Text.RegularExpressions.RegexOptions.Compiled).Success;
        }

        public static string Replace(string input, string pattern, string replacement)
        {
            if (Wf_StringHelper.IsNullOrEmptyByTrim(input)) return "";

            return System.Text.RegularExpressions.Regex.Replace(input, pattern, replacement, System.Text.RegularExpressions.RegexOptions.Compiled | System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Singleline);
        }

        /// <summary>
        /// 判断字符串是否为整数或者浮点数
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsNumeric(string input)
        {
            return Is(input, @"[\+-]?\d+(\.\d+)?$");
        }

        /// <summary>
        /// 判断字符串是否为整数
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsInt(string input)
        {
            return Is(input, @"^[\+-]?\d+$");
        }

        /// <summary>
        /// 判断字符串是否全部为数字
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsNumberStr(string input)
        {
            if (string.IsNullOrEmpty(input))
                return false;
            else
                return Is(input, positiveInteger);
        }

        /// <summary>
        /// 判断字符串是否为浮点数
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsFloat(string input)
        {
            return Is(input, @"[\+-]?\d+\.\d+");
        }

        /// <summary>
        /// 判断字符串是否符合某正则表达式验证
        /// </summary>
        /// <param name="input">验证的字符串</param>
        /// <param name="pattern">正则表达式规则</param>
        /// <returns></returns>
        public static bool Is(string input, string pattern)
        {
            return System.Text.RegularExpressions.Regex.Match(input, pattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.ECMAScript).Success;
        }

        /// <summary>
        /// 判断文件是否是图片文件
        /// </summary>
        /// <param name="filePath">文本路径</param>
        /// <returns>布尔值</returns>
        public static bool IsImage(string filePath)
        {
            System.Drawing.Image image;
            try
            {
                image = System.Drawing.Image.FromFile(filePath);
                image.Dispose();
                return true;
            }
            catch (Exception )
            {
                return false;
            }
        }

        /// <summary>
        /// 将字符串按指定字符切割返回string[]
        /// </summary>
        /// <param name="input"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static string[] Split(string input, string pattern)
        {
            if (string.IsNullOrEmpty(input))
            {
                return null;
            }
            return System.Text.RegularExpressions.Regex.Split(input, pattern, System.Text.RegularExpressions.RegexOptions.Compiled | System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Singleline);
        }

        public static string ParseHtml(object obj)
        {
            try
            {
                return ParseHtml(Wf_ConvertHelper.ToString(obj));
            }
            catch { return ""; }
        }

        /// <summary>
        /// 去除Html标签
        /// </summary>
        /// <param name="html"><p>abcdefg</p></param>
        /// <returns>abcdefg</returns>
        public static string ParseHtml(string input)
        {
            if (Wf_StringHelper.IsNullOrEmptyByTrim(input)) return "";
            try
            {
                string html = Wf_RegexHelper.ParseToHtml(input);		//替换换行 空格
                html = System.Text.RegularExpressions.Regex.Replace(html, "<!--.*?-->", "", System.Text.RegularExpressions.RegexOptions.Compiled);
                html = System.Text.RegularExpressions.Regex.Replace(html, "<style.*?>.*?</style>", "", System.Text.RegularExpressions.RegexOptions.Compiled | System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Singleline);
                html = System.Text.RegularExpressions.Regex.Replace(html, "<script.*?>.*?</script>", "", System.Text.RegularExpressions.RegexOptions.Compiled | System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Singleline);
                html = System.Text.RegularExpressions.Regex.Replace(html, "<head>.*?</head>", "", System.Text.RegularExpressions.RegexOptions.Compiled | System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Singleline);
                html = System.Text.RegularExpressions.Regex.Replace(html, "<option.*?>.*?</option>", "", System.Text.RegularExpressions.RegexOptions.Compiled | System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Singleline);
                html = System.Text.RegularExpressions.Regex.Replace(html, "((<[a-z!].*?>)|(</.+?>))", "", System.Text.RegularExpressions.RegexOptions.Compiled | System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Singleline);
                html = System.Text.RegularExpressions.Regex.Replace(html, @"\s+", " ", System.Text.RegularExpressions.RegexOptions.Compiled | System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Singleline);

                return html;
            }
            catch (Exception )
            {
                return input;
            }
        }
        /// <summary>
        /// 把文本框的回车变为<br/>
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string ParseToHtml(string content)
        {
            content = content.Replace("\n", "<br>");
            content = content.Replace("\n\r", "<br>");
            content = content.Replace("\r", "<br>");
            return content;
        }

        public static string ParseHtmlBQ(object temp)
        {
            string html = Wf_ConvertHelper.ToString(temp);

            if (Wf_StringHelper.IsNullOrEmptyByTrim(html)) return "";
            try
            {
                html = Wf_RegexHelper.ParseToHtml(html);		//替换换行 空格
                html = System.Text.RegularExpressions.Regex.Replace(html, "<!--.*?-->", "", System.Text.RegularExpressions.RegexOptions.Compiled);
                html = System.Text.RegularExpressions.Regex.Replace(html, "((<[a-z!].*?>)|(</.+?>))", "", System.Text.RegularExpressions.RegexOptions.Compiled | System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Singleline);
                html = System.Text.RegularExpressions.Regex.Replace(html, @"\s+", " ", System.Text.RegularExpressions.RegexOptions.Compiled | System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Singleline);
                html = System.Text.RegularExpressions.Regex.Replace(html, @"&nbsp;", " ", System.Text.RegularExpressions.RegexOptions.Compiled | System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Singleline);
                return html;
            }
            catch { return ""; }
        }

        public static int GetHtmlPageCount(string html)
        {
            if (Wf_StringHelper.IsNullOrEmptyByTrim(html)) return 0;
            try
            {
                Regex regex = new Regex("<span class=\"toprand-page-numpage\">.*?</span>");
                MatchCollection matchs = regex.Matches(html);
                string count = new Regex(@"^\+?[1-9]\d*$").Match(matchs[matchs.Count - 1].Value).Value;
                return Wf_ConvertHelper.ToInt32(count);
            }
            catch { return 0; }
        }

        public static string ParesHtmlLineTab(string html)
        {
            try
            {
                if (Wf_StringHelper.IsNullOrEmptyByTrim(html)) return "";
                return System.Text.RegularExpressions.Regex.Replace(html, "\t|\r|\n|\r\n", "", System.Text.RegularExpressions.RegexOptions.Compiled);
            }
            catch { return ""; }
        }

        public static string ReplaceHtmlStyle(string html)
        {
            if (Wf_StringHelper.IsNullOrEmptyByTrim(html)) return "";
            try
            {
                Regex regex = new Regex("<link.*? />");
                MatchCollection matchs = regex.Matches(html);
                foreach (Match match in matchs)
                {
                    string link = new Regex("href=\"(.*?..css\")").Match(match.Value).Value.Replace("href=\"", "").Replace("\"", "");
                    Wf_FileReadOrWrite file = new Wf_FileReadOrWrite();
                    string css = file.FileRead(link.Remove(0, 1));
                    css = css.Replace("../images", "/skins/images");
                    html = html.Replace(match.Value, string.Format("<style type=\"text/css\">{0}</style>", css));
                }
                return html;
            }
            catch { return ""; }
        }

        public static string RemoveHtmlViewState(string html)
        {
            try
            {
                if (Wf_StringHelper.IsNullOrEmptyByTrim(html)) return "";
                return System.Text.RegularExpressions.Regex.Replace(html, "<div><input type=\"hidden\" name=\"__VIEWSTATE\" id=\"__VIEWSTATE\" .*? /></div>", "", System.Text.RegularExpressions.RegexOptions.Compiled);
            }
            catch { return ""; }
        }

        /// <summary>
        /// 过滤SQL关键字
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string SqlKeyWordsFilter(string input)
        {
            string temp = Replace(input, UnsafeStringPattern, "");
            if (IsUnsafeString(temp))
                return SqlKeyWordsFilter(temp);
            return temp;
        }

        /// <summary>
        /// 判断字符串是否安全(包含SQL关键字)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsUnsafeString(string input)
        {
            return Is(input, UnsafeStringPattern);
        }

        /// <summary>
        /// 判断是否为ID字符串如1,2,3或者1
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsIdsStr(string input)
        {
            if (string.IsNullOrEmpty(input))
                return false;
            else
                return Is(input, IDS);
        }

        /// <summary>
        /// 判断是否为ID字符串如1,2,3或者1
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsIdsMStr(string input)
        {
            if (string.IsNullOrEmpty(input))
                return false;
            else
                return Is(input, IDSM);
        }

        /// <summary>
        /// 判断是否为ID字符串如1|2|3或者1
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsIdsSStr(string input)
        {
            if (string.IsNullOrEmpty(input))
                return false;
            else
                return Is(input, IDSS);
        }

        /// <summary>
        /// 判断是否为GUID字符串如1|2|3或者1
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsGuidsStr(string input)
        {
            if (string.IsNullOrEmpty(input))
                return false;
            else
                return Is(input, GUIDS);
        }

        /// <summary>
        /// 判断是否手机号码格式
        /// </summary>
        /// <param name="input">元字符</param>
        /// <returns>false，true</returns>
        public static bool IsChineseMobile(string input)
        {
            if (string.IsNullOrEmpty(input))
                return false;
            else
                return Is(input, chineseMobile);
        }

        /// <summary>
        /// 判断是否卡号
        /// </summary>
        /// <param name="input">元字符</param>
        /// <returns>false，true</returns>
        public static bool IsAnkeCard(string input)
        {
            if (string.IsNullOrEmpty(input))
                return false;
            else
                return Is(input, AnkeCardFormat);
        }

        /// <summary>
        /// 将:“张三,李四[,]”，转换为:“'张三','李四'”，以供sql直接拼接
        /// </summary>
        /// <param name="str">原始格式：value1,value2,……,valuen[,]</param>
        /// <returns>目标格式：'value1','value2',……,'valuen'</returns>
        public static string ToSqlStr(string str)
        {
            string[] strs = str.Split(',');
            if (strs.Length > 0)
            {
                StringBuilder sbStr = new StringBuilder();
                for (int i = 0; i < strs.Length; i++)
                {
                    if (strs[i].Length > 0)
                    {
                        sbStr.Append("'" + strs[i] + "',");
                    }
                }
                string sqlStr = sbStr.ToString();
                return sqlStr.Substring(0, sqlStr.Length - 1);
            }
            else {
                return "";
            }
        }

        /// <summary>
        /// 先验证字符串是否为guid集合，不是则返回"ERR";是，则返回“'guid1','guid2'”，以供sql直接拼接
        /// </summary>
        /// <param name="str">原始格式：guid1,guid2,……,guidn[,]</param>
        /// <returns>"ERR"或”'guid1','guid2',……,'guidn'“</returns>
        public static string ToSqlGuidStr(string str)
        {
            if (!IsGuidsStr(str)) {
                return "ERR";
            }
            return ToSqlStr(str);
        }
        #endregion
    }
}
