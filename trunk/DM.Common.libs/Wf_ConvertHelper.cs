using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DM.Common.libs
{
    public class Wf_ConvertHelper
    {
        /// <summary>
        /// 性别转换0女，1男
        /// </summary>
        /// <param name="SexCode"></param>
        /// <returns></returns>
        public static string GetSexBySexCode(object SexCode)
        {
            if (SexCode == null || string.IsNullOrEmpty(SexCode.ToString()))
            {
                return "";
            }
            else if (SexCode.ToString() == "0")
            {
                return "女";

            }
            else if (SexCode.ToString() == "1")
            {
                return "男";
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 卡状态0停用，1正常
        /// </summary>
        /// <param name="SexCode"></param>
        /// <returns></returns>
        public static string GetCardStateCode(object Code)
        {
            if (Code == null || string.IsNullOrEmpty(Code.ToString()))
            {
                return "";
            }
            else if (Code.ToString() == "0")
            {
                return "停用";

            }
            else if (Code.ToString() == "1")
            {
                return "正常";
            }
            else
            {
                return "";
            }
        }

        #region ToInt16
        public static short ToInt16(object input)
        {
            return ToInt16(input);
        }

        public static short ToInt16(object input, short defaultValue)
        {
            string temp = Wf_ConvertHelper.ToString(input);

            if (Wf_StringHelper.IsNullOrEmptyByTrim(temp))
                return defaultValue;

            if (Wf_RegexHelper.IsInt(temp))
                return System.Convert.ToInt16(temp);
            else
                return defaultValue;
        }
        #endregion

        #region ToInt32
        public static int ToInt32(object input)
        {
            return ToInt32(input, 0);
        }

        public static int ToInt32(object input, int defaultValue)
        {
            string temp = Wf_ConvertHelper.ToString(input);

            if (string.IsNullOrEmpty(temp))
                return defaultValue;

            if (Wf_RegexHelper.IsInt(temp))
                return System.Convert.ToInt32(input);
            else
                return defaultValue;
        }
        #endregion

        #region ToFloat
        public static float ToFloat(object input)
        {
            return ToFloat(input, 0.0f);
        }

        public static float ToFloat(object input, float defaultValue)
        {
            string temp = Wf_ConvertHelper.ToString(input);
            if (Wf_StringHelper.IsNullOrEmptyByTrim(input))
                return defaultValue;

            if (Wf_RegexHelper.IsNumeric(temp))
                return System.Convert.ToSingle(input);
            else
                return defaultValue;
        }
        #endregion

        #region ToDouble
        public static double ToDouble(object input)
        {
            return ToDouble(input, 0.0);
        }

        public static double ToDouble(object input, double defaultValue)
        {
            string temp = Wf_ConvertHelper.ToString(input);
            if (Wf_StringHelper.IsNullOrEmptyByTrim(input))
                return defaultValue;

            if (Wf_RegexHelper.IsNumeric(temp))
                return System.Convert.ToDouble(input);
            else
                return defaultValue;
        }
        #endregion

        #region ToString
        public static string ToString(object input)
        {
            return ToString(input, null);
        }

        public static string ToString(object input, string defaultValue)
        {
            return ToString(input, defaultValue, false);
        }

        public static string ToString(object input, string defaultValue, bool sqlSafeFilter)
        {
            if (input == null || Wf_StringHelper.IsNullOrEmptyByTrim(input.ToString()))
                return defaultValue;

            if (sqlSafeFilter)
                return Wf_RegexHelper.SqlKeyWordsFilter(input.ToString());

            return input.ToString();
        }
        #endregion

        #region ToDecimal
        public static decimal ToDecimal(object input)
        {
            return ToDecimal(input, 0.0m);
        }

        public static decimal ToDecimal(object input, decimal defaultValue)
        {
            string temp = Wf_ConvertHelper.ToString(input);
            if (Wf_StringHelper.IsNullOrEmptyByTrim(input))
                return defaultValue;

            if (Wf_RegexHelper.IsNumeric(temp))
                return System.Convert.ToDecimal(input);
            else
                return defaultValue;
        }
        #endregion

        #region ToBoolean
        public static bool ToBoolean(object input)
        {
            return ToBoolean(input, false);
        }

        public static bool ToBoolean(object input, bool defaultValue)
        {
            if (Wf_StringHelper.IsNullOrEmptyByTrim(input))
                return defaultValue;

            try
            {
                return System.Convert.ToBoolean(input);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }
        #endregion

        #region ToDateTime
        public static DateTime ToDateTime(object input)
        {
            try
            {
                return ToDateTime(Wf_ConvertHelper.ToString(input));
            }
            catch { return DateTime.MinValue; }
        }

        public static DateTime ToDateTime(string input)
        {
            if (Wf_StringHelper.IsNullOrEmptyByTrim(input))
                return DateTime.Parse("1900-01-01");

            try
            {
                return System.Convert.ToDateTime(input);
            }
            catch (Exception)
            {
                return DateTime.MinValue;
            }
        }
        #endregion

        #region ToGUID
        public static Guid ToGUID(object input)
        {
            return ToGUID(input, Guid.Empty);
        }
        public static Guid ToGUID(object input, Guid defaultValue)
        {
            if (Wf_StringHelper.IsNullOrEmptyByTrim(input))
            {
                return defaultValue;
            }

            Guid g = Guid.Empty;

            if (Guid.TryParse(input.ToString(), out g))
            {
                return new Guid(input.ToString());
            }
            else
            {
                return defaultValue;
            }
        }
        #endregion

        #region 圆角半角
        /// <summary>
        /// 转全角的函数(SBC case)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToSBC(string input)
        {
            //半角转全角：
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 32)
                {
                    c[i] = (char)12288;
                    continue;
                }
                if (c[i] < 127)
                    c[i] = (char)(c[i] + 65248);
            }
            return new string(c);
        }

        /// <summary>
        ///  转半角的函数(SBC case)
        /// </summary>
        /// <param name="input">输入</param>
        /// <returns></returns>
        public static string ToDBC(string input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char)32;
                    continue;
                }
                if (c[i] > 65280 && c[i] < 65375)
                    c[i] = (char)(c[i] - 65248);
            }
            return new string(c);
        }
        #endregion

        #region 中英文符号切换

        static char[] zh = new char[] { '，', '。', '〈', '〉', '‖', '《', '》', '〔', '〕', '﹖', '？', '“', '”', '：', '、', '（', '）', '【', '】', '—', '～', '！', '‵', '①', '②', '③', '④', '⑤', '⑥', '⑦', '⑧', '⑨', };

        static char[] en = new char[] { ',', '.', '<', '>', '|', '<', '>', '[', ']', '?', '?', '\'', '\'', ':', ',', '(', ')', '[', ']', '-', '~', '!', '\'', '1', '2', '3', '4', '5', '6', '7', '8', '9', };

        /// <summary>
        /// 英文符号转换为中文符号
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToZhChar(string input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
                for (int j = 0; j < en.Length; j++)
                    if (c[i].Equals(en[j]))
                        c[i] = zh[j];
            return new string(c);
        }

        /// <summary>
        /// 中文符号转换为英文符号
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToEnChar(string input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
                for (int j = 0; j < zh.Length; j++)
                    if (c[i].Equals(zh[j]))
                        c[i] = en[j];
            return new string(c);
        }

        #endregion
    }
}
