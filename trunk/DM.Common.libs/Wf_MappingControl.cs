using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Linq;
using System.Text;
using System.Reflection;

namespace DM.Common.libs
{
    public class Wf_MappingControl
    {
        /// <summary>
        /// 将指定的对象数据绑定到页面控件上显示
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="form"></param>
        public static void MappingControlShow(object obj, HtmlForm form)
        {
            MappingControlShow(obj, form, null);
        }

        public static void MappingControlShow(object obj, HtmlForm form, ContentPlaceHolder cph)
        {
            try
            {
                if (obj == null) return;

                List<PropertyInfo> pilist = obj.GetType().GetProperties().ToList();

                Control baseControl = cph == null ? (Control)form : (Control)cph;

                foreach (PropertyInfo pi in pilist)
                {
                    Control ctrl = baseControl.FindControl("txt" + pi.Name);
                    ctrl = ctrl == null ? baseControl.FindControl("lbl" + pi.Name) : ctrl;
                    ctrl = ctrl == null ? baseControl.FindControl("lit" + pi.Name) : ctrl;
                    ctrl = ctrl == null ? baseControl.FindControl("hid" + pi.Name) : ctrl;
                    ctrl = ctrl == null ? baseControl.FindControl("ddl" + pi.Name) : ctrl;
                    ctrl = ctrl == null ? baseControl.FindControl("rdo" + pi.Name) : ctrl;
                    ctrl = ctrl == null ? baseControl.FindControl("chk" + pi.Name) : ctrl;
                    if (ctrl == null)
                        continue;
                    else if (ctrl is TextBox)
                    {
                        TextBox txt = ctrl as TextBox;
                        txt.Text = Wf_ConvertHelper.ToString(pi.GetValue(obj, null));
                    }
                    else if (ctrl is Label)
                    {
                        Label lbl = ctrl as Label;
                        lbl.Text = Wf_ConvertHelper.ToString(pi.GetValue(obj, null));
                    }
                    else if (ctrl is Literal)
                    {
                        Literal lit = ctrl as Literal;
                        lit.Text = Wf_ConvertHelper.ToString(pi.GetValue(obj, null));
                    }
                    else if (ctrl is HiddenField)
                    {
                        HiddenField hid = ctrl as HiddenField;
                        hid.Value = Wf_ConvertHelper.ToString(pi.GetValue(obj, null));
                    }
                    else if (ctrl is DropDownList)
                    {
                        DropDownList ddl = ctrl as DropDownList;
                        foreach (ListItem item in ddl.Items)
                        {
                            object value = pi.GetValue(obj, null);
                            if (item.Value == Wf_ConvertHelper.ToString(value))
                                ddl.SelectedValue = item.Value;
                        }
                    }
                    else if (ctrl is RadioButtonList)
                    {
                        RadioButtonList rdo = ctrl as RadioButtonList;

                        object value = pi.GetValue(obj, null);
                        string[] arr = Wf_ConvertHelper.ToString(value, "").Split(',');
                        foreach (ListItem item in rdo.Items)
                        {
                            if (arr.Contains(item.Value))
                                item.Selected = true;
                        }
                    }
                    else if (ctrl is CheckBoxList)
                    {
                        CheckBoxList chk = ctrl as CheckBoxList;

                        object value = pi.GetValue(obj, null);
                        string[] arr = Wf_ConvertHelper.ToString(value, "").Split(',');
                        foreach (ListItem item in chk.Items)
                        {
                            if (arr.Contains(item.Value))
                                item.Selected = true;
                        }
                    }
                    continue;
                }
            }

            catch { }
        }

        /// <summary>
        /// 从页面控件中获取数据绑定到对应的对象属性上
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="form"></param>
        /// <returns></returns>
        public static object MappingControlObject(object obj, HtmlForm form)
        {
            return MappingControlObject(obj, form, null);
        }

        public static object MappingControlObject(object obj, HtmlForm form, bool SqlKeyWordsFilter)
        {
            return MappingControlObject(obj, form, null, SqlKeyWordsFilter);
        }

        public static object MappingControlObject(object obj, HtmlForm form, ContentPlaceHolder cph)
        {
            return MappingControlObject(obj, form, cph, true);
        }

        public static object MappingControlObject(object obj, HtmlForm form, ContentPlaceHolder cph, bool SqlKeyWordsFilter)
        {
            try
            {
                if (obj == null) return obj;

                List<PropertyInfo> pilist = obj.GetType().GetProperties().ToList();

                Control baseControl = cph == null ? (Control)form : (Control)cph;

                foreach (PropertyInfo pi in pilist)
                {
                    Control ctrl = baseControl.FindControl("txt" + pi.Name);
                    ctrl = ctrl == null ? baseControl.FindControl("lbl" + pi.Name) : ctrl;
                    ctrl = ctrl == null ? baseControl.FindControl("lit" + pi.Name) : ctrl;
                    ctrl = ctrl == null ? baseControl.FindControl("hid" + pi.Name) : ctrl;
                    ctrl = ctrl == null ? baseControl.FindControl("ddl" + pi.Name) : ctrl;
                    ctrl = ctrl == null ? baseControl.FindControl("rdo" + pi.Name) : ctrl;
                    ctrl = ctrl == null ? baseControl.FindControl("chk" + pi.Name) : ctrl;
                    if (ctrl == null)
                        continue;
                    else if (ctrl is TextBox)
                    {
                        TextBox txt = ctrl as TextBox;
                        if (Wf_StringHelper.IsNullOrEmptyByTrim(txt.Text)) continue;
                        pi.SetValue(obj, ChangeType(txt.Text, pi.PropertyType, SqlKeyWordsFilter), null);
                    }
                    else if (ctrl is Label)
                    {
                        Label lbl = ctrl as Label;
                        if (Wf_StringHelper.IsNullOrEmptyByTrim(lbl.Text)) continue;
                        pi.SetValue(obj, ChangeType(lbl.Text, pi.PropertyType, SqlKeyWordsFilter), null);
                    }
                    else if (ctrl is Literal)
                    {
                        Literal lit = ctrl as Literal;
                        if (Wf_StringHelper.IsNullOrEmptyByTrim(lit.Text)) continue;
                        pi.SetValue(obj, ChangeType(lit.Text, pi.PropertyType, SqlKeyWordsFilter), null);
                    }
                    else if (ctrl is HiddenField)
                    {
                        HiddenField hid = ctrl as HiddenField;
                        if (Wf_StringHelper.IsNullOrEmptyByTrim(hid.Value)) continue;
                        pi.SetValue(obj, ChangeType(hid.Value, pi.PropertyType, SqlKeyWordsFilter), null);
                    }
                    else if (ctrl is DropDownList)
                    {
                        DropDownList ddl = ctrl as DropDownList;
                        if (Wf_StringHelper.IsNullOrEmptyByTrim(ddl.SelectedValue)) continue;
                        pi.SetValue(obj, ChangeType(ddl.SelectedValue, pi.PropertyType, SqlKeyWordsFilter), null);
                    }
                    else if (ctrl is RadioButtonList)
                    {
                        RadioButtonList rdo = ctrl as RadioButtonList;

                        string value = "";
                        foreach (ListItem item in rdo.Items)
                        {
                            if (item.Selected)
                                value += Wf_StringHelper.IsNullOrEmptyByTrim(value) ? item.Value : "," + item.Value;
                        }
                        if (Wf_StringHelper.IsNullOrEmptyByTrim(value)) continue;
                        pi.SetValue(obj, ChangeType(value, pi.PropertyType, SqlKeyWordsFilter), null);
                    }
                    else if (ctrl is CheckBoxList)
                    {
                        CheckBoxList chk = ctrl as CheckBoxList;
                        string value = "";
                        foreach (ListItem item in chk.Items)
                        {
                            if (item.Selected)
                                value += Wf_StringHelper.IsNullOrEmptyByTrim(value) ? item.Value : "," + item.Value;
                        }
                        if (Wf_StringHelper.IsNullOrEmptyByTrim(value)) continue;
                        pi.SetValue(obj, ChangeType(value, pi.PropertyType, SqlKeyWordsFilter), null);
                    }
                    continue;
                }
                return obj;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 转换Nullable类型
        /// </summary>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <returns></returns>

        private static object ChangeType(object value, Type type)
        {
            return ChangeType(value, type, true);
        }

        private static object ChangeType(object value, Type type, bool SqlKeyWordsFilter)
        {
            string tempValue = SqlKeyWordsFilter == true ?
                Wf_RegexHelper.ParseHtmlBQ(Wf_RegexHelper.SqlKeyWordsFilter(Wf_ConvertHelper.ToString(value))) : Wf_ConvertHelper.ToString(value);

            if (type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                System.ComponentModel.NullableConverter nullableConverter = new System.ComponentModel.NullableConverter(type);
                return Convert.ChangeType(tempValue, nullableConverter.UnderlyingType);
            }
            else
                return Convert.ChangeType(tempValue, type);
        }
    }
}
