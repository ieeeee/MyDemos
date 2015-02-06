using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandleByAspose.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            ImportExcel2DataTable xls2dt = new ImportExcel2DataTable();
            DataTable dt = xls2dt.ImportUserInfo();
            StringBuilder sbRowData = new StringBuilder();

            foreach (DataRow dr in dt.Rows)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    sbRowData.AppendFormat("{0}|", dr[i]);
                }
                System.Console.WriteLine(sbRowData.ToString());
                sbRowData.Clear();
            }

            System.Console.ReadKey();
        }
    }
}
