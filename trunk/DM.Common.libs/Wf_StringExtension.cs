using System;
using System.Text;


namespace DM.Common.libs
{
    /// <summary>
    /// 对字符串的扩展
    /// </summary>
    public static class Wf_StringExtension
    {
        #region 将整数转换为大写
        /// <summary>
        /// 将整数转换为大写
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string TranslateToString(int value)
        {
            String[] UNITS = { "", "十", "百", "千", "万", "十", "百", "千", "亿", "十", "百", "千", };
            String[] NUMS = { "零", "一", "二", "三", "四", "五", "六", "七", "八", "九", };

            //转译结果
            String result = "";

            for (int i = value.ToString().Length - 1; i >= 0; i--)
            {
                int r = (int)(value / Math.Pow(10, i));
                result += NUMS[r % 10] + UNITS[i];
            }

            result = result.Replace("零[十, 百, 千]", "零");
            result = result.Replace("零+", "零");
            result = result.Replace("零([万, 亿])", "$1");
            result = result.Replace("亿万", "亿");   //亿万位拼接时发生的特殊情况

            if (result.StartsWith("一十"))
            {
                result = result.Substring(1);
            }

            if (result.EndsWith("零"))
            {
                result = result.Substring(0, result.Length - 1);
            }

            return "第" + result + "周";
        }
        #endregion

        #region Left 取 string 左边L个字符
        /// <summary>
        /// 取 string 左边L个字符
        /// </summary>
        /// <param name="str">原始str</param>
        /// <param name="L">长度</param>
        /// <returns>返回修改后的字符</returns>
        public static string Left(this string str, int L)
        {
            if (str.Length <= L) return str;
            return str.Substring(0, L);
        }
        #endregion

        #region Left 取 string 左边L个字符，并在右边加 n 个"."
        /// <summary>
        /// Left 取 string 左边L个字符，并在右边加 n 个"."
        /// </summary>
        /// <param name="str">原始str</param>
        /// <param name="L">长度</param>
        /// <param name="n">加.的个数</param>
        /// <returns>返回修改后的字符</returns>
        public static string Left(this string str, int L, int n)
        {
            if (str.Length <= L) return str;
            return str.Substring(0, L) + new String('.', n);
        }
        #endregion

        #region Right 取 string 右边L个字符
        /// <summary>
        /// 取 string 右边L个字符
        /// </summary>
        /// <param name="str">原始str</param>
        /// <param name="L">长度</param>
        /// <returns>返回修改后的字符</returns>
        public static string Right(this string str, int L)
        {
            if (str.Length <= L) return str;
            return str.Substring(str.Length - L);
        }
        #endregion

        #region 获取字符串byte长度
        /// <summary>
        /// 获取字符串byte长度
        /// </summary>
        /// <param name="inputStr"></param>
        /// <returns></returns>
        public static int GetByteLength(this string inputStr)
        {
            return System.Text.Encoding.Default.GetByteCount(inputStr);
        }
        #endregion

        #region 获取子字符串（根据开始串和结束串）
        /// <summary>
        /// 获取子字符串（根据开始串和结束串）
        /// </summary>
        /// <param name="inputStr">输入字符串</param>
        /// <param name="beginStr">开始串</param>
        /// <param name="endStr">结束串</param>
        /// <param name="isIncludeBeginAndEndStr">是否包含首尾字符</param>
        /// <returns>子串</returns>
        public static string GetSubstring(this string inputStr, string beginStr, string endStr, bool isIncludeBeginAndEndStr = false)
        {
            int indStart = inputStr.IndexOf(beginStr);
            int indEnd = inputStr.IndexOf(endStr, indStart);
            if (indEnd - indStart > 0)
            {
                string str = inputStr.Substring(indStart, indEnd - indStart + 1);

                if (isIncludeBeginAndEndStr)
                {
                    return str;
                }

                return str.Substring(beginStr.Length, str.Length - beginStr.Length - 1);
            }
            return "";
        }
        #endregion

        #region 数据类型转换
        /// <summary>
        /// 转换为Int32
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static int ToInt32(this string source)
        {
            return source.ToInt32(0);
        }
        /// <summary>
        /// 转换为Int32
        /// </summary>
        /// <param name="source"></param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static int ToInt32(this string source, int defaultValue)
        {
            if (string.IsNullOrEmpty(source))
                return defaultValue;

            int value;

            if (!int.TryParse(source, out value))
                value = defaultValue;

            return value;
        }
        /// <summary>
        /// 转换为长整
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static long ToLong(this string source)
        {
            return source.ToLong(0);
        }
        /// <summary>
        /// 转换为长整
        /// </summary>
        /// <param name="source"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static long ToLong(this string source, long defaultValue)
        {
            if (string.IsNullOrEmpty(source))
                return defaultValue;

            long value;

            if (!long.TryParse(source, out value))
                value = defaultValue;

            return value;
        }
        /// <summary>
        /// 转换为短整
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static short ToShort(this string source)
        {
            return source.ToShort(0);
        }
        /// <summary>
        /// 转换为短整
        /// </summary>
        /// <param name="source"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static short ToShort(this string source, short defaultValue)
        {
            if (string.IsNullOrEmpty(source))
                return defaultValue;

            short value;

            if (!short.TryParse(source, out value))
                value = defaultValue;

            return value;
        }
        /// <summary>
        /// 转换为Decimal
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this string source)
        {
            return source.ToDecimal(0);
        }
        /// <summary>
        /// 转换为Decimal
        /// </summary>
        /// <param name="source"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this string source, decimal defaultValue)
        {
            if (string.IsNullOrEmpty(source))
                return defaultValue;

            decimal value;

            if (!decimal.TryParse(source, out value))
                value = defaultValue;

            return value;
        }
        /// <summary>
        /// 转换为DateTime
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string source)
        {
            return source.ToDateTime(DateTime.MinValue);
        }
        /// <summary>
        /// 转换为DateTime
        /// </summary>
        /// <param name="source"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string source, DateTime defaultValue)
        {
            if (string.IsNullOrEmpty(source))
                return defaultValue;

            DateTime value;

            if (!DateTime.TryParse(source, out value))
                value = defaultValue;

            return value;
        }
        /// <summary>
        /// 转换为bool
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool ToBoolean(this string source)
        {
            return source.ToBoolean(false);
        }
        /// <summary>
        /// 转换为bool
        /// </summary>
        /// <param name="source"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static bool ToBoolean(this string source, bool defaultValue)
        {
            if (string.IsNullOrEmpty(source))
                return defaultValue;

            bool value;

            if (!bool.TryParse(source, out value))
                value = defaultValue;

            return value;
        }
        #endregion

        #region 为ULR添加额为参数
        /// <summary>
        /// url 添加 PersonId 参数
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string URLAppendParameter(this string src_str, string ParameterName, string ParameterValue)
        {
            if (!string.IsNullOrEmpty(src_str))
            {
                int IndexOfWenHao = src_str.IndexOf("?");
                string StrConnector = (IndexOfWenHao == -1) ? "?" : "&";
                return src_str += (StrConnector + ParameterName + "=" + ParameterValue);
            }
            return src_str;
        }

        /// <summary>
        /// url 添加 PersonId 参数
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string URLAppendPersonId(this string src_str, string StrPersonId)
        {
            return URLAppendParameter(src_str, "personId", StrPersonId);
        }
        #endregion

        #region 按指定格式放回日期字符串
        /// <summary>
        /// 按指定格式放回日期字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="formatstr"></param>
        /// <returns></returns>
        public static string FormatDate(this string obj, string formatstr)
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

        /// <summary>
        /// 返回1900-01-01
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="formatstr"></param>
        /// <returns></returns>
        public static string FormatDate1(this string obj, string formatstr)
        {
            return FormatDate(obj, "yyyy-MM-dd");
        }

        /// <summary>
        /// 返回1900:01:01
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="formatstr"></param>
        /// <returns></returns>
        public static string FormatDate2(this string obj, string formatstr)
        {
            return FormatDate(obj, "yyyy:MM:dd");
        }

        /// <summary>
        /// 返回1900-01-01
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="formatstr"></param>
        /// <returns></returns>
        public static string FormatDate3(this string obj, string formatstr)
        {
            return FormatDate(obj, "yyyy/MM/dd");
        }

        /// <summary>
        /// 返回1900年01月01日
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="formatstr"></param>
        /// <returns></returns>
        public static string FormatDate4(this string obj, string formatstr)
        {
            return FormatDate(obj, "yyyy年 MM月 dd日");
        }
        #endregion

    }
}
