using Aspose.Cells;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandleByAspose.Console
{
    public class ImportExcel2DataTable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="xlsPath"></param>
        public DataTable ImportUserInfo(string xlsPath = "")
        {
            try
            {
                xlsPath = string.IsNullOrWhiteSpace(xlsPath) ? Environment.CurrentDirectory + @"\test.xlsx" : xlsPath;

                Aspose.Cells.Workbook workbook = new Aspose.Cells.Workbook(xlsPath);

                Aspose.Cells.Worksheet worksheet = workbook.Worksheets[0];

                Cells cells = worksheet.Cells;

                DataTable datatable = cells.ExportDataTableAsString(0, 0, cells.MaxDataRow + 1, cells.MaxDataColumn + 1, true);

                return datatable;
            }
            catch (Exception ex)
            {
                throw ex;
                //MessageBox.Show(string.Format("您选择的Excel数据格式错误[{0}]", ex.Message));
            }
        }
    }
}
