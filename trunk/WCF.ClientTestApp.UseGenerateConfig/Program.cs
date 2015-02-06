using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCFService.Host.Console;

namespace WCF.ClientTestApp.UseGenerateConfig
{
    class Program
    {
        static void Main(string[] args)
        {
            StudentServiceClient client = new StudentServiceClient();
            IEnumerable<StudentInfo> x = client.GetStudentInfo(Int32.Parse("10010"));

            foreach (StudentInfo s in x)
            {
                Console.WriteLine(s.FirstName + s.LastName);
            }

            Console.ReadKey();
        }
    }
}
