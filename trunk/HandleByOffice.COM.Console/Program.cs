using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Convert Start...");

            ConvertToPDF converter = new ConvertToPDF();

            //converter.DOC2PDF(@"D:\test_doc\CSharp Language Specification.doc", @"D:\test_doc\CSharp Language Specification1.pdf");
            //converter.XLS2PDF(@"D:\test_doc\Book1.xlsx零部件价目表.xls", @"D:\test_doc\Book1.xlsx零部件价目表.pdf");
            //converter.PPT2PDF(@"D:\test_doc\ppt4113.ppt", @"D:\test_doc\ppt4113.pdf");

            //converter.DOC2PDF(@"D:\test_doc\CSharp Language Specification.docx", @"D:\test_doc\CSharp Language Specification.pdf");
            //converter.XLS2PDF(@"D:\test_doc\Book1.xlsx零部件价目表.xlsx", @"D:\test_doc\Book1.xlsx零部件价目表.pdf");
            //converter.PPT2PDF(@"D:\test_doc\ppt4113.pptx", @"D:\test_doc\ppt4113.pdf");


            converter.PPT2PDF(@"D:\test_doc\精美的富有创意的PPT图表1.7.ppt", @"D:\test_doc\精美的富有创意的PPT图表1.7.pdf");


            Console.WriteLine("Convert Over.");

            Console.ReadKey();
        }
    }
}
