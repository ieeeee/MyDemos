using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication2
{
    public partial class OfficeDocToPdfDemo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //bool convertFlag = false;

            //string doc = "Vs2012.doc";
            //string docx = "CSharp Language Specification.docx";

            //string xls = "banche.xls";
            //string xlsx = "Book1.xlsx零部件价目表.xlsx";

            //string ppt1 = "abc.ppt";
            //string ppt2 = "CoreSummary2010.ppt";
            //string pptx = "ppt4113.pptx";

            ////调用COM组建转换文档
            //ConvertToPDF converter = new ConvertToPDF();
            //convertFlag = converter.DOC2PDF(GetFilePath(doc), Server.MapPath(string.Format("~/OfficeDoc/pdf/{0}.pdf", doc)));
            //if (!convertFlag)
            //{
            //    Response.Clear();
            //    Response.Write(string.Format("转换文件{0}失败,失败原因：<p>【{1}】</p>", doc, converter.ExceptionMsg));
            //    Response.End();
            //}

            //convertFlag = converter.DOC2PDF(GetFilePath(docx), Server.MapPath(string.Format("~/OfficeDoc/pdf/{0}.pdf", docx)));
            //if (!convertFlag)
            //{
            //    Response.Clear();
            //    Response.Write(string.Format("转换文件{0}失败,失败原因：<p>【{1}】</p>", docx, converter.ExceptionMsg));
            //    Response.End();
            //}

            //convertFlag = converter.XLS2PDF(GetFilePath(xls), Server.MapPath(string.Format("~/OfficeDoc/pdf/{0}.pdf", xls)));
            //if (!convertFlag)
            //{
            //    Response.Clear();
            //    Response.Write(string.Format("转换文件{0}失败,失败原因：<p>【{1}】</p>", xls, converter.ExceptionMsg));
            //    Response.End();
            //}
            //convertFlag = converter.XLS2PDF(GetFilePath(xlsx), Server.MapPath(string.Format("~/OfficeDoc/pdf/{0}.pdf", xlsx)));
            //if (!convertFlag)
            //{
            //    Response.Clear();
            //    Response.Write(string.Format("转换文件{0}失败,失败原因：<p>【{1}】</p>", xlsx, converter.ExceptionMsg));
            //    Response.End();
            //}

            //convertFlag = converter.PPT2PDF(GetFilePath(ppt1), Server.MapPath(string.Format("~/OfficeDoc/pdf/{0}.pdf", ppt1)));
            //if (!convertFlag)
            //{
            //    Response.Clear();
            //    Response.Write(string.Format("转换文件{0}失败,失败原因：<p>【{1}】</p>", ppt1, converter.ExceptionMsg));
            //    Response.End();
            //}
            //convertFlag = converter.PPT2PDF(GetFilePath(ppt2), Server.MapPath(string.Format("~/OfficeDoc/pdf/{0}.pdf", ppt2)));
            //if (!convertFlag)
            //{
            //    Response.Clear();
            //    Response.Write(string.Format("转换文件{0}失败,失败原因：<p>【{1}】</p>", ppt2, converter.ExceptionMsg));
            //    Response.End();
            //}
            //convertFlag = converter.PPT2PDF(GetFilePath(pptx), Server.MapPath(string.Format("~/OfficeDoc/pdf/{0}.pdf", pptx)));
            //if (!convertFlag)
            //{
            //    Response.Clear();
            //    Response.Write(string.Format("转换文件{0}失败,失败原因：<p>【{1}】</p>", pptx, converter.ExceptionMsg));
            //    Response.End();
            //}

            ConvertToPDF converter = new ConvertToPDF();
            string officeDocFileBasePath = Server.MapPath("~/OfficeDoc/");
            DirectoryInfo dti = new DirectoryInfo(officeDocFileBasePath);
            FileInfo[] allFI = dti.GetFiles("*.*", SearchOption.TopDirectoryOnly);
            IList<string> lstOfficeDocs = new List<string>();

            foreach (FileInfo item in allFI)
            {
                string fileName = item.Name.ToLower();
                if (fileName.EndsWith(".xls") || fileName.EndsWith(".xlsx") || fileName.EndsWith(".doc") || fileName.EndsWith(".docx") || fileName.EndsWith(".ppt") || fileName.EndsWith(".pptx"))
                {
                    lstOfficeDocs.Add(item.FullName);
                }
            }

            System.Text.StringBuilder sbConvertRsInfo = new System.Text.StringBuilder();
            sbConvertRsInfo.Append("<table  width='90%' class='table'>");
            sbConvertRsInfo.Append("<tr><th>文件</th><th>转换结果</th></tr>");
            foreach (string item in lstOfficeDocs)
            {
                FileInfo fi = new FileInfo(item);
                string fileExt = fi.Extension;
                bool flag = false;
                switch (fileExt)
                {
                    case ".xls":
                    case ".xlsx":
                        flag = converter.XLS2PDF(fi.FullName, fi.FullName + ".pdf");
                        break;
                    case ".doc":
                    case ".docx":
                        flag = converter.DOC2PDF(fi.FullName, fi.FullName + ".pdf");
                        break;
                    case ".ppt":
                    case ".pptx":
                        flag = converter.PPT2PDF(fi.FullName, fi.FullName + ".pdf");
                        break;
                }
                sbConvertRsInfo.AppendFormat("<tr><td>{0}</td><td>{1}</td></tr>", fi.Name, flag ? "成功" : "失败");
            }
            sbConvertRsInfo.Append("</table>");
            litConvertRs.Text = sbConvertRsInfo.ToString();
        }

        /// <summary>
        /// 获取文件实际路径
        /// </summary>
        /// <param name="filename"></param>
        protected string GetFilePath(string filename)
        {
            string vPath = string.Format("~/OfficeDoc/{0}", filename);
            return Server.MapPath(vPath);
        }
    }
}