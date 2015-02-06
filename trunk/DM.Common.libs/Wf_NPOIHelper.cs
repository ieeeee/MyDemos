using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using System.Text;
using NPOI;
using NPOI.HPSF;
using NPOI.HSSF;
using NPOI.POIFS;
using NPOI.Util;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.SS.Util;
using System.Collections.Generic;

/// <summary>
/// Summary description for NPOI
/// </summary>
/// 
namespace DM.Common.libs
{
    public static class Wf_NPOIHelper
    {
        //public Wf_NPOIHelper()
        //{
        //    //
        //    // TODO: Add constructor logic here
        //    //
        //}

        /// <summary>
        /// DataTable导出到Excel的MemoryStream
        /// </summary>
        /// <param name="dtSource">源DataTable</param>
        public static MemoryStream Export(DataTable dtSource)
        {
            DataTable[] dtList = new DataTable[] { dtSource };
            return Export(dtList);
        }

        /// <summary>
        /// DataTable导出到Excel的MemoryStream
        /// </summary>
        /// <param name="dtList">源DataTable[]</param>
        public static MemoryStream Export(DataTable[] dtList, CellDropDown[] listt = null)
        {
            HSSFWorkbook workbook = new HSSFWorkbook();
            int cellcount = 256;
            foreach (DataTable dtSource in dtList)
            {
                int sheetPage = Convert.ToInt32(Math.Round(Convert.ToDouble(dtSource.Columns.Count) / cellcount + 0.5));
                for (int k = 0; k < sheetPage; k++)
                {
                    //string sheetName = dtSource.TableName + k.ToString();
                    //string sheetName;
                    ////31：sheetname的长度
                    //if (sheetName.Length > 31)
                    //    sheetName = sheetName.Substring(sheetName.Length - 31);
                    //else
                    string sheetName = dtSource.TableName;
                    //sheetName = "Sheet1";
                    HSSFSheet sheet = workbook.CreateSheet(sheetName) as HSSFSheet;

                    #region 右击文件 属性信息

                    //{
                    //    DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
                    //    dsi.Company = "NPOI";
                    //    workbook.DocumentSummaryInformation = dsi;

                    //    SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
                    //    si.Author = "文件作者信息"; //填加xls文件作者信息
                    //    si.ApplicationName = "创建程序信息"; //填加xls文件创建程序信息
                    //    si.LastAuthor = "最后保存者信息"; //填加xls文件最后保存者信息
                    //    si.Comments = "作者信息"; //填加xls文件作者信息
                    //    si.Title = "标题信息"; //填加xls文件标题信息
                    //    si.Subject = "主题信息";//填加文件主题信息
                    //    si.CreateDateTime = DateTime.Now;
                    //    workbook.SummaryInformation = si;
                    //}

                    #endregion

                    //TODO:将注释内容还原
                    HSSFCellStyle dateStyle = workbook.CreateCellStyle() as HSSFCellStyle;
                    dateStyle.BorderBottom = CellBorderType.THIN;
                    dateStyle.BorderLeft = CellBorderType.THIN;
                    dateStyle.BorderRight = CellBorderType.THIN;
                    dateStyle.BorderTop = CellBorderType.THIN;
                    HSSFDataFormat format = workbook.CreateDataFormat() as HSSFDataFormat;
                    dateStyle.DataFormat = format.GetFormat("yyyy-mm-dd");

                    HSSFCellStyle headStyles = workbook.CreateCellStyle() as HSSFCellStyle;
                    headStyles.BorderBottom = CellBorderType.THIN;
                    headStyles.BorderLeft = CellBorderType.THIN;
                    headStyles.BorderRight = CellBorderType.THIN;
                    headStyles.BorderTop = CellBorderType.THIN;

                    //取得列宽
                    int[] arrColWidth = new int[dtSource.Columns.Count];
                    foreach (DataColumn item in dtSource.Columns)
                    {
                        arrColWidth[item.Ordinal] =
                            Encoding.GetEncoding(936).GetBytes(item.ColumnName.ToString()).Length;
                    }
                    for (int i = 0; i < dtSource.Rows.Count; i++)
                    {
                        for (int j = 0; j < dtSource.Columns.Count; j++)
                        {
                            int intTemp = Encoding.GetEncoding(936).GetBytes(dtSource.Rows[i][j].ToString()).Length;
                            if (intTemp > arrColWidth[j])
                            {
                                arrColWidth[j] = intTemp;
                            }
                        }
                    }
                    int rowIndex = 0;
                    foreach (DataRow row in dtSource.Rows)
                    {
                        #region 新建表，填充表头，填充列头，样式

                        if (rowIndex == 65535 || rowIndex == 0)
                        {
                            if (rowIndex != 0)
                            {
                                sheet = workbook.CreateSheet() as HSSFSheet;
                            }

                            #region 表头及样式

                            //{
                            //    HSSFRow headerRow = sheet.CreateRow(0) as HSSFRow;
                            //    headerRow.HeightInPoints = 25;
                            //    headerRow.CreateCell(0).SetCellValue(strHeaderText);

                            //    HSSFCellStyle headStyle = workbook.CreateCellStyle() as HSSFCellStyle;
                            //    headStyle.Alignment = HorizontalAlignment.CENTER;
                            //    HSSFFont font = workbook.CreateFont() as HSSFFont;
                            //    font.FontHeightInPoints = 20;
                            //    font.Boldweight = 700;
                            //    headStyle.SetFont(font);
                            //    headerRow.GetCell(0).CellStyle = headStyle;
                            //    sheet.AddMergedRegion(new Region(0, 0, 0, dtSource.Columns.Count - 1));
                            //    headerRow=null;
                            //}

                            #endregion


                            #region 列头及样式

                            {
                                HSSFRow headerRow = sheet.CreateRow(0) as HSSFRow;
                                HSSFCellStyle headStyle = workbook.CreateCellStyle() as HSSFCellStyle;
                                headStyle.Alignment = HorizontalAlignment.CENTER;
                                headStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.SKY_BLUE.index;
                                headStyle.FillPattern = NPOI.SS.UserModel.FillPatternType.SOLID_FOREGROUND;

                                //TODO:将注释内容还原
                                headStyle.BorderBottom = CellBorderType.THIN;
                                headStyle.BorderLeft = CellBorderType.THIN;
                                headStyle.BorderRight = CellBorderType.THIN;
                                headStyle.BorderTop = CellBorderType.THIN;


                                HSSFFont font = workbook.CreateFont() as HSSFFont;
                                font.FontHeightInPoints = 10;
                                font.Boldweight = 700;
                                headStyle.SetFont(font);

                                for (int d = 0; d < cellcount; d++)
                                {
                                    if (dtSource.Columns.Count > cellcount * k + d)
                                    {
                                        DataColumn column = dtSource.Columns[cellcount * k + d];
                                        headerRow.CreateCell(k == 0 ? column.Ordinal : column.Ordinal - cellcount).SetCellValue(column.ColumnName);
                                        headerRow.GetCell(k == 0 ? column.Ordinal : column.Ordinal - cellcount).CellStyle = headStyle;
                                    }
                                }

                                //foreach (DataColumn column in dtSource.Columns)
                                //{
                                //    headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
                                //    headerRow.GetCell(column.Ordinal).CellStyle = headStyle;

                                //    //设置列宽
                                //    //sheet.SetColumnWidth(column.Ordinal, (arrColWidth[column.Ordinal] + 1) * 256);
                                //}
                                headerRow = null;
                            }

                            #endregion

                            rowIndex = 1;
                        }

                        #endregion

                        #region 填充内容

                        HSSFRow dataRow = sheet.CreateRow(rowIndex) as HSSFRow;

                        for (int d = 0; d < cellcount; d++)
                        {
                            if (dtSource.Columns.Count > cellcount * k + d)
                            {
                                DataColumn column = dtSource.Columns[cellcount * k + d];

                                HSSFCell newCell = dataRow.CreateCell(k == 0 ? column.Ordinal : column.Ordinal - cellcount) as HSSFCell;
                                newCell.CellStyle = headStyles;

                                string drValue = row[column].ToString();

                                switch (column.DataType.ToString())
                                {
                                    case "System.String": //字符串类型
                                        newCell.SetCellValue(drValue);
                                        break;
                                    case "System.DateTime": //日期类型
                                        DateTime dateV;
                                        DateTime.TryParse(drValue, out dateV);
                                        newCell.SetCellValue(dateV);

                                        newCell.CellStyle = dateStyle; //格式化显示
                                        break;
                                    case "System.Boolean": //布尔型
                                        bool boolV = false;
                                        bool.TryParse(drValue, out boolV);
                                        newCell.SetCellValue(boolV);
                                        break;
                                    case "System.Int16": //整型
                                    case "System.Int32":
                                    case "System.Int64":
                                    case "System.Byte":
                                        int intV = 0;
                                        int.TryParse(drValue, out intV);
                                        newCell.SetCellValue(intV);
                                        break;
                                    case "System.Decimal": //浮点型
                                    case "System.Double":
                                        double doubV = 0;
                                        double.TryParse(drValue, out doubV);
                                        newCell.SetCellValue(doubV);
                                        break;
                                    case "System.DBNull": //空值处理
                                        newCell.SetCellValue("");
                                        break;
                                    case "System.Guid":
                                        newCell.SetCellValue(drValue.ToString());
                                        break;
                                    default:
                                        newCell.SetCellValue("");
                                        break;
                                }

                            }
                        }

                        #endregion

                        rowIndex++;
                    }
                }
            }

            //添加下拉列表
            if (listt != null)
            {
                foreach (var item in listt)
                {
                    CreateListConstaint(workbook, item.colmun, item.list, item.SheetName);
                }
            }
            
            
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;
                //workbook.Dispose();//一般只用写这一个就OK了，他会遍历并释放所有资源，但当前版本有问题所以只释放sheet
                return ms;
            }
        }

        /// <summary>
        /// 用于Web导出
        /// </summary>
        /// <param name="dtSource">源DataTable</param>
        public static void ExportByWeb(DataTable dtSource)
        {
            string strFileName = "Excel.xls";
            if (!dtSource.TableName.EndsWith(".xls"))
            {
                strFileName = dtSource.TableName + ".xls";
            }
            HttpContext curContext = HttpContext.Current;

            // 设置编码和附件格式
            curContext.Response.ContentType = "application/vnd.ms-excel";
            curContext.Response.ContentEncoding = Encoding.UTF8;
            curContext.Response.Charset = "";
            curContext.Response.AppendHeader("Content-Disposition",
                "attachment;filename=" + HttpUtility.UrlEncode(strFileName, Encoding.UTF8));

            curContext.Response.BinaryWrite(Export(dtSource).GetBuffer());
            curContext.Response.End();
        }

        /// <summary>
        /// 用于Web导出
        /// </summary>
        /// <param name="dtSource">源DataTable</param>
        public static void ExportByWeb(DataTable dtSource, string strFileName)
        {
            strFileName = (string.IsNullOrEmpty(strFileName)) ? "Excel.xls" : strFileName;
            if (!dtSource.TableName.EndsWith(".xls"))
            {
                strFileName = dtSource.TableName + ".xls";
            }
            HttpContext curContext = HttpContext.Current;
            // 设置编码和附件格式
            curContext.Response.ContentType = "application/vnd.ms-excel";
            curContext.Response.ContentEncoding = Encoding.UTF8;
            curContext.Response.Charset = "";
            curContext.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(strFileName, Encoding.UTF8));
            curContext.Response.BinaryWrite(Export(dtSource).GetBuffer());
            curContext.Response.End();
        }

        public static void ExportReportToExcelToFolder(string filePath, DataTable dtSource)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                byte[] stream = Export(dtSource).GetBuffer();
                fs.Write(stream, 0, stream.Length);
            }
        }

        /// <summary>
        /// 用于Web导出
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="dtList">源DataTable</param>
        public static void ExportMultiSheetsByWeb(string fileName, DataTable[] dtList, params CellDropDown[] listt)
        {
            string strFileName = (string.IsNullOrEmpty(fileName)) ? "Excel.xls" : fileName;

            if (!strFileName.EndsWith(".xls"))
            {
                strFileName = strFileName + ".xls";
            }

            HttpContext curContext = HttpContext.Current;

            // 设置编码和附件格式
            curContext.Response.ContentType = "application/vnd.ms-excel";
            curContext.Response.ContentEncoding = Encoding.UTF8;
            curContext.Response.Charset = "";
            curContext.Response.AppendHeader("Content-Disposition",
                "attachment;filename=" + HttpUtility.UrlEncode(strFileName, Encoding.UTF8));

            foreach (DataTable dtSource in dtList)
            {
                curContext.Response.BinaryWrite(Export(dtList,listt).GetBuffer());
            }
            curContext.Response.End();
        }

        /// <summary>读取excel
        /// 默认第一行为标头
        /// </summary>
        /// <param name="strFileName">excel文档路径</param>
        /// <returns></returns>
        public static DataTable Import(string strFileName, string sheetName)
        {
            DataTable dt = new DataTable();

            HSSFWorkbook hssfworkbook;
            using (FileStream file = new FileStream(strFileName, FileMode.Open, FileAccess.Read))
            {
                hssfworkbook = new HSSFWorkbook(file);
            }

            HSSFSheet sheet = null;
            if (!string.IsNullOrEmpty(sheetName))
            {
                sheet = hssfworkbook.GetSheet(sheetName) as HSSFSheet;
            }
            else
            {
                sheet = hssfworkbook.GetSheetAt(0) as HSSFSheet;
            }

            System.Collections.IEnumerator rows = sheet.GetRowEnumerator();

            HSSFRow headerRow = sheet.GetRow(0) as HSSFRow;
            int cellCount = headerRow.LastCellNum;

            for (int j = 0; j < cellCount; j++)
            {
                HSSFCell cell = headerRow.GetCell(j) as HSSFCell;
                dt.Columns.Add(cell.ToString());
            }

            for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
            {
                HSSFRow row = sheet.GetRow(i) as HSSFRow;
                DataRow dataRow = dt.NewRow();
                if (row != null)
                {
                    for (int j = row.FirstCellNum; j < cellCount; j++)
                    {
                        if (row.GetCell(j) != null)
                        {
                            if (row.GetCell(j).CellType == CellType.NUMERIC)
                            {
                                if (DateUtil.IsCellDateFormatted(row.GetCell(j)))
                                {
                                    dataRow[j] = row.GetCell(j).DateCellValue.ToString("yyyy-MM-dd");
                                }
                                else
                                {
                                    dataRow[j] = row.GetCell(j).ToString();
                                }
                            }
                            else if (row.GetCell(j).CellType == CellType.FORMULA)
                            {
                                dataRow[j] = row.GetCell(j).NumericCellValue.ToString();
                            }
                            else
                            {
                                dataRow[j] = row.GetCell(j).ToString();
                            }

                        }
                    }
                }

                dt.Rows.Add(dataRow);
            }
            return dt;
        }


        #region CreateListConstaint 给列加下拉框并验证
        /// <summary>
        /// 引用另一个工作表的形式 得到下拉
        /// </summary>
        /// <param name="book"></param>
        /// <param name="columnIndex"></param>
        /// <param name="values"></param>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        public static void CreateListConstaint(this HSSFWorkbook book, Int32 columnIndex, IEnumerable<String> values,string sheetName="")
        {
            if (values == null )
            {
                return;
            }
            if (string.IsNullOrEmpty(sheetName))
            {
                sheetName = "_constraintSheet_";
            }
            //创建下拉数据到新表中
            ISheet sheet = book.GetSheet(sheetName) ?? book.CreateSheet(sheetName); ;
            var firstRow = sheet.GetRow(0);
            var conColumnIndex = firstRow == null ? 0 : firstRow.PhysicalNumberOfCells;
            var rowIndex = 0;
            var lastValue = string.Empty;

            foreach (var value in values)
            {
                var row = sheet.GetRow(rowIndex) ?? sheet.CreateRow(rowIndex);
                row.CreateCell(conColumnIndex).SetCellValue(value);
                rowIndex++;
                lastValue = value;
            }

            //如果无可选值的话，则增加一个空选项，防止用户填写内容  
            if (values.Count() == 0)
            {
                var row = sheet.GetRow(rowIndex) ?? sheet.CreateRow(rowIndex);
                row.CreateCell(conColumnIndex).SetCellValue("    ");
                rowIndex++;
            }

            //给该列所有单元格加上下拉选择
            IName range = book.CreateName() ;
            range.RefersToFormula = String.Format("{2}!${0}$1:${0}${1}",
                                            (Char)('A' + conColumnIndex),
                                            rowIndex.ToString(), sheetName);
            string rangeName = "dicRange" + columnIndex;
            range.NameName = rangeName;
            var cellRegions = new CellRangeAddressList(1, 65535, columnIndex, columnIndex);
            var constraint = DVConstraint.CreateFormulaListConstraint(rangeName);
            book.SetSheetHidden(book.GetSheetIndex(sheet), SheetState.HIDDEN);

            //创建验证
            HSSFDataValidation valid=new HSSFDataValidation(cellRegions, constraint);

            //关联验证
            HSSFSheet v = book.GetSheetAt(0) as HSSFSheet;
            v.AddValidationData(valid);
        }
        #endregion

    }

    #region CellDropDown 下拉类
    /// <summary>
    /// 单元格下拉选择类
    /// </summary>
    public class CellDropDown
    {
        public CellDropDown()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_colmun">第多少列，从0开始计算</param>
        /// <param name="_list">下拉数据集合</param>
        /// <param name="sheetName">存放下拉数据的sheet名，默认为aaa</param>
        public CellDropDown(int _colmun, List<string> _list, string sheetName = "aaa")
        {
            colmun = _colmun;
            list = _list;
            _sheetName = sheetName;
        }

        
        /// <summary>
        /// 多少列，从0开始计算
        /// </summary>
        public int colmun { get; set; }

        /// <summary>
        /// 下拉数据集合，暂采用字符串集合
        /// </summary>
        public List<string> list { get; set; }

        /// <summary>
        /// 数据下拉存放sheet名 默认为“aaa”
        /// </summary>
        private string _sheetName="aaa";
        public string SheetName { get { return _sheetName; } set { _sheetName = value; } }
    }
    #endregion
}
