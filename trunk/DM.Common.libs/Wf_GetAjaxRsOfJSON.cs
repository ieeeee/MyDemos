using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace DM.Common.libs
{
    public class Wf_GetAjaxRsOfJSON
    {
        protected Hashtable hst = new Hashtable();

        /// <summary>
        /// 构造函数
        /// </summary>
        public Wf_GetAjaxRsOfJSON()
        {

        }

        /// <summary>
        /// 添加Json结果属性
        /// </summary>
        /// <param name="key">属性名</param>
        /// <param name="value">值</param>
        /// <returns>是否添加成功</returns>
        public bool AddItemJson(object key, object value)
        {
            try
            {
                hst.Add(key, value);
                return true;
            }
            catch (Exception )
            {
                return false;
            }
        }

        /// <summary>
        /// 获取Json格式结果
        /// </summary>
        /// <returns>Json格式结果</returns>
        public string GetRsOfJson()
        {
            if (hst.Count > 0)
            {
                try
                {
                    return JsonConvert.SerializeObject(hst);
                }
                catch (Exception)
                {
                    return string.Empty;
                }
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
